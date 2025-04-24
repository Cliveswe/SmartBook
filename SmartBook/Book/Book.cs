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
        private set => isbn = string.IsNullOrWhiteSpace(value.Trim()) ? throw new ArgumentException("ISBN cannot be empty!") : value;
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