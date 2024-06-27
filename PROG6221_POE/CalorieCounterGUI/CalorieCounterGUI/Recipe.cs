using System;
using System.Collections.Generic;

namespace CalorieCounterGUI
{
	public class Ingredient
	{
		public string Name { get; set; }
		public double Quantity { get; set; }
		public string Unit { get; set; }
		public double Calories { get; set; }
		public string FoodGroup { get; set; }
	}

	public class Step
	{
		public string Description { get; set; }
	}

	public class Recipe
	{
		public string Name { get; set; }
		public int NumIngredients { get; set; }
		public List<Ingredient> Ingredients { get; set; }
		public int NumSteps { get; set; }
		public List<Step> Steps { get; set; }
		public double ScaleFactor { get; private set; } = 1.0;

		public delegate void CalorieNotificationHandler(string message);
		public event CalorieNotificationHandler CalorieNotification;

		public void ScaleRecipe(double factor)
		{
			ScaleFactor = factor;
			foreach (Ingredient ingredient in Ingredients)
			{
				ingredient.Quantity *= factor;
			}

			CalculateTotalCalories();
		}

		public void ResetQuantities()
		{
			foreach (Ingredient ingredient in Ingredients)
			{
				ingredient.Quantity /= ScaleFactor;
			}
			ScaleFactor = 1.0;

			CalculateTotalCalories();
		}

		public double CalculateTotalCalories()
		{
			double totalCalories = 0;
			foreach (Ingredient ingredient in Ingredients)
			{
				totalCalories = totalCalories + ScaleFactor * ingredient.Calories;
				//totalCalories += ingredient.Calories;
			}

			if (totalCalories > 300)
			{
				CalorieNotification?.Invoke($"Warning: The total calories of this recipe exceed 300! (Total: {totalCalories} calories)");
			}

			return totalCalories;
		}
	}
}