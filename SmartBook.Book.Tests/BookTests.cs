using SmartBook.Tests;

namespace SmartBook;

public class BookTests
{
    [Fact]
    public void CreateANewBookTest() {

        //Arrange

        //Act
        //    var caughtExecption = Assert.Throws<ArgumentException>(() => vehicle.Brand = maxLengthText);
        Book book = new("Laborum Et Dolore", "Fugiat N. Nulla", "Poetry", "9788901234567");
        //Assert
        Assert.Equal($"Title: Laborum Et Dolore{Environment.NewLine}" +
            $"Author: Fugiat N. Nulla{Environment.NewLine}" +
            $"Category: Poetry{Environment.NewLine}" +
            $"ISBN: 9788901234567{Environment.NewLine}", book.ToString());
    }

    [Theory]
    [InlineData("", "Fugiat N. Nulla", "Poetry", "9788901234567", "Title cannot be empty!")]
    [InlineData("Laborum Et Dolore", "", "Poetry", "9788901234567", "Author cannot be empty!")]
    [InlineData("Laborum Et Dolore", "Fugiat N. Nulla", "", "9788901234567", "Category cannot be empty!")]
    [InlineData("Laborum Et Dolore", "Fugiat N. Nulla", "Poetry", "", "ISBN cannot be empty!")]
    [InlineData("Laborum Et Dolore", "Fugiat N. Nulla", "Poetry", "978890123456X", "ISBN must not contain letters, except possibly an 'X' at the end for ISBN-10.")]
    [InlineData("Laborum Et Dolore", "Fugiat N. Nulla", "Poetry", "97889012345666", "ISBN must be between 10 and 13 characters long!")]
    [InlineData(" ", "Fugiat N. Nulla", "Poetry", "9788901234567", "Title cannot be empty!")]
    public void CreateANewBookWithMissingTitleTest(string title, string author, string category, string isbn, string expected) {

        //Arrange

        //Act
        var caughtExecption = Assert.Throws<ArgumentException>(() => new Book(title, author, category, isbn));

        //Assert
        Assert.Equal(expected, caughtExecption.Message);
    }

    [Theory]
    [InlineData("Laborum Et Dolore", "Fugiat N. Nulla", "Poetry", "978-8-901-23456-7")]
    public void CreateANewBookThatIsValidISBN10(string title, string author, string category, string isbn) {

        //Arrange
        //Act
        Book book = new(title, author, category, isbn);
        //Assert
        Assert.Equal($"Title: Laborum Et Dolore{Environment.NewLine}" +
           $"Author: Fugiat N. Nulla{Environment.NewLine}" +
           $"Category: Poetry{Environment.NewLine}" +
           $"ISBN: 978-8-901-23456-7{Environment.NewLine}", book.ToString());

    }

    [Theory]
    [InlineData("Laborum Et Dolore", "Fugiat N. Nulla", "Poetry", "0-8044-2957-X")]
    public void CreateANewBookThatIsValidISBN13(string title, string author, string category, string isbn) {

        //Arrange
        //Act
        Book book = new(title, author, category, isbn);
        //Assert
        Assert.Equal($"Title: Laborum Et Dolore{Environment.NewLine}" +
           $"Author: Fugiat N. Nulla{Environment.NewLine}" +
           $"Category: Poetry{Environment.NewLine}" +
           $"ISBN: 0-8044-2957-X{Environment.NewLine}", book.ToString());

    }
}
