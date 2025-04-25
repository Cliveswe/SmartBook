using SmartBook.Repository;

namespace SmartBook;

public class LibraryTests
{

    /*
Title: Lorem Ipsum Chronicles
Author: Dolor Sit
Category: Fiction
ISBN: 978-0-123456-47-2

Title: Adventures of Amet Elit
Author: Amet Elit
Category: Fantasy
ISBN: 978-1-234567-89-7

Title: Sed Do Temporalis
Author: Incididunt Ut
Category: Science Fiction
ISBN: 978-0-321-56789-0

Title: Labore et Dolore: A Mystery
Author: Magna Aliqua
Category: Mystery
ISBN: 978-3-16-148410-0

Title: Ut Enim Veniam
Author: Quis Nostrud
Category: Romance
ISBN: 978-0-262-13472-9

Title: Exercitationem: The Escape
Author: Laboris Nisi
Category: Thriller
ISBN: 978-1-4028-9462-6

Title: Aliquip Commodo Quest
Author: Duis Consequat
Category: Adventure
ISBN: 978-0-395-19395-8

Title: Reprehenderit Voluptate: A Tale
Author: Velit Esse
Category: Historical Fiction
ISBN: 978-0-7432-7356-5

Title: Cillum Dolore Eu
Author: Fugiat Nulla
Category: Horror
ISBN: 978-0-7432-7357-2

Title: Excepteur Sint Biography
Author: Cupidatat Non
Category: Biography
ISBN: 978-1-56619-909-4
    **/

    public List<LibraryBook> ListOfBooks() {
        List<LibraryBook> books = new();
        books.Add(new("Lorem Ipsum Chronicles", "Dolor Sit", "Fiction", "978-0-123456-47-2"));
        books.Add(new("Adventures of Amet Elit", "Amet Elit", "Fantasy", "978-1-234567-89-7"));
        books.Add(new("Sed Do Temporalis", "Incididunt Ut", "Science Fiction", "978-0-321-56789-0"));
        books.Add(new("Sed Do Temporalis", "Incididunt Ut", "Science Fiction", "978-0-321-56789-0"));
        books.Add(new("Labore et Dolore: A Mystery", "Magna Aliqua", "Mystery", "978-3-16-148410-0"));
        books.Add(new("Ut Enim Veniam", "Quis Nostrud", "Romance", "978-0-262-13472-9"));
        books.Add(new("Exercitationem: The Escape", "Laboris Nisi", "Thriller", "978-1-4028-9462-6"));
        books.Add(new("Aliquip Commodo Quest", "Duis Consequat", "Adventure", "978-0-395-19395-8"));
        books.Add(new("Reprehenderit Voluptate: A Tale", "Velit Esse", "Historical Fiction", "978-0-7432-7356-5"));
        books.Add(new("Cillum Dolore Eu", "Fugiat Nulla", "Horror", "978-0-7432-7357-2"));
        books.Add(new("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4"));

        return books;
    }

    [Fact]
    public void CreateANewLibraryTest() {
        //Arrange
        Library library = Library.Instance;
        library.ClearLibrary();
        //Act
        List<LibraryBook> books = ListOfBooks();
        foreach(var book in books) {
            library.AddBook(book);
        }

        //Assert
        Assert.Equal(books.Count, library.NumberOfBooks);
    }

    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4", 1)]
    public void AddOneBookToTheLibraryTest(string title, string author, string category, string isbn, int expected) {
        //Arrange
        Library library = Library.Instance;
        library.ClearLibrary();
        LibraryBook book = new(title, author, category, isbn);

        //Act
        library.AddBook(book);


        //Assert
        Assert.Equal(expected, library.NumberOfBooks);

    }


    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4", 1)]
    public void AddOneLibraryBookToTheLibraryTest(string title, string author, string category, string isbn, int expected) {
        //Arrange
        Library library = Library.Instance;
        library.ClearLibrary();
        LibraryBook libraryBook = new(title, author, category, isbn);

        //Act
        library.AddBook(libraryBook);

        //Assert
        Assert.Equal(expected, library.NumberOfBooks);
        Assert.False(libraryBook.OnLoan);
        Assert.True(libraryBook.Available);
    }


    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4", 1)]
    public void AddOneLibraryBookToTheLibraryThenBorrowItTest(string title, string author, string category, string isbn, int expected) {
        //Arrange
        Library library = Library.Instance;
        library.ClearLibrary();
        LibraryBook libraryBook = new(title, author, category, isbn);

        //Act
        library.AddBook(libraryBook);
        libraryBook.BorrowLibraryBook();

        //Assert
        Assert.Equal(expected, library.NumberOfBooks);
        Assert.True(libraryBook.OnLoan);
        Assert.False(libraryBook.Available);

    }

