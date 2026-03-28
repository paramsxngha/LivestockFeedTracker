using System;
namespace LivestockFeedTracker
{
    class Program
    {
        static AppManager appManager = new AppManager();
        static List<string> DAYS = new List<string>()
        {
            "Monday", "Tuesday", "Wednesday",
            "Thursday", "Friday", "Saturday", "Sunday"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Farm Animal Feeding Tracker\n");

            List<string> species = appManager.GetSpeciesList();
            Console.WriteLine("Select species:");
            for (int i = 0; i < species.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {species[i]}");
            }

            Console.ReadLine();
        }
    
    }
}