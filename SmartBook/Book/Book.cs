using System.Text;

namespace SmartBook.Tests;

public class Book
{
    private string title;

    public string Title {
        get => title;
        private set => title = string.IsNullOrWhiteSpace(value.Trim()) ? throw new ArgumentException("Title cannot be empty!") : value;
    }

    private string author;

    public string Author {
        get => author;
        private set => author = string.IsNullOrWhiteSpace(value.Trim()) ? throw new ArgumentException("Author cannot be empty!") : value;
    }

    private string category;

    public string Category {
        get => category;
        private set => category = string.IsNullOrWhiteSpace(value.Trim()) ? throw new ArgumentException("Category cannot be empty!") : value;
    }

    private string isbn;

    public string ISBN {
        get => isbn;
        private set {
            if(string.IsNullOrWhiteSpace(value.Trim()))
                throw new ArgumentException("ISBN cannot be empty!");

            string cleaned = value.Replace("-", "");

            if(cleaned.Any(char.IsLetter) && !(cleaned.EndsWith("X", StringComparison.OrdinalIgnoreCase) && cleaned.Length == 10))
                throw new ArgumentException("ISBN must not contain letters, except possibly an 'X' at the end for ISBN-10.");

            if(cleaned.Length < 10 || cleaned.Length > 13)
                throw new ArgumentException("ISBN must be between 10 and 13 characters long!");

            isbn = value;
        }
    }
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

    public override string ToString() {
        StringBuilder sb = new();
        sb.Append($"Title: {Title}{Environment.NewLine}");
        sb.Append($"Author: {Author}{Environment.NewLine}");
        sb.Append($"Category: {Category}{Environment.NewLine}");
        sb.Append($"ISBN: {ISBN}{Environment.NewLine}");
        return sb.ToString();
    }
}