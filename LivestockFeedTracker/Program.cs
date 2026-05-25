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
        // Formats the name so first letter is uppercase and rest is lowercase
        static string FormatName(string input)
        {
            if (input.Length == 0) return input;
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }
        // Checks the name is not blank and contains only letters
        static string CheckName()
        {
            string name = Console.ReadLine();
            while (name.Equals("") || name.Any(char.IsDigit))
            {
                Console.WriteLine("Please enter a valid name.");
                name = Console.ReadLine();
            }
            return FormatName(name);
        }
        static void OneAnimal()
        {
            Console.WriteLine("\n--- New Animal ---");
            Console.WriteLine("Enter Animal Name:");
            string name = CheckName();
            // Show species menu
            List<string> species = appManager.GetSpeciesList();
            Console.WriteLine("\nSelect species:");
            for (int i = 0; i < species.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {species[i]}");
            }
            int speciesChoice = Convert.ToInt32(Console.ReadLine()) - 1;
            while (speciesChoice < 0 || speciesChoice > species.Count - 1)
            {
                Console.WriteLine($"Please enter a number between 1 and {species.Count}.");
                speciesChoice = Convert.ToInt32(Console.ReadLine()) - 1;
            }
            string chosenSpecies = species[speciesChoice];
            // Show food menu with price per 100g
            List<string> foods = appManager.GetFoodsForSpecies(chosenSpecies);
            Console.WriteLine("\nSelect food type:");
            for (int i = 0; i < foods.Count; i++)
            {
                double pricePer100g = appManager.GetFeedCost(foods[i]) * 100;
                Console.WriteLine($"  {i + 1}. {foods[i]} (${pricePer100g:F2} per 100g)");
            }
            int foodChoice = Convert.ToInt32(Console.ReadLine()) - 1;
            while (foodChoice < 0 || foodChoice > foods.Count - 1)
            {
                Console.WriteLine($"Please enter a number between 1 and {foods.Count}.");
                foodChoice = Convert.ToInt32(Console.ReadLine()) - 1;
            }
            string chosenFood = foods[foodChoice];
            double gramCost = appManager.GetFeedCost(chosenFood);
            // Enter food for each day
            double[] foodEachDay = new double[7];
            Console.WriteLine("\nEnter grams eaten each day:");
            for (int i = 0; i < DAYS.Count; i++)
            {
                Console.WriteLine($"{DAYS[i]}:");
                double amount = Convert.ToDouble(Console.ReadLine());
                while (amount < 0)
                {
                    Console.WriteLine("Amount must be 0 or greater.");
                    amount = Convert.ToDouble(Console.ReadLine());
                }
                foodEachDay[i] = amount;
            }
            Animal newAnimal = new Animal(name, chosenSpecies, chosenFood, foodEachDay, gramCost);
            appManager.AddAnimal(newAnimal);
            var (min, max) = appManager.GetFeedingRange(chosenSpecies);
            string status = newAnimal.GetFeedingStatus(min, max);
            Console.WriteLine("\n--- Animal Summary ---");
            Console.WriteLine($"Name      : {name}");
            Console.WriteLine($"Species   : {chosenSpecies}");
            Console.WriteLine($"Food Type : {chosenFood}  (${gramCost * 100:F2} per 100g)");
            for (int i = 0; i < DAYS.Count; i++)
            {
                Console.WriteLine($"{DAYS[i]}: {foodEachDay[i]}g  (${foodEachDay[i] * gramCost:F2})");
            }
            Console.WriteLine($"Total Food: {newAnimal.GetTotalWeeklyFood()}g");
            Console.WriteLine($"Daily Avg : {newAnimal.GetDailyAverage():F1}g");
            Console.WriteLine($"Cost      : ${newAnimal.GetWeeklyCost():F2}");

            // Show status in colour
            Console.Write("Status    : ");
            if (status == "Correct")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(status);
            Console.ForegroundColor = ConsoleColor.White;

            // Show consequence if not eating correctly
            if (status != "Correct")
            {
                Console.WriteLine($"Advice    : {appManager.GetFeedingAdvice(chosenSpecies, status)}");
            }
            // Show vet alert if severely undereating
            if (newAnimal.NeedsVet(min))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("VET ALERT : This animal needs to see a vet.");
                Console.ForegroundColor = ConsoleColor.White;
            }

            // Show budget after each animal added
            string budgetStatus = appManager.CheckFarmerBudget();
            Console.Write("Budget    : ");
            if (budgetStatus.Contains("OVER"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine(budgetStatus);
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Farm Animal Feeding Tracker\n");
            // Get budget before starting
            Console.WriteLine("Enter your weekly feeding budget ($):");
            double budget = Convert.ToDouble(Console.ReadLine());
            while (budget <= 0)
            {
                Console.WriteLine("Budget must be greater than 0.");
                budget = Convert.ToDouble(Console.ReadLine());
            }
            appManager = new AppManager(budget);
            string proceed = "";
            while (proceed.Equals(""))
            {
                OneAnimal();
                Console.WriteLine("Press <Enter> to add another animal or type 'Stop' to finish.");
                proceed = Console.ReadLine().ToUpper();
            }
            // Show full farm summary at end
            appManager.ShowFarmSummary();
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}