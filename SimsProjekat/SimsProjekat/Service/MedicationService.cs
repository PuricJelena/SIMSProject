using SimsProjekat.Model;
using SimsProjekat.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Service
{
    public class MedicationService
    {
       
        private MedicationRepository repository = new MedicationRepository();

        public IEnumerable<Entity> SearchService(string term = "")
        {
            return repository.Search(term);
        }

        public IEnumerable<Entity> SearchPriceService(double priceFrom, double priceTo)
        {
            return repository.SearchPrice(priceFrom, priceTo);
        }

        public IEnumerable<Entity> SearchIngredientsService(string term = "")
        {
            return repository.SearchIngredients(term);
        }

        public List<Entity> GetAllForDeletedService(bool deletedMed = false)
        {
            return repository.GetAllForDeleted(deletedMed);
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
