using SmartBook.Repository;

namespace SmartBook.Application;

public class SmartBookApplication
{
    Library library = Library.Instance;

    /// <summary>
    /// Main method to start the application.
    /// </summary>
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
                case '2':
                BorrowABook();
                break;
                case '3':
                ReturnABook();
                break;
                case '4':
                SearchForABook();
                break;
                case '5':
                ListAllBooksInTheLibrary();
                break;
                case '6':
                LoadLibraryFromFile();
                break;
                case '7':
                SaveLibraryToFile();
                break;
                case '8':
                DeleteABookFromTheLibrary();
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

    /// <summary>
    /// Delete a book from the library. User is prompted to enter the title or ISBN of the book to delete as a search parameter.
    /// </summary>
    private void DeleteABookFromTheLibrary() {
        string searchForBook = string.Empty;
        LibraryBook book = null!;
        searchForBook = "Enter either the books title or ISBN to delete it".GetBookDetails();

        try {

            book = library.GetBookByTitleOrISBN(searchForBook);
        } catch(ArgumentNullException ex) {

            ex.Message.DisplayErrorMessage();
            PressAKey();
            return;
        }
        if(book == null) {

            "Book not found in the library.".DisplayWarningMessage();
            PressAKey();
            return;
        }

        book.ToString().DisplayInfoMessage();
        "Are you sure you want to delete the book (Y/N)?".DisplayWarningMessage();
        ConsoleKeyInfo key = Console.ReadKey();
        Console.WriteLine();
        if(key.Key == ConsoleKey.Y) {

            library.RemoveBook(book);
            "Book deleted.".DisplaySuccessMessage();
        }
        else if(key.Key == ConsoleKey.N) {

            "Book not deleted.".DisplayWarningMessage();
        }
        else {

            "Invalid choice.".DisplayErrorMessage();
        }
        PressAKey();
    }

    /// <summary>
    /// Search for a book in the library by title or author. User is prompted to enter the title or author of the book as a search parameter.
    /// </summary>
    private void SearchForABook() {
        string searchForBook = string.Empty;
        LibraryBook book = null!;

        searchForBook = "Search for a book by Title or Author".GetBookDetails();
        try {
            book = library.GetBookByTitleOrAuthor(searchForBook);
        } catch(ArgumentNullException ex) {
            ex.Message.DisplayErrorMessage();
            PressAKey();
            return;
        }
        book.ToString().DisplaySuccessMessage();
        PressAKey();
    }

    /// <summary>
    /// Return a book to the library. User is prompted to enter the ISBN of the book to return as a search parameter.
    /// </summary>
    private void ReturnABook() {
        string isbn;
        "Enter the ISBN of the book you want to borrow".GetBookISBN(out isbn);
        try {
            library.ReturnBorrowedBook(isbn);
        } catch(ArgumentNullException ex) {
            BorrowABookError(ex.Message);
            return;
        } catch(InvalidOperationException ex) {
            BorrowABookError(ex.Message);
            return;
        }
        LibraryBook book = library.GetBook(isbn)!;
        $"You have now returned the borrowed book:\nTitle: {book.Title}\nBy {book.Author}".DisplaySuccessMessage();
        PressAKey();
    }

    /// <summary>
    /// Borrow a book from the library. User is prompted to enter the ISBN of the book to borrow as a search parameter.
    /// </summary>
    private void BorrowABook() {
        if(library == null || library.NumberOfBooks == 0) {
            $"The library is empty.".DisplayWarningMessage();
            PressAKey();
            return;
        }
        string isbn;
        "Enter the ISBN of the book you want to borrow".GetBookISBN(out isbn);
        try {
            library.BorrowBook(isbn);
        } catch(ArgumentNullException ex) {
            BorrowABookError(ex.Message);
            return;
        } catch(InvalidOperationException ex) {
            BorrowABookError(ex.Message);
            return;
        }
        LibraryBook book = library.GetBook(isbn)!;
        $"You have successfully borrowed the book:\nTitle: {book.Title}\nBy {book.Author}".DisplaySuccessMessage();
        PressAKey();
    }

    /// <summary>
    /// Display an error message when borrowing a book fails.
    /// </summary>
    /// <param name="message"></param>
    private void BorrowABookError(string message) {
        message.DisplayErrorMessage();
        PressAKey();
    }

