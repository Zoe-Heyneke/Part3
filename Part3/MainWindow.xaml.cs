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
            if (RecipeName == "")
            {
                MessageBox.Show("Empty value. Please enter a Recipe Name.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                //now the user is adding a recipe by entering the name
                addedRecipe = new RecipeIngredient.Recipe { RecName = RecipeName };
                //add this recipe name to the list of new recipes
                newRecipes.Add(addedRecipe);
                //confirm with user
                MessageBox.Show("Recipe Name created. Continue adding the ingredients and steps.");
            }
            /*
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
             
        }

        //error handling reference for numbers
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
    }
}