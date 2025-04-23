

namespace SmartBook;

public class SmartBookApplication
{
    public void Start() {
        char input = ' ';

        while(true) {

            DisplayMainMenu();
            try {
                Console.Write("Enter a menu choice: ");
                input = Console.ReadLine()![0];
            } catch(IndexOutOfRangeException) {

                Console.Clear();
                Console.WriteLine("Please enter some input!");
            }

            switch(input) {
                case '1':
                break;
                case '0':
                Environment.Exit(0);
                break;
                default:
                Console.WriteLine("Choice not recognized please choose from the menu.");
                break;
            }

        }
    }

    private void DisplayMainMenu() {

        Console.WriteLine("Navigate through the menu by selecting a number\n" +
            "1. Add a book to the library." +
            "0. Exit the library.");
        Console.WriteLine();
    }
}