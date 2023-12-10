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

            public void CreateOfEverything()
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


            public void MarkBookAsNotRented(int bookId)
            {
                using (var context = new Context())
                {
                    var book = context.Books.Include(b => b.RentalCards).FirstOrDefault(b => b.BookID == bookId);

                    if (book != null)
                    {
                        book.RentCardId = null;

                        if (book.RentalCards != null)
                        {
                            book.RentalCards.Books.Remove(book);
                        }

                        context.SaveChanges();
                    }
                }
            }



            public void AddUserToDatabase(string firstName, string lastName)
            {
                using (var context = new Context())
                {
                    var user2 = new User
                    {
                        FirstName = firstName,
                        LastName = lastName
                    };

                    context.Users.Add(user2);
                    context.SaveChanges();
                }
            }

            public void AddBookToDatabase(string title, params int[] authorIds)
            {
                using (var context = new Context())
                {
                    var authors = context.Authors.Where(a => authorIds.Contains(a.AuthorId)).ToList();

                    var book = new Book
                    {
                        BookTitle = title,
                        Authors = authors,
                        RentalYear = new Random().Next(1900, 2023)

                    };

                    context.Books.Add(book);
                    context.SaveChanges();
                }
            }

            public void AddAuthorToDatabase()
            {
                csSeedGenerator rngGenerator2 = new csSeedGenerator();

                using (var context = new Context())
                {
                var author2 = new Author
                {
                    Name = rngGenerator2.FullName

                };
                    context.Authors.Add(author2);
                    context.SaveChanges();
                }
            }



            public void AddRentCardToUser(int id)
            {
                using (var context = new Context())
                {
                    var person = context.Users.FirstOrDefault(user =>  user.Id == id);

                    if (person == null)
                    {
                        return;
                    }

                    var loanCard = new RentalCard();

                    person.RentalCard = loanCard;

                    context.SaveChanges();
                }
            }

            public void AddBookIdToPersonRentCard(int customerId, int bookId)
            {
                using (var context = new Context())
                {
                    var person = context.Users.Include(p => p.RentalCard).SingleOrDefault(p => p.Id == customerId);

                    if (person == null)
                    {
                        return;
                    }

                    if (person.RentalCard == null)
                    {
                        return;
                    }

                    var book = context.Books.Find(bookId);

                    if (book != null)
                    {
                        book.RentCardId = person.RentalCard.RentalCardId;
                        context.SaveChanges();
                    }
                }
            }


            public void Clear()
            {
            using (var context = new Context())
                {
                    var allUsers = context.Users.ToList();
                    context.Users.RemoveRange(allUsers);
                    var allBooks = context.Books.ToList();
                    context.Books.RemoveRange(allBooks);
                    var allAuthors = context.Authors.ToList();
                    context.Authors.RemoveRange(allAuthors);
                    var allRentCards = context.RentedCards.ToList();
                    context.RemoveRange(allRentCards);
                    context.SaveChanges();

                    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Users', RESEED, 0)");
                    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Authors', RESEED, 0)");
                    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('RentedCards', RESEED, 0)");
                    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Books', RESEED, 0)");

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
                    var usersBook = context.Books.SingleOrDefault(b=> b.RentCardId == UsersRentalCard);

                    if (usersBook != null)
                    {
                        usersBook.Borrowed = false;
                        usersBook.RentCardId = null;
                        usersBook.ReturnDate = null;
                        usersBook.HireDate = null;
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
