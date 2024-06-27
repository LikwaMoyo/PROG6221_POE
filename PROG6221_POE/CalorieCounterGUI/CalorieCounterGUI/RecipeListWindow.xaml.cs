using System.Collections.Generic;
using System.Windows;

namespace CalorieCounterGUI
{
    public partial class RecipeListWindow : Window
    {
        public Recipe SelectedRecipe { get; private set; }

        public RecipeListWindow(List<Recipe> recipes)
        {
            InitializeComponent();
            RecipeListBox.ItemsSource = recipes;
            RecipeListBox.DisplayMemberPath = "Name"; // Add this line to display recipe names
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
    }
}