
using SmartBook.Repository;

namespace SmartBook;

public class SmartBookApplication
{
    Library library = Library.Instance;
    public void Start() {
        char input = ' ';

        while(true) {

            DisplayMainMenu();
            try {
                Console.Write("Enter a menu choice: ");
                input = Console.ReadLine()![0];
            } catch(IndexOutOfRangeException) {

                Console.Clear();
                Console.WriteLine("Please enter some input!");
            }


            switch(input) {
                case '1':
                AddNewBook();
                break;
                case '0':
                Environment.Exit(0);
                break;
                default:
                Console.WriteLine("Choice not recognized please choose from the menu.");
                break;
            }

        }
    }

    private void AddNewBook() {
        string title = string.Empty;
        string author = string.Empty;
        string isbn = string.Empty;
        string category = string.Empty;

        Console.WriteLine("Enter details of the book.");
        title = "Title".GetBookDetails();
        author = "Author".GetBookDetails();
        category = "Category".GetBookDetails();
        isbn = "ISBN".GetBookDetails();

        LibraryBook book = new LibraryBook(title, author, category, isbn);
        try {
            library.AddBook(book);
        } catch(ArgumentNullException ex) {
            ex.Message.DisplayErrorMessage();
            return;
        } catch(ArgumentException ex) {
            ex.Message.DisplayWarningMessage();
            return;
        }
        $"Book {title} by {author} added to the library.".DisplaySuccessMessage();
        "Press any key to continue...".DisplayStandardMessage();
        Console.ReadKey();
    }

    private void DisplayMainMenu() {

        Console.Clear();
        "Welcome to SmartBook!".DisplayStandardMessage();
        "1. Add a new book".DisplayStandardMessage();
        "2. Borrow a book".DisplayStandardMessage();
        "3. Return a book".DisplayStandardMessage();
        "4. Search for a book".DisplayStandardMessage();
        "5. List all books".DisplayStandardMessage();
        "0. Exit".DisplayStandardMessage();
    }
}