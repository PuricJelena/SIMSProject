using SimsProjekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Persistance
{
   public class BillRepository : Repository<Bill>
    {
        public override IEnumerable<Entity> Search(string term = "")
        {
            List<Entity> result = new List<Entity>();

            foreach (Entity entity in SIMSContext.Instance.Bills)
            {
                if (((Bill)entity).Password.Contains(term) || ((Bill)entity).Pharmacist.Contains(term))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public double CountBoughtMedication()
        {
            double count = 0;

            foreach (Bill bill in SIMSContext.Instance.Bills)
            {
                if (DateTime.Now >= bill.DateAndTime.AddDays(-7))
                {
                    foreach (KeyValuePair<Medication, double> pair in bill.MedicationsAndTheirQuantity) 
                    {
                        count += pair.Value;
                    }
                }
            }

            return count;
        }
    }
}
