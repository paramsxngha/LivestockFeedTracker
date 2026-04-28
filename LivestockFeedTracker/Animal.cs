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
        public string GetAnimalName() { return animalName; }
        public string GetSpeciesName() { return speciesName; }
        public string GetFoodTypeName() { return foodTypeName; }
        public double[] GetFoodEachDay() { return foodEachDay; }
        public double GetGramCost() { return gramCost; }

        // Adds up all 7 days
        public double GetTotalWeeklyFood()
        {
            double total = 0;
            foreach (double day in foodEachDay)
            {
                total += day;
            }
            return total;
        }

        // Weekly total divided by 7
        public double GetDailyAverage()
        {
            return GetTotalWeeklyFood() / foodEachDay.Length;
        }

        // Weekly food x cost per gram
        public double GetWeeklyCost()
        {
            return GetTotalWeeklyFood() * gramCost;
        }

        // Checks if animal is eating the right amount
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

        // Returns true if animal is eating below 70% of the minimum
        public bool NeedsVet(double minFood)
        {
            return GetTotalWeeklyFood() < (minFood * 0.70);
        }
    }
}