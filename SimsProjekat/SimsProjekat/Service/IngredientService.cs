using SimsProjekat.Model;
using SimsProjekat.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Service
{
    public class IngredientService
    {
     
        private IngredientRepository repository = new IngredientRepository();

        public IEnumerable<Entity> SearchService(string term = "")
        {
            return repository.Search(term);
        }

        public bool IngredientInMedicationService(Ingredient ingredient, string medicationName)
        {
            return repository.IngredientInMedication(ingredient, medicationName);
        }

        public IEnumerable<Entity> SortService()
        {
            return repository.Sort();
        }

        public int CountInMedicamentService(Ingredient ingredient)
        {
            return repository.CountInMedicament(ingredient);
        }

        public IEnumerable<Entity> GetAllService()
        {
            return repository.GetAll();
        }

        public void SaveService()
        {
            repository.Save();
        }

        public void RemoveService(Entity entity)
        {
            repository.Remove(entity);
        }



    }

}
