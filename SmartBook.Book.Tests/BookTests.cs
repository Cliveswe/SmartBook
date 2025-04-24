using SmartBook.Tests;

namespace SmartBook;

public class BookTests
{
    /*
    Title: Dolor Sit Amet
    Author: Lorem T. Ipsum
    Category: Fiction
    ISBN: 9781234567890

    Title: Consectetur Adipiscing Elit
    Author: Aenean V. Vitae
    Category: SelfHelp
    ISBN: 9780987654321

    Title: Ut Enim Ad Minim
    Author: Cillum D. Tempor
    Category: Science Fiction
    ISBN: 9781112233445

    Title: Exercitation Ullamco Laboris
    Author: Magna A. Aliqua
    Category: Thriller
    ISBN: 9783456789012

    Title: Nisi Ut Aliquip
    Author: Commodo C. Consequat
    Category: Romance
    ISBN: 9782123456789

    Title: Duis Aute Irure Dolor
    Author: Reprehenderit I. Voluptate
    Category: Mystery
    ISBN: 9784567890123

    Title: Velit Esse Cillum
    Author: Eu F. Fugiat
    Category: Historical Fiction
    ISBN: 9785678901234

    Title: Cupidatat Non Proident
    Author: Sunt I. Culpa
    Category: Biography
    ISBN: 9786789012345

    Title: Officia Deserunt Mollit
    Author: Anim E. Est
    Category: Horror
    ISBN: 9787890123456

    Title: Laborum Et Dolore
    Author: Fugiat N. Nulla
    Category: Poetry
    ISBN: 9788901234567
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
}
