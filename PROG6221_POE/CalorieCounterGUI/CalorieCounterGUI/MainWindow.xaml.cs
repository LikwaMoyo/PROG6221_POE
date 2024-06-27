using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CalorieCounterGUI
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes = new List<Recipe>();
        private Recipe currentRecipe;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewRecipe_Click(object sender, RoutedEventArgs e)
        {
            var recipeWindow = new RecipeInputWindow();
            if (recipeWindow.ShowDialog() == true)
            {
                currentRecipe = recipeWindow.Recipe;
                recipes.Add(currentRecipe);
                DisplayRecipe(currentRecipe);
            }
        }

        private void DisplayRecipes_Click(object sender, RoutedEventArgs e)
        {
            if (recipes.Count == 0)
            {
                MessageBox.Show("No recipes have been entered yet.", "Recipe List");
                return;
            }

            var recipeListWindow = new RecipeListWindow(recipes);
            if (recipeListWindow.ShowDialog() == true)
            {
                currentRecipe = recipeListWindow.SelectedRecipe;
                DisplayRecipe(currentRecipe);
            }
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (currentRecipe == null)
            {
                MessageBox.Show("No recipe has been selected yet.", "Scale Recipe");
                return;
            }

            var scaleWindow = new ScaleWindow();
            if (scaleWindow.ShowDialog() == true)
            {
                currentRecipe.ScaleRecipe(scaleWindow.ScaleFactor);
                DisplayRecipe(currentRecipe);
            }
        }

        private void ResetQuantities_Click(object sender, RoutedEventArgs e)
        {
            if (currentRecipe == null)
            {
                MessageBox.Show("No recipe has been selected yet.", "Reset Quantities");
                return;
            }

            currentRecipe.ResetQuantities();
            DisplayRecipe(currentRecipe);
        }

        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            recipes.Clear();
            currentRecipe = null;
            RecipeDetailsTextBox.Clear();
            MessageBox.Show("Recipe data has been cleared.", "Clear Data");
        }

        private void DisplayRecipe(Recipe recipe)
        {
            if (recipe == null) return;

            var details = $"Recipe: {recipe.Name}\n";
            details += $"Number of ingredients: {recipe.NumIngredients}\n\n";

            foreach (var ingredient in recipe.Ingredients)
            {
                details += $"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} " +
                           $"({ingredient.Calories} calories, {ingredient.FoodGroup} group)\n";
            }

            details += $"\nNumber of steps: {recipe.NumSteps}\n";

            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                details += $"{i + 1}. {recipe.Steps[i].Description}\n";
            }

            double totalCalories = recipe.CalculateTotalCalories();
            details += $"\nTotal calories: {totalCalories}\n";
            details += $"Current scale factor: {recipe.ScaleFactor}";

            RecipeDetailsTextBox.Text = details;
        }
    }
}