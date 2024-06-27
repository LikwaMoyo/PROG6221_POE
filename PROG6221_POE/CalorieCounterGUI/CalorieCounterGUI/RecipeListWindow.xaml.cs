using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CalorieCounterGUI
{
    public partial class RecipeListWindow : Window
    {
        public Recipe SelectedRecipe { get; private set; }
        private List<Recipe> allRecipes;

        public RecipeListWindow(List<Recipe> recipes)
        {
            InitializeComponent();
            allRecipes = recipes;
            RecipeListBox.ItemsSource = allRecipes;
            RecipeListBox.DisplayMemberPath = "Name";

            // Populate food group ComboBox
            FoodGroupComboBox.ItemsSource = allRecipes
                .SelectMany(r => r.Ingredients)
                .Select(i => i.FoodGroup)
                .Distinct()
                .OrderBy(fg => fg)
                .ToList();
        }

        private void SelectRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeListBox.SelectedItem is Recipe recipe)
            {
                SelectedRecipe = recipe;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a recipe.", "Recipe Selection");
            }
        }

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            FilterRecipes();
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            IngredientFilterTextBox.Clear();
            FoodGroupComboBox.SelectedIndex = -1;
            MaxCaloriesTextBox.Clear();
            RecipeListBox.ItemsSource = allRecipes;
        }

        private void FilterRecipes()
        {
            IEnumerable<Recipe> filteredRecipes = allRecipes;

            // Filter by ingredient
            string ingredientFilter = IngredientFilterTextBox.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(ingredientFilter))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.Name.ToLower().Contains(ingredientFilter)));
            }

            // Filter by food group
            if (FoodGroupComboBox.SelectedItem is string selectedFoodGroup)
            {
                filteredRecipes = filteredRecipes.Where(r => r.Ingredients.Any(i => i.FoodGroup == selectedFoodGroup));
            }

            // Filter by maximum calories
            if (double.TryParse(MaxCaloriesTextBox.Text, out double maxCalories))
            {
                filteredRecipes = filteredRecipes.Where(r => r.CalculateTotalCalories() <= maxCalories);
            }

            RecipeListBox.ItemsSource = filteredRecipes.ToList();
        }
    }
}