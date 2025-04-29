
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


    public static void GetBookISBN(string header, out int isbn) {
        string? input = string.Empty;

        do {
            Console.Write($"{header}: ");
            input = Console.ReadLine();

            if(string.IsNullOrEmpty(input) || input.Length > 13 || input.Length < 10) {
                Console.WriteLine($"An {header} must have at least 13 digits and more " +
                    $"than 10 digits.");
                Console.WriteLine($"Please enter a valid {header}.");
                continue;
            }

        } while(!int.TryParse(Console.ReadLine(), out isbn));

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
