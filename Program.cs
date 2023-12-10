using SuryoyoBibliotek.Data;

namespace SuryoyoBibliotek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataAccess data = new DataAccess();
            Context context1 = new Context();
            Context context2 = new Context();
            Context context3 = new Context();

            //data.CreateFiller();     // Creates 10 random of everything
            //data.Clear();
            //data.AddRentCardToUser(2);    // Adds a card to the user, as for now to user 1
            //data.AddBookIdToPersonRentCard(2, 10);    // The user with id number x gets the book with bookid x
            //data.AddBookToDatabase("Josefs bok", 6, 7);   // First string is the name of the book, second int is the user id who wrote the book, third int is another user id who wrote the book
            //data.MarkBookAsNotRented(10);     // If a book has a RentalCard connected to it, then this nulls it, clears the connection between the user and the book as not rented
            //data.AddUserToDatabase("Josef", "Hemme");
            //data.AddBookToDatabase("GOAT", 8);
            //data.AddAuthorToDatabase();
            //data.RemoveBookFromDatabase(5);
            //data.RemoveUserFromDatabase(5);
            //data.RemoveAuthorFromDatabase(5);
            WriteOutBooks(context1);
            WriteOutUser(context2);
            WriteOutAuthor(context3);
        }

        public static void WriteOutBooks(Context context1)
        {
            int counter = 0;


            foreach (var book in context1.Books)
            {

                string _bookInfo = $"\n(Book Id: {book.BookID}), Book title: {book.BookTitle}, Release date: {book.RentalYear}, (Borrowed: {book.Borrowed})\n";
                Console.WriteLine(_bookInfo);

                counter++;

                if (counter % 10 == 0)
                {

                    Console.WriteLine("--------------------------------------------------------------------------------\n");
                }
            }
        }


        public static void WriteOutUser(Context context2)
        {
            int counter = 0;


            foreach (var user in context2.Users)
            {

                string _userInfo = $"\n(User Id: {user.Id}), Firstname: {user.FirstName}, Lastname: {user.LastName}\n";
                Console.WriteLine(_userInfo);

                counter++;

                if (counter % 10 == 0)
                {

                    Console.WriteLine("--------------------------------------------------------------------------------\n");
                }
            }
        }


        public static void WriteOutAuthor(Context context3)
        {
            int counter = 0;


            foreach (var author in context3.Authors)
            {

                string _authorInfo = $"\n(Author Id: {author.Name}), : Name: {author.Name})\n";
                Console.WriteLine(_authorInfo);

                counter++;

                if (counter % 10 == 0)
                {

                    Console.WriteLine("\n");
                }
            }
        }

    }
}