    /// <summary>
    /// List all books in the library. If the library is empty, display a warning message.
    /// </summary>
    private void ListAllBooksInTheLibrary() {
        if(library.NumberOfBooks != 0) {
            int bookCount = 0;
            Console.Clear();
            "List of books in the library:".DisplayStandardMessage();
            foreach(LibraryBook book in library.Books) {
                Console.WriteLine(book.ToString());
                "----------------------------------".DisplayInfoMessage();
                bookCount++;
            }
            $"Total number of books in the library: {library.NumberOfBooks}".DisplayStandardMessage();
            $"Total number of books displayed: {bookCount}".DisplayStandardMessage();
            PressAKey();
        }
        else {
            "The library is empty of books.".DisplayWarningMessage();
            PressAKey();
            return;
        }
    }

    /// <summary>
    /// Load the library from a file. If the file does not exist, display an error message.
    /// </summary>
    private void LoadLibraryFromFile() {
        JSONRepository jSONRepository = new JSONRepository();
        int existingBookCount = 0;
        int bookCount = 0;
        int libraryBookCount = library.NumberOfBooks;

        foreach(LibraryBook book in jSONRepository.LoadFromFile()) {
            try {
                library.AddBook(book);
                bookCount++;
            } catch(ArgumentNullException ex) {
                ex.Message.DisplayErrorMessage();
                PressAKey();
                return;
            } catch(ArgumentException ex) {
                ex.Message.DisplayWarningMessage();
                existingBookCount++;
            }
        }

        if(library.NumberOfBooks == 0) {
            "There are no books in the library and non where added!".DisplayWarningMessage();
            PressAKey();
            return;
        }
        if(bookCount == 0) {
            "No books where added to the library!".DisplayInfoMessage();
            PressAKey();
            return;
        }
        if(bookCount > 0) {
            $"Number of books not added to the library: {existingBookCount}".DisplayWarningMessage();
            $"Number of books added to the library: {bookCount}".DisplaySuccessMessage();
            $"Loaded {(Math.Abs(libraryBookCount - library.NumberOfBooks))} books from the file.".DisplaySuccessMessage();
            PressAKey();
        }
    }

    /// <summary>
    /// Save the library to a file. If the library is empty, display a warning message.
    /// </summary>
    private void SaveLibraryToFile() {
        JSONRepository jSONRepository = new JSONRepository();

        if(library.NumberOfBooks == 0) {
            "Cannot save an empty library to a file.".DisplayWarningMessage();
            PressAKey();
            return;
        }

        jSONRepository.SaveToFile(library.Books);
        $"Library saved to file: {JSONRepository.PathToFile}".DisplaySuccessMessage();
        PressAKey();
    }

    /// <summary>
    /// Add a new book to the library. User is prompted to enter the title, author, category, and ISBN of the book.
    /// </summary>
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

        LibraryBook book = new LibraryBook(title, author, category, isbn, true);
        try {

            library.AddBook(book);
        } catch(ArgumentNullException ex) {

            ex.Message.DisplayErrorMessage();
            PressAKey();
            return;
        } catch(ArgumentException ex) {

            ex.Message.DisplayWarningMessage();
            PressAKey();
            return;
        }

        $"Book {title} by {author} added to the library.".DisplaySuccessMessage();
        PressAKey();
    }

    /// <summary>
    /// Display a message prompting the user to press any key to continue.
    /// </summary>
    private void PressAKey() {
        "Press any key to continue...".GetAnyKey();
    }

    /// <summary>
    /// Display the main menu of the application.
    /// </summary>
    private void DisplayMainMenu() {

        Console.Clear();
        "Welcome to SmartBook!".DisplayStandardMessage();
        "1. Add a new book.".DisplayStandardMessage();
        "2. Borrow a book.".DisplayStandardMessage();
        "3. Return a book.".DisplayStandardMessage();
        "4. Search for a book.".DisplayStandardMessage();
        "5. List all books.".DisplayStandardMessage();
        "6. Load library from file.".DisplayStandardMessage();
        "7. Save library to file.".DisplayStandardMessage();
        "8. Delete a book from the library.".DisplayStandardMessage();
        "0. Exit".DisplayStandardMessage();
    }
}