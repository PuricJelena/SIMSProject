using SimsProjekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.UI.Dialogs.Model
{
   public  class BillItem : Entity
    {
        private string medication;
        private string medQunatity;

        public string Medication
        {
            get { return medication; }
            set { medication = value; }
        }

        public string MedQuantity
        {
            get { return medQunatity; }
            set { medQunatity = value; }
        }
    }
}
