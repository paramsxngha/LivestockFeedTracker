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

            Console.WriteLine("Enter Animal Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Animal ID:");
            string id = Console.ReadLine();

            List<string> species = appManager.GetSpeciesList();
            Console.WriteLine("\nSelect species:");
            for (int i = 0; i < species.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {species[i]}");
            }
            int speciesChoice = Convert.ToInt32(Console.ReadLine()) - 1;
            string chosenSpecies = species[speciesChoice];

            List<string> foods = appManager.GetFoodsForSpecies(chosenSpecies);
            Console.WriteLine("\nSelect food type:");
            for (int i = 0; i < foods.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {foods[i]}");
            }
            int foodChoice = Convert.ToInt32(Console.ReadLine()) - 1;
            string chosenFood = foods[foodChoice];

            double[] foodEachDay = new double[7];
            Console.WriteLine("\nEnter grams eaten each day:");
            for (int i = 0; i < DAYS.Count; i++)
            {
                Console.WriteLine($"{DAYS[i]}:");
                foodEachDay[i] = Convert.ToDouble(Console.ReadLine());
            }

            Console.ReadLine();
        }
    }
}