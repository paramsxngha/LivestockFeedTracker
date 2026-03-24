using System;
namespace FarmingFeedingApp
{
    public class AppManager
    {
        // Fields
        static List<Animal> animalList = new List<Animal>();

        // Constant food costs per gram
        static readonly double MAIZE_SILAGE = 0.0004;
        static readonly double ALFALFA_HAY = 0.00048;
        static readonly double GRAIN_MIX = 0.0006;
        static readonly double RYEGRASS = 0.00018;
        static readonly double MEADOW_HAY = 0.00033;
        static readonly double BARLEY = 0.00034;
        static readonly double CLOVER = 0.00021;
        static readonly double OATS = 0.00031;
        static readonly double WHEAT = 0.0003;
        static readonly double VEGETABLE_SCRAPS = 0.00015;
        static readonly double LAYER_PELLETS = 0.00042;

        // Species list
        static List<string> speciesList = new List<string>() { "Dairy Cow", "Beef Cow", "Sheep", "Pig", "Chicken" };

        // Food options per species
        static List<string> dairyCowFoods = new List<string>() { "Maize Silage", "Alfalfa Hay", "Grain Mix" };
        static List<string> beefCowFoods = new List<string>() { "Ryegrass", "Meadow Hay", "Barley" };
        static List<string> sheepFoods = new List<string>() { "Ryegrass", "Clover", "Oats" };
        static List<string> pigFoods = new List<string>() { "Wheat", "Barley", "Vegetable Scraps" };
        static List<string> chickenFoods = new List<string>() { "Layer Pellets", "Wheat", "Oats" };

        // Constructor
        public AppManager()
        {
        }

        // Returns food list for chosen species
        public List<string> GetFoodsForSpecies(string species)
        {
            if (species == "Dairy Cow") return dairyCowFoods;
            if (species == "Beef Cow") return beefCowFoods;
            if (species == "Sheep") return sheepFoods;
            if (species == "Pig") return pigFoods;
            return chickenFoods;
        }

        // Returns cost per gram for chosen food type
        public double GetFeedCost(string foodType)
        {
            if (foodType == "Maize Silage") return MAIZE_SILAGE;
            if (foodType == "Alfalfa Hay") return ALFALFA_HAY;
            if (foodType == "Grain Mix") return GRAIN_MIX;
            if (foodType == "Ryegrass") return RYEGRASS;
            if (foodType == "Meadow Hay") return MEADOW_HAY;
            if (foodType == "Barley") return BARLEY;
            if (foodType == "Clover") return CLOVER;
            if (foodType == "Oats") return OATS;
            if (foodType == "Wheat") return WHEAT;
            if (foodType == "Vegetable Scraps") return VEGETABLE_SCRAPS;
            return LAYER_PELLETS;
        }

        // Returns the species list
        public List<string> GetSpeciesList()
        {
            return speciesList;
        }

        // Adds a new animal to the list
        public void AddAnimal(Animal a)
        {
            animalList.Add(a);
        }
    }
}