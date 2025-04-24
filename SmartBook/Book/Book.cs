using System.Text;

namespace SmartBook.Tests;

public class Book
{
    private string title;

    /// <summary>
    /// Gets or sets the title of the book.
    /// </summary>
    public string Title {
        get => title;
        private set => title = string.IsNullOrWhiteSpace(value.Trim()) ? throw new ArgumentException("Title cannot be empty!") : value;
    }

    private string author;
    /// <summary>
    /// Gets or sets the author of the book.
    /// </summary>
    public string Author {
        get => author;
        private set => author = string.IsNullOrWhiteSpace(value.Trim()) ? throw new ArgumentException("Author cannot be empty!") : value;
    }

    private string category;
    /// <summary>
    /// Gets or sets the category of the book.
    /// </summary>
    public string Category {
        get => category;
        private set => category = string.IsNullOrWhiteSpace(value.Trim()) ? throw new ArgumentException("Category cannot be empty!") : value;
    }

    private string isbn;
    /// <summary>
    /// Gets or sets the ISBN of the book.
    /// </summary>
    public string ISBN {
        get => isbn;
        private set {
            // Check if the value is null or empty
            if(string.IsNullOrWhiteSpace(value.Trim()))
                throw new ArgumentException("ISBN cannot be empty!");

            // Check if the value contains only digits and possibly an 'X' at the end
            string cleaned = value.Replace("-", "");

            // Check if the cleaned value contains only digits or an 'X' at the end if is no ISBN10
            if(cleaned.Any(char.IsLetter) && !(cleaned.EndsWith("X", StringComparison.OrdinalIgnoreCase) && cleaned.Length == 10))
                throw new ArgumentException("ISBN must not contain letters, except possibly an 'X' at the end for ISBN-10.");

            // Check if the cleaned value is between 10 and 13 characters long
            if(cleaned.Length < 10 || cleaned.Length > 13)
                throw new ArgumentException("ISBN must be between 10 and 13 characters long!");

            isbn = value;
        }
    }
    /// <summary>
    /// Constructor for the Book class.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="author"></param>
    /// <param name="category"></param>
    /// <param name="isbn"></param>
    public Book(string title, string author, string category, string isbn) {
        try {

            Title = title;
            Author = author;
            Category = category;
            ISBN = isbn;
        } catch(ArgumentException) {

            throw;
        }
    }
    /// <summary>
    /// Overrides the ToString method to provide a string representation of the book.
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
        StringBuilder sb = new();
        sb.Append($"Title: {Title}{Environment.NewLine}");
        sb.Append($"Author: {Author}{Environment.NewLine}");
        sb.Append($"Category: {Category}{Environment.NewLine}");
        sb.Append($"ISBN: {ISBN}{Environment.NewLine}");
        return sb.ToString();
    }
}