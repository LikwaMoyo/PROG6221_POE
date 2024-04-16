using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221_POE
{
    internal class Program
    {
        static Recipe currentRecipe;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to the Recipe Application!");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Enter a new recipe");
                Console.WriteLine("2. Display the current recipe");
                Console.WriteLine("3. Scale the recipe");
                Console.WriteLine("4. Reset the recipe quantities");
                Console.WriteLine("5. Clear the recipe data");
                Console.WriteLine("6. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        currentRecipe = GetRecipeDetails();
                        break;
                    case 2:
                        if (currentRecipe != null)
                        {
                            DisplayRecipe(currentRecipe);
                        }
                        else
                        {
                            Console.WriteLine("No recipe has been entered yet.");
                        }
                        break;
                    case 3:
                        if (currentRecipe != null)
                        {
                            Console.WriteLine("Enter the scale factor (0.5, 2, or 3):");
                            double factor = double.Parse(Console.ReadLine());
                            currentRecipe.ScaleRecipe(factor);
                            DisplayRecipe(currentRecipe);
                        }
                        else
                        {
                            Console.WriteLine("No recipe has been entered yet.");
                        }
                        break;
                    case 4:
                        if (currentRecipe != null)
                        {
                            currentRecipe.ResetQuantities();
                            DisplayRecipe(currentRecipe);
                        }
                        else
                        {
                            Console.WriteLine("No recipe has been entered yet.");
                        }
                        break;
                    case 5:
                        currentRecipe = null;
                        Console.WriteLine("Recipe data has been cleared.");
                        break;
                    case 6:
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

        // Function to display the recipe
        static void DisplayRecipe(Recipe recipe)
        {
            Console.WriteLine("Recipe:");
            Console.WriteLine($"Number of ingredients: {recipe.NumIngredients}");

            // Display the details of each ingredient
            for (int i = 0; i < recipe.NumIngredients; i++)
            {
                Ingredient ingredient = recipe.Ingredients[i];
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
            }

            Console.WriteLine($"Number of steps: {recipe.NumSteps}");

            // Display the details of each step
            for (int i = 0; i < recipe.NumSteps; i++)
            {
                Step step = recipe.Steps[i];
                Console.WriteLine($"{i + 1}. {step.Description}");
            }

            Console.WriteLine($"Current scale factor: {recipe._scaleFactor}");
        }
    }
}

    