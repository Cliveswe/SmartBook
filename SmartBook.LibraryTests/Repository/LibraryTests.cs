using SmartBook.Repository;


namespace SmartBook.LibraryTests.Repository;
public class LibraryTests
{
    private Library library = Library.Instance;
    private readonly DummyData dummyData = new();

    [Fact]
    public void CreateANewLibraryTest() {
        //Arrange
        library.ClearLibrary();

        //Act
        List<LibraryBook> books = dummyData.ListOfBooks;
        dummyData.PopulateLibrary(ref library);

        //Assert
        Assert.Equal(books.Count, library.NumberOfBooks);
    }

    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4", true, 1)]
    public void AddOneBookToTheLibraryTest(string title, string author, string category, string isbn, bool isAvaLiable, int expected) {
        //Arrange
        library.ClearLibrary();
        LibraryBook book = new(title, author, category, isbn, isAvaLiable);

        //Act
        library.AddBook(book);

        //Assert
        Assert.Equal(expected, library.NumberOfBooks);

    }

    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4")]
    public void SearchForABookByTitleOrAuthorTest(string title, string author, string category, string isbn) {
        //Arrange
        library.ClearLibrary();
        LibraryBook expectedBook = new(title, author, category, isbn, true);
        //Act
        dummyData.PopulateLibrary(ref library);
        library.GetBook(title, author, out LibraryBook? foundBook);
        //Assert
        Assert.Equal(expectedBook, foundBook);
    }


    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4", true, 1)]
    public void AddOneLibraryBookToTheLibraryTest(string title, string author, string category, string isbn, bool isAvailable, int expected) {
        //Arrange
        library.ClearLibrary();
        LibraryBook libraryBook = new(title, author, category, isbn, isAvailable);

        //Act
        library.AddBook(libraryBook);

        //Assert
        Assert.Equal(expected, library.NumberOfBooks);
        Assert.True(libraryBook.IsAvailable);
    }


    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4", true, 1)]
    public void AddOneLibraryBookToTheLibraryThenBorrowItTest(string title, string author, string category, string isbn, bool isAvailable, int expected) {
        //Arrange
        library.ClearLibrary();
        LibraryBook libraryBook = new(title, author, category, isbn, isAvailable);

        //Act
        library.AddBook(libraryBook);
        libraryBook.BorrowLibraryBook();

        //Assert
        Assert.Equal(expected, library.NumberOfBooks);
        Assert.False(libraryBook.IsAvailable);

    }

    [Fact]
    public void CreateANewLibraryShowAvailableBooksSortedByTitleTest() {
        //Arrange
        library.ClearLibrary();

        //Act
        dummyData.PopulateLibrary(ref library);
        //Create a list of available books
        IOrderedEnumerable<LibraryBook> listOfBooks = dummyData.ListOfBooks
            .Where(b => b.IsAvailable)
            .OrderBy(b => b.Title);
        var libraryResult = library.GetAllAvailableBooksSortedByTitle();

        //Assert
        Assert.Equal(listOfBooks, libraryResult);
    }

    [Fact]
    public void CreateANewLibraryShowAllBooksSortedByTitleTest() {
        //Arrange
        library.ClearLibrary();

        //Act
        dummyData.PopulateLibrary(ref library);
        IOrderedEnumerable<LibraryBook> listOfBooks = dummyData.ListOfBooks
            .OrderBy(b => b.Title);
        var libraryResult = library.GetAllBooksSortedByTitle();

        //Assert
        Assert.Equal(listOfBooks, libraryResult);
    }


    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4", true)]
    public void SearchForABookByAuthorAndTitleTest(string title, string author, string category, string isbn, bool isAvailable) {
        //Arrange
        library.ClearLibrary();
        LibraryBook expectedBook = new(title, author, category, isbn, isAvailable);

        //Act
        dummyData.PopulateLibrary(ref library);
        library.GetBook(title, author, out LibraryBook? foundBook);

        //Assert
        Assert.Equal(expectedBook, foundBook);

    }

    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4", true)]
    public void SearchForABookByAuthorAndTitleMarkItAsBorrowedTest(string title, string author, string category, string isbn, bool isAvailable) {
        //Arrange
        library.ClearLibrary();
        LibraryBook expectedBook = new(title, author, category, isbn, isAvailable);
        expectedBook.BorrowLibraryBook();

        //Act
        dummyData.PopulateLibrary(ref library);

        //Search for the book
        bool bookFound = library.GetBook(title, author, out LibraryBook? libraryBook);

        // Ensure the book is found before borrowing
        if(bookFound && libraryBook != null) {
            library.BorrowBook(libraryBook);
        }

        //Assert
        Assert.Equal(expectedBook, libraryBook);
        Assert.False(libraryBook?.IsAvailable);
    }

    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4", true)]
    public void RemoveBookByISBN(string title, string author, string category, string isbn, bool isAvailable) {

        //Arrange
        library.ClearLibrary();
        LibraryBook targetBook = new(title, author, category, isbn, isAvailable);

        //Act
        List<LibraryBook> books = dummyData.ListOfBooks;
        dummyData.PopulateLibrary(ref library);
        library.RemoveBookByISBN(isbn);

        //Assert
        Assert.Equal(books.Count - 1, library.NumberOfBooks);
        Assert.DoesNotContain(targetBook, library.Books);
    }

    [Theory]
    [InlineData("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4", true)]
    public void RemoveBookByTitle(string title, string author, string category, string isbn, bool isAvailable) {

        //Arrange
        library.ClearLibrary();
        LibraryBook targetBook = new(title, author, category, isbn, isAvailable);

        //Act
        List<LibraryBook> books = dummyData.ListOfBooks;
        dummyData.PopulateLibrary(ref library);
        library.RemoveBookByTitle(targetBook.Title);

        //Assert
        Assert.Equal(books.Count - 1, library.NumberOfBooks);
        Assert.DoesNotContain(targetBook, library.Books);
    }

    [Fact]
    public void AddTwoIdenticalBooksToTheLibraryTest() {
        //Arrange
        library.ClearLibrary();
        LibraryBook book1 = new("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4", true);
        LibraryBook book2 = new("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4", true);

        //Act
        library.AddBook(book1);

        //Assert
        Assert.Throws<ArgumentException>(() => library.AddBook(book2));
    }
}
