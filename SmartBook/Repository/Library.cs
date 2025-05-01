using SmartBook.Utilities;

namespace SmartBook.Repository;
/// <summary>
/// Singleton class that represents a library.
/// </summary>
public class Library
{
    #region Properties
    private List<LibraryBook> books;
    /// <summary>
    /// Gets or sets the list of books in the library.
    /// </summary>
    public List<LibraryBook> Books {
        get => books;
        private set => books = value;
    }

    private static readonly Library instance = new();

    /// <summary>
    /// Singleton instance of the Library class.
    /// </summary>
    public static Library Instance {
        get {
            if(instance != null)
                return instance;
            return new Library();
        }
    }

    /// <summary>
    /// Gets the number of books in the library.
    /// </summary>
    public int NumberOfBooks => books.Count;
    #endregion

    /// <summary>
    /// Private constructor to prevent instantiation from outside the class.
    /// </summary>
    private Library() => books = [];

    /// <summary>
    /// Finds a book in the library by its ISBN.
    /// </summary>
    /// <param name="isbn"></param>
    /// <returns></returns>
    private LibraryBook? FindBookByISBN(string isbn) {

        Log.Instance.LogMessage($"Searching for book with ISBN {isbn} in the library.");
        return books
             .Where(b => b.ISBN == isbn)
             .FirstOrDefault();
    }

    /// <summary>
    /// Adds a book to the library.
    /// </summary>
    /// <param name="book"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddBook(LibraryBook book) {

