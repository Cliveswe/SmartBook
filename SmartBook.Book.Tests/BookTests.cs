using SmartBook.Tests;

namespace SmartBook;

public class BookTests
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
