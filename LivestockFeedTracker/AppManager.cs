using System;
namespace LivestockFeedTracker
{
    public class AppManager
    {
        // Fields
        static List<Animal> animalList = new List<Animal>();
        static double farmerBudget;

        // Food costs per gram
        static readonly double GRASS_SILAGE = 0.0003;
        static readonly double MAIZE_SILAGE = 0.0004;
        static readonly double GRAIN_MIX = 0.0006;
        static readonly double PASTURE_GRASS = 0.0002;
        static readonly double GRASS_HAY = 0.00035;
        static readonly double GRAIN_SUPPLEMENT = 0.00055;
        static readonly double SILAGE = 0.0003;
        static readonly double CORN = 0.00028;
        static readonly double SOYBEAN_MEAL = 0.00045;
        static readonly double WHEAT = 0.0003;
        static readonly double LAYER_FEED = 0.0004;
        static readonly double OATS = 0.00031;
        static readonly double ALFALFA = 0.00048;
        static readonly double MIXED_HAY = 0.00033;

        // Maps each species to its food types
        static Dictionary<string, List<string>> foodBySpecies = new Dictionary<string, List<string>>()
        {
            { "Dairy Cow", new List<string>() { "Grass Silage", "Maize Silage", "Grain Mix" } },
            { "Beef Cow",  new List<string>() { "Pasture Grass", "Grass Hay", "Grain Supplement" } },
            { "Sheep",     new List<string>() { "Pasture Grass", "Silage", "Grain Mix" } },
            { "Pig",       new List<string>() { "Corn", "Soybean Meal", "Wheat" } },
            { "Chicken",   new List<string>() { "Layer Feed", "Corn", "Oats" } },
            { "Goat",      new List<string>() { "Mixed Hay", "Pasture Grass", "Grain Mix" } },
            { "Buffalo",   new List<string>() { "Alfalfa", "Maize Silage", "Grass Silage" } }
        };

        // Maps each food type to its cost per gram
        static Dictionary<string, double> feedCosts = new Dictionary<string, double>()
        {
            { "Grass Silage",     GRASS_SILAGE },
            { "Maize Silage",     MAIZE_SILAGE },
            { "Grain Mix",        GRAIN_MIX },
            { "Pasture Grass",    PASTURE_GRASS },
            { "Grass Hay",        GRASS_HAY },
            { "Grain Supplement", GRAIN_SUPPLEMENT },
            { "Silage",           SILAGE },
            { "Corn",             CORN },
            { "Soybean Meal",     SOYBEAN_MEAL },
            { "Wheat",            WHEAT },
            { "Layer Feed",       LAYER_FEED },
            { "Oats",             OATS },
            { "Alfalfa",          ALFALFA },
            { "Mixed Hay",        MIXED_HAY }
        };

        // Constructor
        public AppManager(double budget)
        {
            farmerBudget = budget;
        }

        // Adds a new animal to the list
        public void AddAnimal(Animal a)
        {
            animalList.Add(a);
        }

        // Returns food types for a given species
        public List<string> GetFoodsForSpecies(string species)
        {
            return foodBySpecies[species];
        }

        // Returns cost per gram for a given food type
        public double GetFeedCost(string foodType)
        {
            return feedCosts[foodType];
        }

        // Returns the full species list
        public List<string> GetSpeciesList()
        {
            return new List<string>(foodBySpecies.Keys);
        }

        // Returns healthy food range for a given species
        public (double min, double max) GetFeedingRange(string species)
        {
            if (species == "Dairy Cow") return (49000, 70000);
            if (species == "Beef Cow") return (42000, 63000);
            if (species == "Sheep") return (2800, 4900);
            if (species == "Pig") return (7000, 14000);
            if (species == "Chicken") return (700, 1050);
            if (species == "Goat") return (3500, 6000);
            if (species == "Buffalo") return (56000, 84000);
        }

        // Adds up all animals weekly costs
        public double GetTotalFarmCost()
        {
            double total = 0;
            foreach (Animal a in animalList)
            {
                total += a.GetWeeklyCost();
            }
            return total;
        }

        // Compares total cost to budget
        public string CheckFarmerBudget()
        {
            double diff = GetTotalFarmCost() - farmerBudget;
            if (diff > 0)
            {
                return $"OVER BUDGET by ${diff:F2}";
            }
            return $"Under budget by ${Math.Abs(diff):F2}";
        }
    }
}