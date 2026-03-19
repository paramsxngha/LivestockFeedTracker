using System;
namespace FarmingFeedingApp
{
    public class AppManager
    {
        // Fields
        static double farmerBudget;
        static List<Animal> animalList = new List<Animal>();

        // Constant food costs per gram
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

        // Constructor
        public AppManager(double budget)
        {
            farmerBudget = budget;
        }
    }
}