
namespace SmartBook;

public static class ConsoleInputExtensions
{

    public static string GetBookDetails(this string header) {
        string? details = string.Empty;

        do {

            Console.Write($"{header}: ");
            details = Console.ReadLine();

            if(string.IsNullOrEmpty(details)) {
                Console.WriteLine($"Please enter a valid {header}.");
                continue;
            }
            else {

                break;
            }

        } while(true);

        return details;
    }


    public static void GetBookISBN(this string header, out string isbn) {
        string? input = string.Empty;
        bool done = false;

        do {
            Console.Write($"{header}: ");
            isbn = Console.ReadLine()!;
            input = isbn.Replace("-", "").Trim();

            if(string.IsNullOrWhiteSpace(isbn)) {

                $"\"{isbn}\" is not a valid ISBN.".DisplayErrorMessage();
                $"An ISBN must have more than 10 digits and at least 13 digits.".DisplayErrorMessage();
                continue;
            }
            else if(input.Length > 13 || input.Length < 10) {

                $"{isbn} is not a valid ISBN.".DisplayErrorMessage();
                $"An ISBN must have more than 10 digits and at least 13 digits.".DisplayErrorMessage();
                continue;
            }
            else if(input.Any(char.IsLetter) && !(input.EndsWith("X", StringComparison.OrdinalIgnoreCase) && input.Length == 10)) {
                $"{isbn} is not a valid ISBN.".DisplayErrorMessage();
                $"An ISBN must not contain letters, except possibly an 'X' at the end for ISBN-10.".DisplayErrorMessage();
                continue;
            }
            done = true;
        } while(!done);

    }

    public static ConsoleKeyInfo GetAnyKey(this string message) {
        Console.WriteLine(message);
        return Console.ReadKey();
    }

    public static void DisplayErrorMessage(this string message) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }


    public static void DisplaySuccessMessage(this string message) {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static void DisplayWarningMessage(this string message) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static void DisplayInfoMessage(this string message) {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    public static void DisplayStandardMessage(this string message) {
        Console.WriteLine(message);
    }
}
