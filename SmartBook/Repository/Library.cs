using SmartBook.Tests;
using System.Collections;

namespace SmartBook.Repository;
/// <summary>
/// Singleton class that represents a library.
/// </summary>
public class Library : IEnumerable<Book>
{

    private List<Book> books;
    /// <summary>
    /// Gets or sets the list of books in the library.
    /// </summary>
    public List<Book> Books {
        get => books;
        private set => books = value;
    }

    private static Library instance;
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
            return Instance.Books.Count;
        }
    }

    /// <summary>
    /// Private constructor to prevent instantiation from outside the class.
    /// </summary>
    private Library() {
        books = new List<Book>();

    }

    /// <summary>
    /// Adds a book to the library.
    /// </summary>
    /// <param name="book"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddBook(Book book) {

        if(book == null)
            throw new ArgumentNullException(nameof(book), "Book cannot be null.");

        books.Add(book);
    }

    /// <summary>
    /// Removes a book from the library.
    /// </summary>
    /// <param name="book"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void RemoveBook(Book book) {
        if(book == null)
            throw new ArgumentNullException(nameof(book), "Book cannot be null.");

        books.Remove(book);
    }

    public IEnumerator<Book> GetEnumerator() {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}
