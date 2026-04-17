using System;
namespace LivestockFeedTracker
{
    public class Animal
    {
        // Fields
        private string animalName;
        private string speciesName;
        private string foodTypeName;
        private double[] foodEachDay;
        private double gramCost;
        // Constructor
        public Animal(string animalName, string speciesName, string foodTypeName, double[] foodEachDay, double gramCost)
        {
            this.animalName = animalName;
            this.speciesName = speciesName;
            this.foodTypeName = foodTypeName;
            this.foodEachDay = foodEachDay;
            this.gramCost = gramCost;
        }
        // Getters
        public string GetAnimalName()
        {
            return animalName;
        }
        public string GetSpeciesName()
        {
            return speciesName;
        }
        public string GetFoodTypeName()
        {
            return foodTypeName;
        }
        public double[] GetFoodEachDay()
        {
            return foodEachDay;
        }
        public double GetGramCost()
        {
            return gramCost;
        }
        // Returns total grams eaten across the week
        public double GetTotalWeeklyFood()
        {
            double total = 0;
            foreach (double day in foodEachDay)
            {
                total += day;
            }
            return total;
        }
        // Returns average grams eaten per day
        public double GetDailyAverage()
        {
            return GetTotalWeeklyFood() / foodEachDay.Length;
        }
        // Returns total cost to feed this animal for the week
        public double GetWeeklyCost()
        {
            return GetTotalWeeklyFood() * gramCost;
        }
        // Checks weekly food against healthy range
        public string GetFeedingStatus(double minFood, double maxFood)
        {
            double total = GetTotalWeeklyFood();
            if (total < minFood)
            {
                return "Undereating";
            }
            else if (total > maxFood)
            {
                return "Overeating";
            }
            else
            {
                return "Correct";
            }
        }
    }
}