
namespace SmartBook;

public static class ConsoleInputExtensions
{

    /// <summary>
    /// Get the details of a book form the user.
    /// </summary>
    /// <param name="header"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Get the ISBN of a book from the user.
    /// </summary>
    /// <param name="header"></param>
    /// <param name="isbn"></param>
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

    /// <summary>
    /// Get a string input from the user.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static ConsoleKeyInfo GetAnyKey(this string message) {
        Console.WriteLine(message);
        return Console.ReadKey();
    }

    /// <summary>
    /// Display an red error message to the user.
    /// </summary>
    /// <param name="message"></param>
    public static void DisplayErrorMessage(this string message) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    /// <summary>
    /// Display a green success message to the user.
    /// </summary>
    /// <param name="message"></param>
    public static void DisplaySuccessMessage(this string message) {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    /// <summary>
    /// Display a yellow warning message to the user.
    /// </summary>
    /// <param name="message"></param>
    public static void DisplayWarningMessage(this string message) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    /// <summary>
    /// Display a blue information message to the user.
    /// </summary>
    /// <param name="message"></param>
    public static void DisplayInfoMessage(this string message) {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    /// <summary>
    /// Display a standard message to the user.
    /// </summary>
    /// <param name="message"></param>
    public static void DisplayStandardMessage(this string message) {
        Console.WriteLine(message);
    }
}
