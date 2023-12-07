using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Microsoft.EntityFrameworkCore;
using SuryoyoBibliotek.Model;

namespace SuryoyoBibliotek.Data
{
        internal class DataAccess
        {
        public enum BookTitles
        {
            [Description("The Godfather")] Godfather, [Description("Lord of the rings")] Lotr, [Description("The Dark Knight")] TDK,
            [Description("Game Of Thrones")] GOT, [Description("House of the Dragon")] HotD, [Description("Avengers")] MCU,
            [Description("The Revenant")] Revenant, [Description("Harry Potter")] HarryPotter, [Description("Percy Jackson")] PJ, 
            [Description("The Green Mile")] GreenMile, [Description("The Hobbit")] Hobbit, [Description("The Mist")] Mist, 
            [Description("The Chronicles of Spiderwick")] Spiderwick, [Description("The Hunger Games")] THG, 
            [Description("The Chronicles of Narnia")] Narnia, [Description("The Twilight Series")] Twilight,
        }


        internal csSeedGenerator rngGenerator = new csSeedGenerator();

            public void CreateFiller()
            {
                using (var context = new Context())
                {
                    for (int i = 0; i < 10; i++)
                    {
                        User user = new User();

                        user.FirstName = rngGenerator.FirstName;
                        user.LastName = rngGenerator.LastName;

                        Book book = new Book();
                        book.RentalYear = rngGenerator.Next(1950, 2023);
                        book.BookTitle = GetEnumDescription(rngGenerator.FromEnum<BookTitles>());
                        RentalCard rentCard = new RentalCard();

                        Author author1 = new Author();

                        author1.Name = rngGenerator.FullName;

                        context.Users.Add(user);
                        context.Books.Add(book);
                        context.Authors.Add(author1);
                        context.RentedCards.Add(rentCard);


                    }

                    context.SaveChanges();
                }
            }


            public void MarkBookAsNotLoaned(int bookId)
            {
                using (var context = new Context())
                {
                    var book = context.Books.Include(b => b.RentalCards).FirstOrDefault(b => b.BookID == bookId);

                    if (book != null)
                    {
                        // Update LoanCardId to null, marking the book as not loaned
                        book.LoanCardId = null;

                        // If the book was associated with a LoanCard, remove it from the LoanCard's collection
                        if (book.RentalCards != null)
                        {
                            book.RentalCards.Books.Remove(book);
                        }

                        // Save changes to the database
                        context.SaveChanges();
                    }
                }
            }


            public void AddPersonToDatabase(string firstName, string lastName)
            {
                using (var context = new Context())
                {
                    var person = new User
                    {
                        FirstName = firstName,
                        LastName = lastName
                    };

                    context.Users.Add(person);
                    context.SaveChanges();
                }
            }

            public void AddBookToDatabase(string title, params int[] autorIds)
            {
                using (var context = new Context())
                {
                    var autors = context.Authors.Where(a => autorIds.Contains(a.AuthorId)).ToList();

                    var book = new Book
                    {
                        BookTitle = title,
                        Authors = autors,
                        RentalYear = new Random().Next(1900, 2023)

                    };

                    context.Books.Add(book);
                    context.SaveChanges();
                }
            }



            public void AddLoanCardToPerson(int id)
            {
                using (var context = new Context())
                {
                    // Step 1: Retrieve the Person
                    var person = context.Users.FirstOrDefault(user =>  user.Id == id);

                    if (person == null)
                    {
                        // Handle the case where the person with the specified ID doesn't exist
                        // You can throw an exception, log a message, or take appropriate action
                        return;
                    }

                    // Step 2: Create a new LoanCard
                    var loanCard = new RentalCard();

                    // Step 3: Link the LoanCard to the Person
                    person.RentalCard = loanCard;

                    // Step 4: Save changes to the database
                    context.SaveChanges();
                }
            }

            public void AddBookIdToPersonLoanCard(int personId, int bookId)
            {
                using (var context = new Context())
                {
                    // Step 1: Retrieve the Person with LoanCard
                    var person = context.Users.Include(p => p.RentalCard).SingleOrDefault(p => p.Id == personId);

                    if (person == null)
                    {
                        // Handle the case where the person with the specified ID doesn't exist
                        // You can throw an exception, log a message, or take appropriate action
                        return;
                    }

                    // Step 2: Check if the person has a LoanCard
                    if (person.RentalCard == null)
                    {
                        // Handle the case where the person doesn't have a LoanCard
                        // You can create a new LoanCard, associate it with the person, and proceed
                        return;
                    }

                    // Step 3: Link the existing book to the LoanCard using the book ID

                    var book = context.Books.Find(bookId);

                    if (book != null)
                    {
                        // Assuming LoanCardId is the foreign key in the Book entity
                        book.LoanCardId = person.RentalCard.RentalCardId;
                        context.SaveChanges(); // Save changes to the book
                    }
                }
            }


            public void Clear()
            {
            using (var context = new Context())
                {
                    var allPersons = context.Users.ToList();
                    context.Users.RemoveRange(allPersons);
                    var allBooks = context.Books.ToList();
                    context.Books.RemoveRange(allBooks);
                    var allAutors = context.Authors.ToList();
                    context.Authors.RemoveRange(allAutors);
                    var allLoanC = context.RentedCards.ToList();
                    context.RemoveRange(allLoanC);
                    context.SaveChanges();

                    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Users', RESEED, 0)");
            }
        }

            private string GetEnumDescription(Enum value)
            {
                var field = value.GetType().GetField(value.ToString());

                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    return attribute.Description;
                }

                return value.ToString();
            }


        public void RemoveBookFromDatabase(int Book_ID)
        {
            using(var context = new Context())
            {
                var RemoveBook = context.Books.SingleOrDefault(b=> b.BookID == Book_ID);
                if (RemoveBook != null)
                {
                    context.Books.Remove(RemoveBook);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("There is no book with this book id!");
                }
            }
        }


        public void RemoveUserFromDatabase(int user_id)
        {
            using(var context = new Context())
            {
                var RemoveUser = context.Users.Include(u=> u.RentalCard).SingleOrDefault(u=> u.Id == user_id);
                if (RemoveUser != null)
                {
                    var UsersRentalCard = RemoveUser.RentalCard.RentalCardId;
                    context.Users.Remove(RemoveUser);
                    var usersBook = context.Books.SingleOrDefault(b=> b.LoanCardId == UsersRentalCard);

                    if (UsersRentalCard != null)
                    {
                        usersBook.Loaned = false;
                        usersBook.RentalCards = null;
                        usersBook.ReturnDate = null;
                        usersBook.LoanDate = null;
                    }
                    else
                    {
                        Console.WriteLine("There is no book with this id!");
                    }
                }
                context.SaveChanges();
            }
        }


        public void RemoveAuthorFromDatabase(int author_id)
        {
            using (var context = new Context())
            {
                var removeAuthor = context.Authors.SingleOrDefault(a => a.AuthorId == author_id);
                if (removeAuthor != null)
                {
                    context.Authors.Remove(removeAuthor);
                }
                else
                {
                    Console.WriteLine("There is no book with this id!");
                }
                context.SaveChanges();
            }
        }
        
    }
}
