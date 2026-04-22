using System;
namespace LivestockFeedTracker
{
    class Program
    {
        static AppManager appManager;
        static List<string> DAYS = new List<string>()
        {
            "Monday", "Tuesday", "Wednesday",
            "Thursday", "Friday", "Saturday", "Sunday"
        };

        static void OneAnimal()
        {
            Console.WriteLine("\n--- New Animal ---");
            Console.WriteLine("Enter Animal Name:");
            string name = Console.ReadLine();

            // Show species menu
            List<string> species = appManager.GetSpeciesList();
            Console.WriteLine("\nSelect species:");
            for (int i = 0; i < species.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {species[i]}");
            }
            int speciesChoice = Convert.ToInt32(Console.ReadLine()) - 1;
            string chosenSpecies = species[speciesChoice];

            // Show food menu for chosen species
            List<string> foods = appManager.GetFoodsForSpecies(chosenSpecies);
            Console.WriteLine("\nSelect food type:");
            for (int i = 0; i < foods.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {foods[i]}");
            }
            int foodChoice = Convert.ToInt32(Console.ReadLine()) - 1;
            string chosenFood = foods[foodChoice];

            // Enter food for each day
            double[] foodEachDay = new double[7];
            Console.WriteLine("\nEnter grams eaten each day:");
            for (int i = 0; i < DAYS.Count; i++)
            {
                Console.WriteLine($"{DAYS[i]}:");
                foodEachDay[i] = Convert.ToDouble(Console.ReadLine());
            }

            double gramCost = appManager.GetFeedCost(chosenFood);
            Animal newAnimal = new Animal(name, chosenSpecies, chosenFood, foodEachDay, gramCost);
            appManager.AddAnimal(newAnimal);

            var (min, max) = appManager.GetFeedingRange(chosenSpecies);

            // Print summary for this animal
            Console.WriteLine("\n--- Animal Summary ---");
            Console.WriteLine($"Name      : {name}");
            Console.WriteLine($"Species   : {chosenSpecies}");
            Console.WriteLine($"Food Type : {chosenFood}");
            for (int i = 0; i < DAYS.Count; i++)
            {
                Console.WriteLine($"{DAYS[i]}: {foodEachDay[i]}g");
            }
            Console.WriteLine($"Total Food: {newAnimal.GetTotalWeeklyFood()}g");
            Console.WriteLine($"Daily Avg : {newAnimal.GetDailyAverage():F1}g");
            Console.WriteLine($"Cost      : ${newAnimal.GetWeeklyCost():F2}");
            Console.WriteLine($"Status    : {newAnimal.GetFeedingStatus(min, max)}");
            Console.WriteLine($"Budget    : {appManager.CheckFarmerBudget()}");

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Farm Animal Feeding Tracker\n");

            // Ask for budget before starting
            Console.WriteLine("Enter your weekly feeding budget ($):");
            double budget = Convert.ToDouble(Console.ReadLine());
            appManager = new AppManager(budget);

            string proceed = "";
            while (proceed.Equals(""))
            {
                OneAnimal();
                Console.WriteLine("Press <Enter> to add another animal or type 'Stop' to finish.");
                proceed = Console.ReadLine().ToUpper();
            }

            // Show total at the end
            Console.WriteLine($"\nTotal Farm Cost This Week: ${appManager.GetTotalFarmCost():F2}");
            Console.WriteLine($"Budget Status: {appManager.CheckFarmerBudget()}");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}