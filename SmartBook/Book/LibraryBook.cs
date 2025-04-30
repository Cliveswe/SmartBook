using SmartBook.Tests;

namespace SmartBook;

public class LibraryBook : Book
{
    /// <summary>
    /// Gets or sets the availability of the library book.
    /// </summary>
    public bool IsAvailable {
        get;
        private set;
    }

    /// <summary>
    /// Constructor for the LibraryBook class.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="author"></param>
    /// <param name="category"></param>
    /// <param name="isbn"></param>
    /// <param name="isAvailable"></param>
    public LibraryBook(string title, string author, string category, string isbn, bool isAvailable)
        : base(title, author, category, isbn) {
        IsAvailable = isAvailable;
    }

    /// <summary>
    /// Borrowing a library book.
    /// </summary>
    /// <returns> false if we could not borrow the library book otherwise true</returns>
    public bool BorrowLibraryBook() {
        if(IsAvailable) {
            IsAvailable = false;
            return true;
        }
        else {
            return false;
        }
    }

    /// <summary>
    /// Returning a library book.
    /// </summary>
    /// <returns></returns>
    public bool ReturnLibraryBook() {
        if(!IsAvailable) {
            IsAvailable = true;
            return true;
        }
        else {
            return false;
        }
    }

    /// <summary>
    /// Returning a library book.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>False if the obj is not equal to this instance.</returns>
    public override bool Equals(object? obj) {
        if(obj is LibraryBook other) {
            return Title == other.Title &&
                   Author == other.Author &&
                   Category == other.Category &&
                   ISBN == other.ISBN;
        }
        return false;
    }

    /// <summary>
    /// Overrides the GetHashCode method to provide a hash code for the book.
    /// </summary>
    /// <returns>HashCode of Title, Author, Category and ISBN</returns>
    public override int GetHashCode() {
        return HashCode.Combine(Title, Author, Category, ISBN);
    }

    /// <summary>
    /// Overrides the ToString method to provide a string representation of the books availability.
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
        return $"{base.ToString()}Available: {(IsAvailable ? "Yes" : "No")}";
    }
}
