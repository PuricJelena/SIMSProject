using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Model
{
    public class Medication : Entity
    {
        public string password;
        public string name;
        public string producer;
        public double price;
        public double quantity;
        public bool accepted;
        public bool deleted;
        public Dictionary<Ingredient, double> ingredients = new Dictionary<Ingredient, double>();
        public string explonation;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Producer
        {
            get { return producer; }
            set { producer = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public bool Accepted
        {
            get { return accepted; }
            set { accepted = value; }
        }

        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }

        public Dictionary<Ingredient, double> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        public string Explonation
        {
            get { return explonation; }
            set { explonation = value; }
        }
    }
}
