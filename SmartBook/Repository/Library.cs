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

        // return Books.FirstOrDefault(book => book.ISBN == isbn);
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

        if(book == null)
            throw new ArgumentNullException(nameof(book), "Book cannot be null.");

        if(FindBookByISBN(book.ISBN) != null)
            throw new ArgumentException($"Book with an identical {book.ISBN} already exists in the library.");

        Books.Add(book);
    }

    /// <summary>
    /// Gets all books in the library sorted by title.
    /// </summary>
    /// <returns></returns>
    public List<LibraryBook> GetAllBooksSortedByTitle() {

        return (List<LibraryBook>)[.. books.OrderBy(b => b.Title)];
    }

    /// <summary>
    /// Gets all available books in the library sorted by title.
    /// </summary>
    /// <returns></returns>
    public List<LibraryBook> GetAllAvailableBooksSortedByTitle() {

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
        if(book == null)
            throw new ArgumentNullException(nameof(book), "Book cannot be null.");

        books.Remove(book);
    }

    /// <summary>
    /// Clears the library of all books.
    /// </summary>
    public void ClearLibrary() {
        if(Books.Count > 0) {
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
        if(string.IsNullOrWhiteSpace(isbn))
            throw new ArgumentNullException(nameof(isbn), "ISBN cannot be null or empty.");
        LibraryBook? book = FindBookByISBN(isbn);
        if(book == null)
            throw new ArgumentNullException(nameof(book), $"Could not find a book with the ISBN {isbn}");
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

        if(string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            throw new ArgumentNullException("Title and author cannot be null or empty.");

        findBook = books
               .Where(b => b.Title == title && b.Author == author).FirstOrDefault();

        if(findBook == null)
            return false;

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
        if(book == null)
            throw new ArgumentNullException(nameof(book), $"Could not find a book with the ISBN {isbn}");
        try {
            BorrowBook(book);
        } catch(InvalidOperationException ex) {
            throw new InvalidOperationException($"Could not borrow the book with the ISBN {isbn}", ex);
        }
    }

    /// <summary>
    /// Borrows a book from the library.
    /// </summary>
    /// <param name="book"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public void BorrowBook(LibraryBook book) {
        if(book == null)
            throw new ArgumentNullException(nameof(book), "Book cannot be null.");
        if(!book.BorrowLibraryBook())
            throw new InvalidOperationException("Book is not available for borrowing.");

        if(GetBook(book.Title, book.Author, out LibraryBook? findBook) && findBook != null) {
            findBook.BorrowLibraryBook();
        }
        else {
            throw new InvalidOperationException("That specified book could not be found in the library.");
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
        if(book == null)
            throw new ArgumentNullException(nameof(book), $"Could not find a book with the ISBN {isbn}");
        if(book.IsAvailable)
            throw new InvalidOperationException($"Book with ISBN {isbn} is already available and cannot be returned.");
        if(!book.ReturnLibraryBook()) {
            throw new InvalidOperationException($"Book with ISBN {isbn} is not available and can not be returned.");
        }
    }

    /// <summary>
    /// Gets a book from the library by its title or author.
    /// </summary>
    /// <param name="searchForBook"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public LibraryBook GetBookByTitleOrAuthor(string searchForBook) {
        if(string.IsNullOrWhiteSpace(searchForBook))
            throw new ArgumentNullException(nameof(searchForBook), "Search term cannot be null or empty.");
        LibraryBook? book = books
             .Where(b => b.Title == searchForBook || b.Author == searchForBook)
             .FirstOrDefault();
        if(book == null)
            throw new ArgumentNullException(nameof(book), $"Could not find a book with the title {searchForBook} of author {searchForBook}!");
        return book;
    }

    /// <summary>
    /// Gets a book from the library by its title or ISBN.
    /// </summary>
    /// <param name="searchForBook"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public LibraryBook GetBookByTitleOrISBN(string searchForBook) {

        if(string.IsNullOrWhiteSpace(searchForBook))
            throw new ArgumentNullException(nameof(searchForBook), "Search term cannot be null or empty.");
        LibraryBook? book = books
            .Where(b => b.Title == searchForBook || b.ISBN == searchForBook)
            .FirstOrDefault();
        if(book == null)
            throw new ArgumentNullException(nameof(book), $"Could not find a book with the title {searchForBook} or ISBN {searchForBook}!");

        return book;

    }
}