        if(book == null) {

            string message = "Book cannot be null.";
            Log.Instance.LogMessage(new ArgumentNullException(nameof(book), message));
            throw new ArgumentNullException(nameof(book), message);
        }
        if(FindBookByISBN(book.ISBN) != null) {

            string message = $"Book with an identical {book.ISBN} already exists in the library.";
            Log.Instance.LogMessage(new ArgumentException(message), message);
            throw new ArgumentException(message);
        }
        Log.Instance.LogMessage($"Adding book with ISBN {book.ISBN} to the library.");
        Books.Add(book);
    }

    /// <summary>
    /// Gets all books in the library sorted by title.
    /// </summary>
    /// <returns></returns>
    public List<LibraryBook> GetAllBooksSortedByTitle() {

        Log.Instance.LogMessage("Getting all books in the library sorted by title.");
        return (List<LibraryBook>)[.. books.OrderBy(b => b.Title)];
    }

    /// <summary>
    /// Gets all available books in the library sorted by title.
    /// </summary>
    /// <returns></returns>
    public List<LibraryBook> GetAllAvailableBooksSortedByTitle() {

        Log.Instance.LogMessage("Getting all available books in the library sorted by title.");
        return (List<LibraryBook>)[.. books
             .Where(b => b.IsAvailable)
             .OrderBy(b => b.Title)];

    }

    /// <summary>
    /// Removes a book from the library.
    /// </summary>
    /// <param name="book"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void RemoveBook(LibraryBook book) {

        if(book == null) {

            string message = "Book cannot be null.";
            Log.Instance.LogMessage(new ArgumentNullException(nameof(book), message));
            throw new ArgumentNullException(nameof(book), message);
        }

        Log.Instance.LogMessage($"Removing book with ISBN {book.ISBN} from the library.");
        books.Remove(book);
    }

    /// <summary>
    /// Clears the library of all books.
    /// </summary>
    public void ClearLibrary() {
        if(Books.Count > 0) {
            Log.Instance.LogMessage("Clearing the library of all books.");
            books.Clear();
        }
    }

    /// <summary>
    /// Gets a book from the library by its ISBN.
    /// </summary>
    /// <param name="isbn"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public LibraryBook GetBook(string isbn) {

        if(string.IsNullOrWhiteSpace(isbn)) {

            string message = "ISBN cannot be null or empty.";
            Log.Instance.LogMessage(new ArgumentNullException(nameof(isbn), message));
            throw new ArgumentNullException(nameof(isbn), message);
        }
        LibraryBook? book = FindBookByISBN(isbn);
        if(book == null) {

            string message = $"Could not find a book with the ISBN {isbn}";
            Log.Instance.LogMessage(new ArgumentNullException(nameof(book), message));
            throw new ArgumentNullException(nameof(book), message);
        }

        Log.Instance.LogMessage($"Getting book with ISBN {isbn} from the library.");
        return book;
    }

    /// <summary>
    /// Gets a book from the library by its title and author.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="author"></param>
    /// <param name="findBook"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public bool GetBook(string title, string author, out LibraryBook? findBook) {

        if(string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author)) {

            string message = "Title and author cannot be null or empty.";
            Log.Instance.LogMessage(new ArgumentNullException(nameof(title), message));
            throw new ArgumentNullException(message);
        }
        Log.Instance.LogMessage($"Searching for book with title {title} and author {author} in the library.");
        findBook = books
               .Where(b => b.Title == title && b.Author == author).FirstOrDefault();

        if(findBook == null) {

            Log.Instance.LogMessage($"Could not find a book with the title {title} and author {author}");
            return false;
        }

        Log.Instance.LogMessage($"Found book with title {title} and author {author} in the library.");
        return true;
    }

    /// <summary>
    /// Borrows a book from the library by its ISBN.
    /// </summary>
    /// <param name="isbn"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void BorrowBook(string isbn) {
        LibraryBook book = FindBookByISBN(isbn)!;
        if(book == null) {

            string message = $"Could not find a book with the ISBN {isbn}";
            Log.Instance.LogMessage(new ArgumentNullException(nameof(book), message));
            throw new ArgumentNullException(nameof(book), message);
        }
        try {

            Log.Instance.LogMessage($"Borrowing book with ISBN {isbn} from the library.");
            BorrowBook(book);
        } catch(InvalidOperationException ex) {

            string message = $"Book with ISBN {isbn} is not available for borrowing.";
            Log.Instance.LogMessage(ex, message);
            throw new InvalidOperationException(message, ex);
        }
    }

    /// <summary>
    /// Borrows a book from the library.
    /// </summary>
    /// <param name="book"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void BorrowBook(LibraryBook book) {
        if(book == null) {

            string message = "Book cannot be null.";
            Log.Instance.LogMessage(new ArgumentNullException(nameof(book), message));
            throw new ArgumentNullException(nameof(book), message);
        }
        if(!book.BorrowLibraryBook()) {

            string message = "Book is not available for borrowing.";
            Log.Instance.LogMessage(new InvalidOperationException(message), message);
            throw new InvalidOperationException(message);
        }
        if(GetBook(book.Title, book.Author, out LibraryBook? findBook) && findBook != null) {

            Log.Instance.LogMessage($"Borrowing book with title {book.Title} and author {book.Author} from the library.");
            findBook.BorrowLibraryBook();
        }
        else {

            string message = $"Could not find a book with the title {book.Title} and author {book.Author}";
            Log.Instance.LogMessage(new InvalidOperationException(message), message);
            throw new InvalidOperationException(message);
        }
    }

    /// <summary>
    /// Returns a borrowed book to the library by its ISBN.
    /// </summary>
    /// <param name="isbn"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void ReturnBorrowedBook(string isbn) {
        LibraryBook book = FindBookByISBN(isbn)!;
        if(book == null) {

            string message = $"Could not find a book with the ISBN {isbn}";
            Log.Instance.LogMessage(new ArgumentNullException(nameof(book), message));
            throw new ArgumentNullException(nameof(book), message);
        }
        if(book.IsAvailable) {

            string message = $"Book with ISBN {isbn} is already available and cannot be returned.";
            Log.Instance.LogMessage(new InvalidOperationException(message), message);
            throw new InvalidOperationException(message);
        }
        if(!book.ReturnLibraryBook()) {

            string message = $"Book with ISBN {isbn} is not available and cannot be returned.";
            Log.Instance.LogMessage(new InvalidOperationException(message), message);
            throw new InvalidOperationException(message);
        }

        Log.Instance.LogMessage($"Returning book {book.Title} by {book.Author} ISBN: {isbn} to the library.");
    }

    /// <summary>
    /// Gets a book from the library by its title or author.
    /// </summary>
    /// <param name="searchForBook"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public LibraryBook GetBookByTitleOrAuthor(string searchForBook) {
        if(string.IsNullOrWhiteSpace(searchForBook)) {

            string message = "Search term cannot be null or empty.";
            Log.Instance.LogMessage(new ArgumentNullException(nameof(searchForBook), message));
            throw new ArgumentNullException(nameof(searchForBook), message);
        }
        Log.Instance.LogMessage($"Searching for book with title {searchForBook} or author {searchForBook} in the library.");
        LibraryBook? book = books
             .Where(b => b.Title == searchForBook || b.Author == searchForBook)
             .FirstOrDefault();
        if(book == null) {

            string message = $"Could not find a book with the title {searchForBook} or author {searchForBook}!";
            Log.Instance.LogMessage(new ArgumentNullException(nameof(book), message));
            throw new ArgumentNullException(nameof(book), message);
        }
        Log.Instance.LogMessage($"Found book with title {searchForBook} or author {searchForBook} in the library.");
        return book;
    }

    /// <summary>
    /// Gets a book from the library by its title or ISBN.
    /// </summary>
    /// <param name="searchForBook"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public LibraryBook GetBookByTitleOrISBN(string searchForBook) {

        if(string.IsNullOrWhiteSpace(searchForBook)) {

            string message = "Search term cannot be null or empty.";
            Log.Instance.LogMessage(new ArgumentNullException(nameof(searchForBook), message));
            throw new ArgumentNullException(nameof(searchForBook), message);
        }
        Log.Instance.LogMessage($"Searching for book with title {searchForBook} or ISBN {searchForBook} in the library.");
        LibraryBook? book = books
            .Where(b => b.Title == searchForBook || b.ISBN == searchForBook)
            .FirstOrDefault();
        if(book == null) {

            string message = $"Could not find a book with the title {searchForBook} or ISBN {searchForBook}!";
            Log.Instance.LogMessage(new ArgumentNullException(nameof(book), message));
            throw new ArgumentNullException(nameof(book), message);
        }
        Log.Instance.LogMessage($"Found book with title {searchForBook} or ISBN {searchForBook} in the library.");
        return book;

    }
}
