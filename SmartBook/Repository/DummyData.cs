namespace SmartBook.Repository;
public class DummyData
{
    public List<LibraryBook> ListOfBooks {
        get {
            List<LibraryBook> books =
            [
                new("Lorem Ipsum Chronicles", "Dolor Sit", "Fiction", "978-0-123456-47-2"),
            new("Adventures of Amet Elit", "Amet Elit", "Fantasy", "978-1-234567-89-7"),
            new("Sed Do Temporalis", "Incididunt Ut", "Science Fiction", "978-0-321-56789-0"),
            new("Labore et Dolore: A Mystery", "Magna Aliqua", "Mystery", "978-3-16-148410-0"),
            new("Ut Enim Veniam", "Quis Nostrud", "Romance", "978-0-262-13472-9"),
            new("Exercitationem: The Escape", "Laboris Nisi", "Thriller", "978-1-4028-9462-6"),
            new("Aliquip Commodo Quest", "Duis Consequat", "Adventure", "978-0-395-19395-8"),
            new("Reprehenderit Voluptate: A Tale", "Velit Esse", "Historical Fiction", "978-0-7432-7356-5"),
            new("Cillum Dolore Eu", "Fugiat Nulla", "Horror", "978-0-7432-7357-2"),
            new("Excepteur Sint Biography", "Cupidatat Non", "Biography", "978-1-56619-909-4"),
        ];

            return books;
        }
    }

    public void PopulateLibrary(ref Library library) {
        //Populate the library with books
        List<LibraryBook> books = ListOfBooks;
        //Add the books to the library
        foreach(var book in books) {
            library.AddBook(book);
        }

    }
}
