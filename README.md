# 📚 SmartBook

**SmartBook** is a console-based library management system built in C# using .NET 9. The application allows users to manage a collection of books through a menu-driven interface.

## 🚀 Features

- 📖 Add a book (title, author, ISBN, category)
- ❌ Remove a book (by title or ISBN)
- 📋 List all books (sorted, e.g., by title) using LINQ
- 🔍 Search for books (by title or author) using LINQ
- 🔄 Mark books as **borrowed** or **available**
- 💾 Save and load the library to/from a file (JSON format)

## 🛠️ Built With

- C#
- .NET 9
- Visual Studio 2022 or later

## ▶️ How to Run the Project
Open in Visual Studio
Double-click the .sln file in the project folder.
Press F5 or click Start to run the application.
Alternatively, use the .NET CLI:
dotnet build
dotnet run

💡 Usage

The application displays a menu with options. Use the number keys to select an action. Example:
1. Add a new book.
2. Borrow a book.
3. Return a book.
4. Search for a book.
5. List all books.
6. Load library from file.
7. Save library to file.
8. Delete a book from the library."
0. Exit
  
2. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/smartbook.git
   cd smartbook


> 💡 **Note:** 
The project contains some useful xUnit tests. Depending on how you use the application or use the tests you may have
to repeat the repository test at least 2 times. This is due to the application or the tests generating a json file
called library.json.

✅ Unit Tests: BookTests

📁 Namespace: SmartBook.LibraryTests.Repository
🔍 Purpose: Validates the construction and behavior of the Book class in the SmartBook library.
🔬 Tests Included:
  
    ✅ CreateANewBookTest()

    Verifies correct creation of a Book object with valid input.
    Asserts that the ToString() method formats the book details properly.
    
    ❌ CreateANewBookWithMissingTitleTest(...)

    Tests multiple invalid book creation scenarios using [Theory] and [InlineData].
    Validates that appropriate ArgumentException messages are thrown when:
    Title, author, category, or ISBN is empty.
    ISBN contains invalid characters or has incorrect length.
    
    📘 CreateANewBookThatIsValidISBN10(...)

    Confirms that valid ISBN-10 formatted books are accepted and correctly represented.

    📗 CreateANewBookThatIsValidISBN13(...)
    
    Confirms that valid ISBN-13 formatted books are accepted and correctly represented.

    💡 Note: These tests help ensure input validation and correct object formatting for the Book class.

🧪 Unit Tests: LibraryTests

📁 Namespace: SmartBook.LibraryTests.Repository
🔍 Purpose: Validates the behavior and integrity of the Library singleton and book-related operations in the SmartBook repository.
🔬 Tests Included:

    ✅ CreateANewLibraryTest()
    Verifies that the library initializes correctly and stores the expected number of books.

    📚 AddOneBookToTheLibraryTest(...)
    Confirms that adding a single book increases the library’s book count appropriately.

    🔍 SearchForABookByTitleOrAuthorTest(...)
    Tests the search functionality by title or author and confirms correct book retrieval.

    📗 AddOneLibraryBookToTheLibraryTest(...)
    Adds a library book and checks both availability status and book count.

    🔄 AddOneLibraryBookToTheLibraryThenBorrowItTest(...)
    Adds a book and simulates borrowing it; confirms availability status changes to false.

    📖 CreateANewLibraryShowAvailableBooksSortedByTitleTest()
    Retrieves all available books and confirms they are sorted correctly by title using LINQ.

    📖 CreateANewLibraryShowAllBooksSortedByTitleTest()
    Retrieves all books (regardless of status) and ensures correct sorting by title.

    🔍 SearchForABookByAuthorAndTitleTest(...)
    Validates combined search by both author and title.

    🔄 SearchForABookByAuthorAndTitleMarkItAsBorrowedTest(...)
    Searches and borrows a book, confirming it is found and marked as unavailable.

    ⚠️ AddTwoIdenticalBooksToTheLibraryTest()
    Ensures that trying to add a duplicate book (same ISBN) throws an ArgumentException.

    💡 Note: These tests ensure the core Library logic behaves predictably, covering scenarios from adding and searching to borrowing and duplication checks.

💾 Unit Tests: RepositoryTests

📁 Namespace: SmartBook.LibraryTests.Repository
🔍 Purpose: Ensures data persistence and file handling in the JSONRepository class works correctly for saving and loading the library.
🔬 Tests Included:

    ✅ CheckThatTheLibraryIsSaveAndLoadedTest()

        Populates the library with dummy data.

        Saves the list of available books to a JSON file.

        Loads the data back and asserts that the saved and loaded data match.

        Cleans up the generated file after testing.

    ⚠️ CheckThatSaveToFileNullTest()

        Attempts to save a null book list.

        Asserts that an ArgumentNullException is thrown with the expected message.

        Ensures any test-generated files are deleted.

    💡 Note: These tests verify that the JSON-based file repository handles both valid and invalid save operations correctly.

    📚 Support Data: DummyData

📁 Namespace: SmartBook.Repository
🔍 Purpose: Provides a predefined list of sample LibraryBook objects for testing and populating the Library during development or unit tests.
📘 Key Properties & Methods:

    List<LibraryBook> ListOfBooks
    A collection of 10 sample LibraryBook instances across various genres, authors, and ISBNs. All books are initially marked as available.

    Example Entries Include:

        "Lorem Ipsum Chronicles" by Dolor Sit – Fiction

        "Excepteur Sint Biography" by Cupidatat Non – Biography

        "Labore et Dolore: A Mystery" by Magna Aliqua – Mystery

    void PopulateLibrary(ref Library library)
    Populates the given Library instance with the full set of predefined books by calling AddBook() on each item in ListOfBooks.

    💡 Note: This class is primarily used in unit tests and setup routines to ensure consistent, repeatable test data.
