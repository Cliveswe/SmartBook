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

    public IEnumerable<LibraryBook> GetBooksSortedByTitle() {

        IOrderedEnumerable<LibraryBook> avaliableLibraryBooks = books
            .Where(b => b.Available)
            .OrderBy(b => b.Title);
        var sortedBooks = avaliableLibraryBooks;

        return sortedBooks;
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
}
