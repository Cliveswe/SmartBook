using SmartBook.Tests;

namespace SmartBook;

public class LibraryBook : Book
{
    private bool IsAvailable {
        get;
        set;
    }

    public bool OnLoan => !IsAvailable;
    public bool Available => IsAvailable;

    public LibraryBook(string title, string author, string category, string isbn)
        : base(title, author, category, isbn) {

        IsAvailable = true;
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

    public override string ToString() {
        return $"{base.ToString()}Available: {(Available ? "Yes" : "No")}";
    }
}
