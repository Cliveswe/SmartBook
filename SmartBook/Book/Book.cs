using System.Text;

namespace SmartBook.Tests;

public class Book
{
    private string title;

    public string Title {
        get {
            return title;
        }
        private set {

            if(string.IsNullOrEmpty(value))
                throw new ArgumentException("Title cannot be empty!");
            else
                title = value;
        }
    }

    private string author;

    public string Author {
        get {
            return author;
        }
        private set {

            if(string.IsNullOrEmpty(value))
                throw new ArgumentException("Author cannot be empty!");
            else
                author = value;
        }
    }

    private string category;

    public string Category {
        get {
            return category;
        }
        private set {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentException("Category cannot be empty!");
            else
                category = value;
        }
    }


    private string isbn;

    public string ISBN {
        get {
            return isbn;
        }
        private set {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentException("ISBN cannot be empty!");
            else
                isbn = value;
        }
    }
    public Book(string title, string author, string category, string isbn) {
        Title = title;
        Author = author;
        Category = category;
        ISBN = isbn;
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