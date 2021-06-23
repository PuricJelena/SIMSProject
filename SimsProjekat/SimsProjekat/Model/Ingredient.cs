using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Model
{
    public class Ingredient : Entity
    {
        public string name;
        public string description;


        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public override bool Equals(object obj)
        {
            Ingredient ingredient = obj as Ingredient;

            if(ingredient == null)
            {
                return false;
            }

            return name == ingredient.Name;
            
        }
    }
}
