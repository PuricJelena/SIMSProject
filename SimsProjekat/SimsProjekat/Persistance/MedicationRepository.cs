using SimsProjekat.Model;
using SimsProjekat.UI.Dialogs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Persistance
{
    public class MedicationRepository : Repository<Medication>
    {
        public override IEnumerable<Entity> Search(string term = "")
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in SIMSContext.Instance.Medications)
            {
                if(((Medication)entity).Quantity.ToString().Contains(term) ||
                    ((Medication)entity).Password.Contains(term) ||
                    ((Medication)entity).Name.Contains(term) ||
                    ((Medication)entity).Producer.Contains(term)) 
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public IEnumerable<Entity> SearchPrice(double priceFrom, double priceTo)
        {
            List<Entity> prices = new List<Entity>();

            foreach (Entity entity in SIMSContext.Instance.Medications)
            {
                if (((Medication)entity).Price >= priceFrom && ((Medication)entity).Price <= priceTo)
                {
                    prices.Add(entity);
                }
            }
            return prices;
        }

        public IEnumerable<Entity> SearchIngredients(string term="")
        {
            List<Entity> resultIngredient = new List<Entity>();

            string[] terms = term.Split('|');


            foreach(Entity entity in SIMSContext.Instance.Medications)
            {
                bool found = false;

                foreach (KeyValuePair<Ingredient, double> pair in ((Medication)entity).Ingredients)
                {
                    if (found) 
                    {
                        break;
                    }


                    foreach (string t in terms)
                    {
                        if (pair.Key.Name.ToLower().Contains(t.ToLower()))
                        {
                            resultIngredient.Add(entity);
                            found = true;
                            break;
                        }
                    }
                }
            }


            return resultIngredient;

        }

        public List<Entity> GetAllForDeleted(bool deletedMed = false)
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity medication in SIMSContext.Instance.Medications)
            {
                if (((Medication)medication).deleted == deletedMed)
                {
                    result.Add(medication);
                }
            }

            return result;
        }


    }
}
