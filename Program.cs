using SuryoyoBibliotek.Data;

namespace SuryoyoBibliotek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataAccess data = new DataAccess();

            //data.CreateFiller(); // Creates 10 random books of random titles
            //data.Clear(); // Clears everything
            //data.AddLoanCardToPerson(2); // Adds a card to the user, as for now to user 1
            //data.AddBookIdToPersonLoanCard(2, 10); // The user with id number 1 gets the book with bookid 5
            //data.AddBookToDatabase("Josefs bok", 6, 7); // First string is the name of the book, second int is the user id who wrote the book, third int is another user id who wrote the book
            //data.MarkBookAsNotRented(10); // If a book has a RentalCard connected to it, then this nulls it, clears the connection between the user and the book as not rented
            //data.RemoveBookFromDatabase(2);
            //data.RemoveUserFromDatabase(2);
            //data.RemoveAuthorFromDatabase(3);
        }
    }
}