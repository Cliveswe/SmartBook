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

    private static Library instance = new();
    /// <summary>
    /// Singleton instance of the Library class.
    /// </summary>
    public static Library Instance {
        get {

            if(instance == null)
                instance = new Library();

            return instance;
        }
    }

    /// <summary>
    /// Gets the number of books in the library.
    /// </summary>
    public int NumberOfBooks {
        get {
            return books.Count;
        }
    }
    #endregion

    /// <summary>
    /// Private constructor to prevent instantiation from outside the class.
    /// </summary>
    private Library() {
        books = new List<LibraryBook>();

    }

    /// <summary>
    /// Adds a book to the library.
    /// </summary>
    /// <param name="book"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddBook(LibraryBook book) {

        if(book == null)
            throw new ArgumentNullException(nameof(book), "Book cannot be null.");

        books.Add(book);
    }

    public List<LibraryBook> GetAllBooksSortedByTitle() {

        return (List<LibraryBook>)books
             .OrderBy(b => b.Title).ToList();
    }

    public List<LibraryBook> GetAllAvailableBooksSortedByTitle() {

        return (List<LibraryBook>)books
             .Where(b => b.Available)
             .OrderBy(b => b.Title).ToList();

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

    public void ClearLibrary() {
        if(Books.Count > 0) {
            books.Clear();
        }
    }

    public bool GetBook(string title, string author, out LibraryBook? findBook) {

        if(string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            throw new ArgumentNullException("Title and author cannot be null or empty.");

        findBook = books
               .Where(b => b.Title == title && b.Author == author).FirstOrDefault();

        if(findBook == null)
            return false;

        return true;

    }

    public void BorrowBook(LibraryBook book) {
        if(book == null)
            throw new ArgumentNullException(nameof(book), "Book cannot be null.");
        if(!book.BorrowLibraryBook())
            throw new InvalidOperationException("Book is not available for borrowing.");

        GetBook(book.Title, book.Author, out LibraryBook? findBook);
        findBook.BorrowLibraryBook();

    }

}
