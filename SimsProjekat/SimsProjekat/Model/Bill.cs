using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Model
{
    public class Bill : Entity
    {
        public string password;
        public string pharmacist;
        public DateTime dateAndTime;
        public Dictionary<Medication, double> medicationsAndTheirQuantity = new Dictionary<Medication, double>();
        public double totalPrice;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Pharmacist
        {
            get { return pharmacist; }
            set { pharmacist = value; }
        }

        public DateTime DateAndTime
        {
            get { return dateAndTime; }
            set { dateAndTime  = value; }
        }

        public Dictionary<Medication, double> MedicationsAndTheirQuantity
        {
            get { return medicationsAndTheirQuantity; }
            set { medicationsAndTheirQuantity = value; }
        }

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }


    }
}
