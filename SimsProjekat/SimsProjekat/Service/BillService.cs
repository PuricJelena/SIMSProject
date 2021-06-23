using SimsProjekat.Model;
using SimsProjekat.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Service
{
    public class BillService
    {
        
        private BillRepository repository = new BillRepository();

        public  IEnumerable<Entity> SearchService(string term = "")
        {
            return repository.Search(term);
        }

        public double CountBoughtMedicationService()
        {
            return repository.CountBoughtMedication();
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
