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