    [Fact]
    public void CreateANewLibraryShowAvailableBooksSortedByTitleTest() {
        //Arrange
        Library library = Library.Instance;
        library.ClearLibrary();

        //Act
        List<LibraryBook> books = ListOfBooks();
        foreach(var book in books) {
            library.AddBook(book);
        }
        IOrderedEnumerable<LibraryBook> listOfBooks = ListOfBooks()
            .Where(b => b.Available)
            .OrderBy(b => b.Title);
        var libraryResult = library.GetAllAvailableBooksSortedByTitle();

        //Assert
        Assert.Equal(listOfBooks, libraryResult);
    }

    [Fact]
    public void CreateANewLibraryShowAllBooksSortedByTitleTest() {
        //Arrange
        Library library = Library.Instance;
        library.ClearLibrary();

        //Act
        List<LibraryBook> books = ListOfBooks();
        foreach(var book in books) {
            library.AddBook(book);
        }
        IOrderedEnumerable<LibraryBook> listOfBooks = ListOfBooks()
            .OrderBy(b => b.Title);
        var libraryResult = library.GetAllBooksSortedByTitle();

        //Assert
        Assert.Equal(listOfBooks, libraryResult);
    }


    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4")]
    public void SearchForABookByAuthorAndTitleTest(string title, string author, string category, string isbn) {
        //Arrange
        Library library = Library.Instance;
        library.ClearLibrary();
        LibraryBook libraryBook = new(title, author, category, isbn);
        LibraryBook expectedBook = new(title, author, category, isbn);

        //Act
        List<LibraryBook> books = ListOfBooks();
        foreach(var book in books) {
            library.AddBook(book);
        }

        LibraryBook? foundBook;
        bool result = library.GetBook(title, author, out foundBook);

        //Assert
        Assert.Equal(expectedBook, foundBook);

    }

    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4")]
    public void SearchForABookByAuthorAndTitleMarkItAsBorrowedTest(string title, string author, string category, string isbn) {
        //Arrange
        Library library = Library.Instance;
        library.ClearLibrary();
        LibraryBook libraryBook = new(title, author, category, isbn);
        LibraryBook expectedBook = new(title, author, category, isbn);
        //Mark the book as borrowed
        expectedBook.BorrowLibraryBook();

        //Act
        List<LibraryBook> books = ListOfBooks();
        foreach(var book in books) {
            library.AddBook(book);
        }

        //Search for the book
        library.BorrowBook(libraryBook);

        //Assert
        Assert.Equal(expectedBook, libraryBook);
        Assert.True(libraryBook?.OnLoan);
        Assert.True(expectedBook?.OnLoan);

    }

    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4")]
    public void RemoveBookByISBN(string title, string author, string category, string isbn) {

        //Arrange
        Library library = Library.Instance;
        library.ClearLibrary();
        LibraryBook targetBook = new(title, author, category, isbn);

        //Act
        List<LibraryBook> books = ListOfBooks();
        foreach(var book in books) {
            library.AddBook(book);
        }
        library.RemoveBookByISBN(isbn);

        //Assert
        Assert.Equal(books.Count - 1, library.NumberOfBooks);
        Assert.False(library.Books.Contains(targetBook));
    }

    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4")]
    public void RemoveBookByTitle(string title, string author, string category, string isbn) {

        //Arrange
        Library library = Library.Instance;
        library.ClearLibrary();
        LibraryBook targetBook = new(title, author, category, isbn);

        //Act
        List<LibraryBook> books = ListOfBooks();
        foreach(var book in books) {
            library.AddBook(book);
        }
        library.RemoveBookByTitle(title);

        //Assert
        Assert.Equal(books.Count - 1, library.NumberOfBooks);
        Assert.False(library.Books.Contains(targetBook));
    }
}
