using System;
using System.Collections.Generic;
using System.Windows;

namespace CalorieCounterGUI
{
    public partial class RecipeInputWindow : Window
    {
        public Recipe Recipe { get; private set; }

        public RecipeInputWindow()
        {
            InitializeComponent();
            Recipe = new Recipe
            {
                Ingredients = new List<Ingredient>(),
                Steps = new List<Step>()
            };
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateIngredientInput())
            {
                var ingredient = new Ingredient
                {
                    Name = IngredientNameTextBox.Text,
                    Quantity = double.Parse(QuantityTextBox.Text),
                    Unit = UnitTextBox.Text,
                    Calories = double.Parse(CaloriesTextBox.Text),
                    FoodGroup = FoodGroupTextBox.Text
                };
                Recipe.Ingredients.Add(ingredient);

                // Clear input fields
                IngredientNameTextBox.Clear();
                QuantityTextBox.Clear();
                UnitTextBox.Clear();
                CaloriesTextBox.Clear();
                FoodGroupTextBox.Clear();

                MessageBox.Show("Ingredient added successfully!", "Success");
            }
        }

        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(StepDescriptionTextBox.Text))
            {
                var step = new Step { Description = StepDescriptionTextBox.Text };
                Recipe.Steps.Add(step);

                // Clear input field
                StepDescriptionTextBox.Clear();

                MessageBox.Show("Step added successfully!", "Success");
            }
            else
            {
                MessageBox.Show("Please enter a step description.", "Invalid Input");
            }
        }

        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateRecipe())
            {
                Recipe.Name = RecipeNameTextBox.Text;
                Recipe.NumIngredients = Recipe.Ingredients.Count;
                Recipe.NumSteps = Recipe.Steps.Count;
                DialogResult = true;
                Close();
            }
        }

        private bool ValidateIngredientInput()
        {
            if (string.IsNullOrWhiteSpace(IngredientNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(QuantityTextBox.Text) ||
                string.IsNullOrWhiteSpace(UnitTextBox.Text) ||
                string.IsNullOrWhiteSpace(CaloriesTextBox.Text) ||
                string.IsNullOrWhiteSpace(FoodGroupTextBox.Text))
            {
                MessageBox.Show("Please fill in all ingredient fields.", "Invalid Input");
                return false;
            }

            if (!double.TryParse(QuantityTextBox.Text, out _) || !double.TryParse(CaloriesTextBox.Text, out _))
            {
                MessageBox.Show("Quantity and Calories must be valid numbers.", "Invalid Input");
                return false;
            }

            return true;
        }

        private bool ValidateRecipe()
        {
            if (string.IsNullOrWhiteSpace(RecipeNameTextBox.Text))
            {
                MessageBox.Show("Please enter a recipe name.", "Invalid Input");
                return false;
            }

            if (Recipe.Ingredients.Count == 0)
            {
                MessageBox.Show("Please add at least one ingredient.", "Invalid Input");
                return false;
            }

            if (Recipe.Steps.Count == 0)
            {
                MessageBox.Show("Please add at least one step.", "Invalid Input");
                return false;
            }

            return true;
        }
    }
}