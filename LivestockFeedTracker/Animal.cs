using System;
namespace LivestockFeedTracker
{
    public class Animal
    {
        // Fields
        private string animalName;
        private string animalID;
        private string speciesName;
        private string foodTypeName;
        private double[] foodEachDay;
        private double gramCost;
        // Constructor
        public Animal(string animalName, string animalID, string speciesName, string foodTypeName, double[] foodEachDay, double gramCost)
        {
            this.animalName = animalName;
            this.animalID = animalID;
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
        public string GetAnimalID()
        {
            return animalID;
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
        // Returns total cost to feed this animal for the week
        public double GetWeeklyCost()
        {
            return GetTotalWeeklyFood() * gramCost;
        }
    }
}