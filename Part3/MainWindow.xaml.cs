using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
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
                { RecName = RecipeName };
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
                IngredientUnit = selectedFoodGroup.Content.ToString();
            }

            //error handling check for no empty fields
            if (string.IsNullOrEmpty(IngredientName))
            {
                MessageBox.Show("Empty value. Please enter the ingredient name.");
                return;
            }

      

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

            //store each ingredient entered by user in list of ingredients
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
            MessageBox.Show("Recipe Ingredients added. Now, continue adding the steps");

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
                MessageBox.Show("Recipe Steps added. You have successfully entered a Recipe! Feel free to enter another Recipe or go back to the Menu Page.");
            }

            //clears the boxes for new adding
            RecSteps.Clear();
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
                alphaRec.Content += $"({i + 1}) {alphaRecipeNames[i].RecName}";
            }
        }

        //2) choose recipe that is in the list
        private void SelRecName_Click(object sender, RoutedEventArgs e)
        {
            string chosen = SelName.Text;

            ViewRecDetails.Content = string.Empty;
            foreach(var addedRecName in newRecipes)
            {
                if(addedRecName.RecName == chosen)
                {
                    ViewRecDetails.Content += $"Recipe Name: {newRecipes.RecName}\n\nIngredients:\n {Ingredients.IngName} {} {} : {} calories and belongs to the food group {}\n\nSteps:\n{}";
                }
            }
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