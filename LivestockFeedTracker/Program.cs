using System;
namespace FarmingFeedingApp
{
    class Program
    {
        // Global variables
        static AppManager appManager;
        static List<string> DAYS = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        static string CheckProceed()
        {
            string proceed;
            while (true)
            {
                Console.WriteLine("Press <Enter> to add another animal or type 'Stop' to finish.");
                proceed = Console.ReadLine().ToUpper();
                if (proceed.Equals("") || proceed.Equals("STOP"))
                {
                    return proceed;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Invalid input.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static double CheckBudget()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter your weekly feeding budget ($):");
                    double budget = Convert.ToDouble(Console.ReadLine());
                    if (budget > 0)
                    {
                        return budget;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: Budget must be greater than 0.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: You must enter a valid number.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Farm Animal Feeding Tracker");

            double budget = CheckBudget();
            appManager = new AppManager(budget);

            string proceed = "";
            while (proceed.Equals(""))
            {
                // OneAnimal();
                proceed = CheckProceed();
            }
        }
    }
}