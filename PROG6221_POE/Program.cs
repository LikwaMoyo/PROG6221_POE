using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221_POE
{
    class Program
    {
        static List<Recipe> recipes = new List<Recipe>();
        static Recipe currentRecipe;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to the Recipe Application!");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Enter a new recipe");
                Console.WriteLine("2. Display recipes");
                Console.WriteLine("3. Display the current recipe");
                Console.WriteLine("4. Scale the recipe");
                Console.WriteLine("5. Reset the recipe quantities");
                Console.WriteLine("6. Clear the recipe data");
                Console.WriteLine("7. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        currentRecipe = GetRecipeDetails();
                        recipes.Add(currentRecipe);
                        break;
                    case 2:
                        DisplayRecipeList();
                        break;
                    case 3:
                        if (currentRecipe != null)
                        {
                            DisplayRecipe(currentRecipe);
                        }
                        else
                        {
                            Console.WriteLine("No recipe has been selected yet.");
                        }
                        break;
                    case 4:
                        if (currentRecipe != null)
                        {
                            Console.WriteLine("Enter the scale factor (0.5, 2, or 3):");
                            double factor = double.Parse(Console.ReadLine());
                            currentRecipe.ScaleRecipe(factor);
                            DisplayRecipe(currentRecipe);
                        }
                        else
                        {
                            Console.WriteLine("No recipe has been selected yet.");
                        }
                        break;
                    case 5:
                        if (currentRecipe != null)
                        {
                            currentRecipe.ResetQuantities();
                            DisplayRecipe(currentRecipe);
                        }
                        else
                        {
                            Console.WriteLine("No recipe has been selected yet.");
                        }
                        break;
                    case 6:
                        recipes.Clear(); // Clear the recipes list
                        currentRecipe = null; // Reset the currentRecipe
                        Console.WriteLine("Recipe data has been cleared.");
                        break;
                    case 7:
                        Console.WriteLine("Exiting the application...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        // Function to get the recipe details from the user
        static Recipe GetRecipeDetails()
        {
            Recipe recipe = new Recipe();

            // Subscribe to the calorie notification event
            recipe.CalorieNotification += HandleCalorieNotification;

            // Get the recipe name
            Console.Write("Enter the recipe name: ");
            recipe.Name = Console.ReadLine();

            // Get the number of ingredients
            Console.Write("Enter the number of ingredients: ");
            recipe.NumIngredients = int.Parse(Console.ReadLine());

            // Get the details of each ingredient
            recipe.Ingredients = new List<Ingredient>();
            for (int i = 0; i < recipe.NumIngredients; i++)
            {
                Console.WriteLine($"Ingredient {i + 1}:");
                Ingredient ingredient = new Ingredient();
                Console.Write("Name: ");
                ingredient.Name = Console.ReadLine();
                Console.Write("Quantity: ");
                ingredient.Quantity = double.Parse(Console.ReadLine());
                Console.Write("Unit: ");
                ingredient.Unit = Console.ReadLine();
                Console.Write("Calories: ");
                ingredient.Calories = double.Parse(Console.ReadLine());
                Console.Write("Food Group: ");
                ingredient.FoodGroup = Console.ReadLine();
                recipe.Ingredients.Add(ingredient);
            }

            // Get the number of steps
            Console.Write("Enter the number of steps: ");
            recipe.NumSteps = int.Parse(Console.ReadLine());

            // Get the details of each step
            recipe.Steps = new List<Step>();
            for (int i = 0; i < recipe.NumSteps; i++)
            {
                Console.WriteLine($"Step {i + 1}:");
                Step step = new Step();
                Console.Write("Description: ");
                step.Description = Console.ReadLine();
                recipe.Steps.Add(step);
            }

            return recipe;
        }

        // Function to handle calorie notifications
        static void HandleCalorieNotification(string message)
        {
            Console.WriteLine(message);
        }

        // Function to display the recipe
        static void DisplayRecipe(Recipe recipe)
        {
            Console.WriteLine($"Recipe: {recipe.Name}");
            Console.WriteLine($"Number of ingredients: {recipe.NumIngredients}");

            // Display the details of each ingredient
            for (int i = 0; i < recipe.NumIngredients; i++)
            {
                Ingredient ingredient = recipe.Ingredients[i];
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup} group)");
            }

            Console.WriteLine($"Number of steps: {recipe.NumSteps}");

            // Display the details of each step
            for (int i = 0; i < recipe.NumSteps; i++)
            {
                Step step = recipe.Steps[i];
                Console.WriteLine($"{i + 1}. {step.Description}");
            }

            double totalCalories = recipe.CalculateTotalCalories();
            Console.WriteLine($"Total calories: {totalCalories}");

            Console.WriteLine($"Current scale factor: {recipe.ScaleFactor}");
        }

        // Function to display the list of recipes
        static void DisplayRecipeList()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes have been entered yet.");
            }
            else
            {
                Console.WriteLine("List of recipes:");
                recipes.Sort((r1, r2) => string.Compare(r1.Name, r2.Name)); // Sort recipes by name
                for (int i = 0; i < recipes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {recipes[i].Name}");
                }

                Console.Write("Enter the number of the recipe to display: ");
                int recipeIndex = int.Parse(Console.ReadLine()) - 1;
                if (recipeIndex >= 0 && recipeIndex < recipes.Count)
                {
                    currentRecipe = recipes[recipeIndex];
                    DisplayRecipe(currentRecipe);
                }
                else
                {
                    Console.WriteLine("Invalid recipe number.");
                }
            }
        }
    }
}

