using SimsProjekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.UI.Dialogs.Model
{
    public class IngredientItem : Entity
    {
        private string ingredient;
        private string ingQunatity;

        public string Ingredient
        {
            get { return ingredient; }
            set { ingredient = value; }
        }

        public string IngQuantity
        {
            get { return ingQunatity; }
            set { ingQunatity = value; }
        }
    }
}
