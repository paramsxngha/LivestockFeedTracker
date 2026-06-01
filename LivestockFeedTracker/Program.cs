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

        // Checks the name/id is not blank
        static string CheckNameID()
        {
            string input = Console.ReadLine();
            while (input.Equals(""))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Cannot be blank. (e.g. Molly#COW01)");
                Console.ForegroundColor = ConsoleColor.White;
                input = Console.ReadLine();
            }
            return input;
        }

        // Checks menu selection is within range and handles invalid input
        static int CheckMenuChoice(int count)
        {
            while (true)
            {
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (choice >= 0 && choice <= count - 1)
                    {
                        return choice;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid input. Please enter a number between 1 and {count}. (e.g. 1)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid input. Please enter a number between 1 and {count}. (e.g. 1)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        // Checks daily food amount is valid for the species
        static double CheckFoodAmount(double maxDaily, string species)
        {
            while (true)
            {
                try
                {
                    double amount = Convert.ToDouble(Console.ReadLine());
                    if (amount >= 0 && amount <= maxDaily)
                    {
                        return amount;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Invalid input. Amount must be between 0 and {maxDaily}g for {species}. (e.g. 500)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid number. (e.g. 500)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        // Checks budget is a valid positive number
        static double CheckBudget()
        {
            while (true)
            {
                try
                {
                    double budget = Convert.ToDouble(Console.ReadLine());
                    if (budget > 0)
                    {
                        return budget;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Budget must be greater than 0. (e.g. 500)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid number. (e.g. 500)");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        // Prints a grey separator line between sections
        static void PrintLine()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void OneAnimal()
        {
            PrintLine();
            Console.WriteLine("\n--- New Animal ---");
            Console.WriteLine("Enter Animal Name/ID (e.g. Molly#COW01):");
            string nameID = CheckNameID();

            // Show species menu
            List<string> species = appManager.GetSpeciesList();
            Console.WriteLine("\nSelect species:");
            for (int i = 0; i < species.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {species[i]}");
            }
            int speciesChoice = CheckMenuChoice(species.Count);
            string chosenSpecies = species[speciesChoice];

            // Show food menu with price per 100g
            List<string> foods = appManager.GetFoodsForSpecies(chosenSpecies);
            Console.WriteLine("\nSelect food type:");
            for (int i = 0; i < foods.Count; i++)
            {
                double pricePer100g = appManager.GetFeedCost(foods[i]) * 100;
                Console.WriteLine($"  {i + 1}. {foods[i]} (${pricePer100g:F2} per 100g)");
            }
            int foodChoice = CheckMenuChoice(foods.Count);
            string chosenFood = foods[foodChoice];
            double gramCost = appManager.GetFeedCost(chosenFood);

            // Enter food for each day
            double[] foodEachDay = new double[7];
            double maxDaily = appManager.GetMaxDailyFood(chosenSpecies);
            Console.WriteLine($"\nEnter grams eaten each day (max {maxDaily}g per day for {chosenSpecies}):");
            for (int i = 0; i < DAYS.Count; i++)
            {
                Console.WriteLine($"{DAYS[i]}:");
                foodEachDay[i] = CheckFoodAmount(maxDaily, chosenSpecies);
            }

            Animal newAnimal = new Animal(nameID, chosenSpecies, chosenFood, foodEachDay, gramCost);
            appManager.AddAnimal(newAnimal);

            var (min, max) = appManager.GetFeedingRange(chosenSpecies);
            string status = newAnimal.GetFeedingStatus(min, max);

            PrintLine();
            Console.WriteLine("\n--- Animal Summary ---");
            Console.WriteLine($"Name/ID   : {nameID}");
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
                Console.WriteLine(status);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(status);
                Console.ForegroundColor = ConsoleColor.White;
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
+                                                                                                            
 ▄▄▄▄▄▄▄                                      ▄▄▄▄▄▄▄             ▄▄                     ▄▄▄▄               
███▀▀▀▀▀                   ▀▀                ███▀▀▀▀▀             ██ ▀▀                ▄██▀▀██▄             
███▄▄  ▀▀█▄ ████▄ ███▄███▄ ██  ████▄ ▄████   ███▄▄ ▄█▀█▄ ▄█▀█▄ ▄████ ██  ████▄ ▄████   ███  ███ ████▄ ████▄ 
███▀▀ ▄█▀██ ██ ▀▀ ██ ██ ██ ██  ██ ██ ██ ██   ███▀▀ ██▄█▀ ██▄█▀ ██ ██ ██  ██ ██ ██ ██   ███▀▀███ ██ ██ ██ ██ 
███   ▀█▄██ ██    ██ ██ ██ ██▄ ██ ██ ▀████   ███   ▀█▄▄▄ ▀█▄▄▄ ▀████ ██▄ ██ ██ ▀████   ███  ███ ████▀ ████▀ 
                                        ██                                        ██            ██    ██    
                                      ▀▀▀                                       ▀▀▀             ▀▀    ▀▀    +
            ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Track animal feeding, monitor health indicators, calculate feed costs, receive feeding alerts, and manage your farm's budget all in one place.\n");
            Console.WriteLine("==========================================================\n");

            // Get budget before starting
            Console.WriteLine("Enter your weekly feeding budget ($):");
            double budget = CheckBudget();
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