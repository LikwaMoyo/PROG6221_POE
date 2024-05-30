using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221_POE
{

    // Define the Recipe class to hold all the details of the recipe
    class Recipe
    {
        public string Name { get; set; }
        public int NumIngredients { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int NumSteps { get; set; }
        public List<Step> Steps { get; set; }
        public double ScaleFactor { get; private set; } = 1.0;

        // Define a delegate for the calorie notification
        public delegate void CalorieNotificationHandler(string message);
        public event CalorieNotificationHandler CalorieNotification;

        // Method to scale the recipe by a given factor
        public void ScaleRecipe(double factor)
        {
            ScaleFactor = factor;
            foreach (Ingredient ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }

            // Recalculate the calories after scaling
            CalculateTotalCalories();
        }

        // Method to reset the quantities to the original values
        public void ResetQuantities()
        {
            foreach (Ingredient ingredient in Ingredients)
            {
                ingredient.Quantity /= ScaleFactor;
            }
            ScaleFactor = 1.0;

            // Recalculate the calories after resetting
            CalculateTotalCalories();
        }

        // Method to calculate the total calories
        public double CalculateTotalCalories()
        {
            double totalCalories = 0;
            foreach (Ingredient ingredient in Ingredients)
            {
                totalCalories += ingredient.Calories;
            }

            // Trigger the event if calories exceed 300
            if (totalCalories > 300)
            {
                CalorieNotification?.Invoke($"Warning: The total calories of this recipe exceed 300! (Total: {totalCalories} calories)");
            }

            return totalCalories;
        }
    }
}
