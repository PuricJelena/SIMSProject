using SimsProjekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Persistance
{
    public class IngredientRepository : Repository<Ingredient>
    {
        public override IEnumerable<Entity> Search(string term = "")
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in SIMSContext.Instance.Ingredients)
            {
                if (((Ingredient)entity).Name.ToLower().Contains(term.ToLower()) || ((Ingredient)entity).Description.ToLower().Contains(term.ToLower())
                    || IngredientInMedication(entity as Ingredient, term))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public bool IngredientInMedication(Ingredient ingredient, string medicationName)
        {

            foreach (Medication medication in SIMSContext.Instance.Medications)
            {
                if (medication.Name.ToLower().Contains(medicationName.ToLower()))
                {
                    if (medication.Ingredients.ContainsKey(ingredient))
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        public IEnumerable<Entity> Sort()
        {
            Dictionary<Ingredient, int> dic = new Dictionary<Ingredient, int>();


            foreach (Entity entity in SIMSContext.Instance.Ingredients)
            {
                dic.Add(entity as Ingredient, CountInMedicament(entity as Ingredient));
            }

            List<Entity> result = new List<Entity>();

            foreach (var item in dic.OrderByDescending(value => value.Value))
            {
                result.Add(item.Key);
            }

            return result;
        }

        public int CountInMedicament(Ingredient ingredient)
        {
            int count = 0;

            foreach (Medication medication in SIMSContext.Instance.Medications) 
            {
                if (medication.Ingredients.ContainsKey(ingredient)) 
                {
                    count++;
                }
            }

            return count;
        }

    }
}
