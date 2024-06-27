using System.Diagnostics;   //link
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;    //link
using System.Windows.Shapes;

namespace Part3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //use list that can only be accessed in the window to save/store all recipes created in this list as objects
        private List<RecipeIngredient.Recipe> newRecipes = new List<RecipeIngredient.Recipe>();
        //when user is adding a new recipe it will be stored to the recipe list
        private RecipeIngredient.Recipe addedRecipe;
        public MainWindow()
        {
            InitializeComponent();
        }

        //menu buttons

        private void RecAdd_Click(object sender, RoutedEventArgs e)
        {
            //when user clicks Add Recipe, allows tab page Add Recipe to show because it is the second index (1)
            RecipePages.SelectedIndex = 1;  
        }

        private void RecView_Click(object sender, RoutedEventArgs e)
        {
            //when user clicks View Recipe, allows tab page View Recipe to show because it is the third index (2)
            RecipePages.SelectedIndex = 2;  
        }

        private void RecFilter_Click(object sender, RoutedEventArgs e)
        {
            //when user clicks Filter Recipe, allows tab page Filter Recipe to show because it is the fourth index (3)
            RecipePages.SelectedIndex = 3;  
        }

        private void BackMenu_Click(object sender, RoutedEventArgs e)
        {
            //when user clicks Back To Menu on other pages, allows Menu page to show because it is the first index (0)
            RecipePages.SelectedIndex = 0;  
        }

        //add recipe buttons
        //1) recipe name
        private void RecName_Click(object sender, RoutedEventArgs e)
        {
            //declare recipe name entered in the textbox by user as a string
            string RecipeName = RecName.Text;
            //error handling
            //if (RecipeName == "")
            if(string.IsNullOrEmpty(RecipeName))
            {
                MessageBox.Show("Empty value. Please enter a Recipe Name.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                //now the user is adding a recipe by entering the name
                addedRecipe = new RecipeIngredient.Recipe 
                { 
                    RecName = RecipeName 
                };
                //add this recipe name to the list of new recipes
                newRecipes.Add(addedRecipe);
                //confirm with user
                MessageBox.Show("Recipe Name created. Continue adding the ingredients of this recipe.");
            }
            /* contains numbers
            if (ValidName(RecipeName))
            {
                MessageBox.Show("Recipe Names cannot contain numbers", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            */
        }

        //2) ingredients
        private void AddIng_Click(object sender, RoutedEventArgs e)
        {
            //declare ingredients
            string IngredientName = IngName.Text;
            double IngredientQuantity = double.Parse(IngQuantity.Text);
            //combobox unit
            string IngredientUnit = string.Empty;   //no user input therefore empty string value
            //is it a comboboxitem (true or false)
            if(SelUnit.SelectedItem is ComboBoxItem selectedUnit)
            {
                //selected combobox unit gets value of combo and converts that value to the string value since the user is choosing from the combobox
                IngredientUnit = selectedUnit.Content.ToString();
            }
            int IngredientCalories = int.Parse(IngCalories.Text);
            //combobox food group
            string IngredientFoodGroup = string.Empty;   //no user input therefore empty string value
            if (SelFoodGroup.SelectedItem is ComboBoxItem selectedFoodGroup)
            {
                //selected combobox food group gets value of combo and converts that value to the string value since the user is choosing from the combobox
                IngredientFoodGroup = selectedFoodGroup.Content.ToString();
            }

            //error handling check for no empty fields
            if (string.IsNullOrEmpty(IngredientName))
            {
                MessageBox.Show("Empty value. Please enter the ingredient name.");
                return;
            }

            //quantity and calories

            if (string.IsNullOrEmpty(IngredientUnit))
            {
                MessageBox.Show("No chosen value. Please choose a Unit of Measurement.");
                return;
            }

            if (string.IsNullOrEmpty(IngredientFoodGroup))
            {
                MessageBox.Show("No chosen value. Please choose a Food Group.");
                return;
            }

            //store each ingredient entered by user in list of ingredients with corresponding variable names
            RecipeIngredient.Ingredients newRecipes = new RecipeIngredient.Ingredients
            {
                IngName = IngredientName,
                Quantity = IngredientQuantity,
                Unit = IngredientUnit,
                Calories = IngredientCalories,
                FoodGroup = IngredientFoodGroup,
            };

            //add entered values to recipe name added by user
            addedRecipe.Ingredients.Add(newRecipes);
            MessageBox.Show("Recipe Ingredients added. Now, continue adding the steps of this ingredient.");

            //clears the boxes for new adding
            IngName.Clear();
            IngQuantity.Clear();
            IngCalories.Clear();    
        }

        //3) steps
        public void AddSteps_Click(object sender, EventArgs e)
        {
            //declare steps
            string stepsEntered = RecSteps.Text;
            if (string.IsNullOrEmpty(stepsEntered))
            {
                MessageBox.Show("No steps entered. Please enter steps for the recipe");
            }
            else
            {
                //add entered steps to steps list to be added to the recipe list
                addedRecipe.Steps.Add(stepsEntered);
                //clear method to clear textblock
                RecSteps.Clear();
                //confirm user
                MessageBox.Show("Recipe Steps added. You have successfully entered a Recipe! Feel free to enter more ingredients or another Recipe or go back to the Menu Page.");
            }

            //clears the boxes for new adding
            RecSteps.Clear();
            IngName.Clear();
        }

        //view recipe buttons
        //1) show recipe names in alphabetical order 
        private void DisplayRec_Click(object sender, RoutedEventArgs e)
        {
            //sort recipe names in alphabetical order from out the recipe list
            var alphaRecipeNames = newRecipes.OrderBy(z => z.RecName).ToList();

            //will display list and refreshes it by first setting content to empty
            alphaRec.Content = string.Empty;

            //show names underneath each other
            for (int i = 0; i < alphaRecipeNames.Count; i++)
            {
                alphaRec.Content += $"({i + 1}) {alphaRecipeNames[i].RecName}\n";
            }
        }

        //2) choose recipe that is in the list
        private void SelRecName_Click(object sender, RoutedEventArgs e)
        {
            string chosen = SelName.Text;
            bool recipeNameSelected = false;

            //clear label since no user input required
            ViewRecDetails.Content = string.Empty;
            foreach(var addedRecName in newRecipes)
            {
                if(addedRecName.RecName == chosen)
                {
                    //if recipe name selected is found show respective details of that recipe name
                    recipeNameSelected = true;
                    string startView = $"Recipe Name: {addedRecName.RecName}\n\n";

                    startView += "Ingredients:\n";
                    foreach(var ingredientViewing in addedRecName.Ingredients)
                    {
                        startView += $"{ingredientViewing.Quantity} {ingredientViewing.Unit} of {ingredientViewing.IngName} \n => " +
                            $"which is equal to {ingredientViewing.Calories} calories " +
                            $"and belongs to the food group of {ingredientViewing.FoodGroup}\n";
                    }

                    //find steps in recipe therefore calling var name because that contains the steps
                    startView += "\nSteps:\n";
                    /*
                    foreach(var stepsViewing in addedRecName.Steps)
                    {
                        startView += $"{stepsViewing}";
                    }
                    */
                    //numbered steps
                    for (int i = 0; i < addedRecName.Steps.Count; i++)
                    {
                        startView += $"{(i + 1)} {addedRecName.Steps[i]}\n";
                    }

                    //show the recipe details in the label by using the startView variable as this enables the print out of details
                    ViewRecDetails.Content = startView;
                    break;
                }
            }
            //if no recipe name selected is found in list then error message will show
            if(!recipeNameSelected)
            {
                MessageBox.Show("The Recipe Name you entered does not exist. Please choose a recipe from the list.");
            }
        }

        //filter recipe
        //each criteria has their own list to save the filtering and show the user their corresponding recipe names
        //1) ingredient name
        private void FilterName_Click(object sender, RoutedEventArgs e)
        {
            string filteredName = FilName.Text;

            if(string.IsNullOrEmpty(FilName.Text))
            {
                MessageBox.Show("No Name entered. Please enter a name to continue filtering");
                return;
            }

            //if recipes match
            bool recipeMatched = false;

            //clear label since no user input required
            FilIngName.Content = string.Empty;

            //create a new list to store recipe names that matched with the user's ingredient
            //List<string> ingredientNamesMatched = new List<string>();

            //look through each recipe that user entered to find ingredient that matches
            foreach(var lookRecipe in newRecipes)
            {
                //look through each ingredient from that recipe(lookRecipe)
                foreach(var ing in lookRecipe.Ingredients)
                {
                    //if ingredient name entered by the user matches with the name in recipe, the recipe's name is added to the new list
                    if(ing.IngName.Equals(filteredName))
                    {
                        //ingredientNamesMatched.Add(lookRecipe.RecName);
                        //true
                        recipeMatched = true;
                        //show recipe names in label that matches filtering
                        FilIngName.Content += $"{lookRecipe.RecName}";
                        break;
                    }
                    /*
                    else
                    {
                        FilIngName.Content += $"No Recipe Name exists containing filtered Ingredient Name given";
                    }
                    */
                }
                //if no recipe name matched
                if (!recipeMatched)
                {
                    FilIngName.Content = $"No Recipe Name exists containing filtered Ingredient Name given";
                }
            }
        }

        //2) food group
        private void FilterForFood_Click(object sender, RoutedEventArgs e)
        {
            string IngredientFoodGroup = string.Empty;   //no user input therefore empty string value
            if (FilFoodGroup.SelectedItem is ComboBoxItem foodGroupFiltered)
            {
                //selected combobox food group gets value of combo and converts that value to the string value since the user is choosing from the combobox
                IngredientFoodGroup = foodGroupFiltered.Content.ToString();
            }

            if (string.IsNullOrEmpty(IngredientFoodGroup))
            {
                MessageBox.Show("No amount of calories entered. Please enter an amount of calories to continue filtering");
                return;
            }

            //if recipes match
            bool recipeMatched = false;

            //clear label since no user input required
            FilFood.Content = string.Empty;

            //create a new list to store recipe calories that matched with the user's calorie number
            //List<string> foodGroupMatched = new List<string>();

            //look through each recipe that user entered to find calorie amount that matches
            foreach (var lookRecipe in newRecipes)
            {
                //look through each ingredient from that recipe(lookRecipe)
                foreach (var foogr in lookRecipe.Ingredients)
                {
                    //if ingredient name entered by the user matches with the name in recipe, the recipe's name is added to the new list
                    if (foogr.FoodGroup.Equals(IngredientFoodGroup))
                    {
                        //foodGroupMatched.Add(lookRecipe.RecName);
                        //true
                        recipeMatched = true;
                        //show recipe names in label that matches amount of calores
                        FilFood.Content += $"{lookRecipe.RecName}";
                    }
                    /*
                    else
                    {
                        FilFood.Content += $"No Recipe Name exists containing filtered Food Group chosen";
                    }
                    */
                }
                //if no recipe name matched
                if (!recipeMatched)
                {
                    FilFood.Content = $"No Recipe Name exists containing filtered Food Group";
                }
            }
        }

        //3) calories
        private void FilterCal_Click(object sender, RoutedEventArgs e)
        {
            //string filteredCals = FilCalories.Text;
            int filteredCals = int.Parse(FilCalories.Text);

            if (string.IsNullOrEmpty(FilCalories.Text))
            {
                MessageBox.Show("No amount of calories entered. Please enter an amount of calories to continue filtering");
                return;
            }

            //if recipes match
            bool recipeMatched = false;

            //clear label since no user input required
            FilCal.Content = string.Empty;

            //create a new list to store recipe calories that matched with the user's calorie number
            //List<string> caloriesMatched = new List<string>();

            //look through each recipe that user entered to find calorie amount that matches
            foreach (var lookRecipe in newRecipes)
            {
                //look through each ingredient from that recipe(lookRecipe)
                foreach (var cals in lookRecipe.Ingredients)
                {
                    //if ingredient name entered by the user matches with the name in recipe, the recipe's name is added to the new list
                    if (cals.Calories.Equals(filteredCals))
                    {
                        //caloriesMatched.Add(lookRecipe.RecName);
                        //true
                        recipeMatched = true;
                        //show recipe names in label that matches amount of calores
                        FilCal.Content += $"{lookRecipe.RecName}";
                    }
                    /*
                    else
                    {
                        FilCal.Content += $"No Recipe Name exists containing filtered amount of Calories given";
                    }
                    */
                }
                //if no recipe name matched
                if (!recipeMatched)
                {
                    FilCal.Content = $"No Recipe Name exists containing filtered Calories";
                }
            }
        }

        //link to github
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true});
            e.Handled = true;
        }

        //error handling reference for numbers
        /*
            private bool ValidName(string name)
        {
            foreach (char c in name)
            {
                if (c <= 30 && c >= 39)
                {
                    return false;
                }
            }
            return true;
        }
        */
    }
}