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


    }
}