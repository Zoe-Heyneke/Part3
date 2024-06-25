using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3
{
    class RecipeIngredient
    {
        //ingredient class
        public class Ingredients
        {
            //initialize variables with get and set methods to be accessed
            public string IngName { get; set; }
            public double Quantity { get; set; }
            public string Unit { get; set; }
            public int Calories { get; set; }
            public string FoodGroup { get; set; }
        }

        //recipe class
        public class Recipe
        {
            //ingredients and steps data saved in the generic collection: list 
            public string RecName { get; set; }
            public List<Ingredients> Ingredients { get; set;} = new List<Ingredients>();
            public List<string> Steps { get; set; } = new List<string>();
        }
    }
}
