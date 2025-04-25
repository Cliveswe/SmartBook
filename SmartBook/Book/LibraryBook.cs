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
}
