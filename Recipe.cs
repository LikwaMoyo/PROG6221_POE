using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221_POE
{
    // Define the Recipe class to hold all the details of the recipe
   internal class Recipe
    {
        public int NumIngredients { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int NumSteps { get; set; }
        public List<Step> Steps { get; set; }
        public double _scaleFactor = 1.0;

        // Method to scale the recipe by a given factor
        public void ScaleRecipe(double factor)
        {
            _scaleFactor = factor;
            foreach (Ingredient ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        // Method to reset the quantities to the original values
        public void ResetQuantities()
        {
            _scaleFactor = 1.0;
            foreach (Ingredient ingredient in Ingredients)
            {
                ingredient.Quantity /= _scaleFactor;
            }
        }
    }
}
