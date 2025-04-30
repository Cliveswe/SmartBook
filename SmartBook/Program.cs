using SmartBook.Application;

namespace SmartBook
{
    internal class Program
    {
        static void Main(string[] args) {
            Console.WriteLine("SmartBook: your personal library system");
            new SmartBookApplication().Start();
        }
    }
}
