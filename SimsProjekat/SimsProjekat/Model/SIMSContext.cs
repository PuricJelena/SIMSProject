using SimsProjekat.Persistance;
using SimsProjekat.UI.Dialogs.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.Model
{
    public class SIMSContext
    {
        private List<Entity> users;
        private List<Entity> medications;
        private List<Entity> bills;
        private List<Entity> ingredients;
        private List<Entity> loginUsers;
        private List<Entity> billsItems;
        private User user;
        private Bill bill;

        private static SIMSContext instance;

        public SIMSContext() 
        {
            
        }

        public static SIMSContext Instance 
        {
            get 
            {
                if (instance == null) 
                {
                    instance = new SIMSContext();
                    instance.Load();
                }

                return instance;
            }
        }

        public User User 
        {
            get { return user; }
            set { user = value; }
        }


        public Bill Bill
        {
            get { return bill; }
            set { bill = value; }
        }

        public List<Entity> Users
        {
            get { return users; }
            set { users = value; }
        }

        public List<Entity> Medications
        {
            get { return medications; }
            set { medications = value; }
        }

        public List<Entity> Bills
        {
            get { return bills; }
            set { bills = value; }
        }

        public List<Entity> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        public List<Entity> LoginUsers
        {
            get { return loginUsers; }
            set { loginUsers = value; }
        }

        public List<Entity> BillsItems
        {
            get { return billsItems; }
            set { billsItems = value; }
        }

        public List<Entity> Get(Type type)
        {
            if(type == typeof(User))
            {
                return Users;
            }
            
            if(type == typeof(Medication))
            {
                return Medications;
            }

            if(type == typeof(Bill))
            {
                return Bills;
            }
            if(type == typeof(LogInUser))
            {
                return loginUsers;
            }
            if(type == typeof(BillItem))
            {
                return billsItems;
            }    

            return Ingredients;
        }

        public void Set(Type type, List<Entity> entities)
        {
            if(type == typeof(User))
            {
                Users = entities;
                return;
            }    

            if(type == typeof(Medication))
            {
                Medications = entities;
                return;
            }

            if(type == typeof(Bill))
            {
                Bills = entities;
                return;
            }
            if(type == typeof(LogInUser))
            {
                loginUsers = entities;
                return;
            }
            if(type == typeof(BillItem))
            {
                billsItems = entities;
                return;
            }

            Ingredients = entities;
        }

        public void Load() 
        {
            LoadIngredients();
            LoadMedications();
            LoadBills();
            LoadUsers();
            LoadLogInUsers();
         
        }



        public void LoadLogInUsers()
        {
            List<Entity> loginUsersList = new List<Entity>();

            if (!File.Exists("loginUser.txt"))
            {
                loginUsers = loginUsersList;
                return;
            }

            StreamReader reader = new StreamReader("loginUser.txt");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');

                LogInUser loginUser = new LogInUser();

                loginUser.LoginUsername = data[0];
                loginUser.LoginPassword = data[1];
               
                loginUsersList.Add(loginUser);


            }
            reader.Close();
            loginUsers = loginUsersList;
        }

        public void LoadUsers() 
        {
            List<Entity> userList = new List<Entity>();

            if(!File.Exists("user.txt"))
            {
                users = userList;
                return;
            }

            StreamReader reader = new StreamReader("user.txt");
            string line;

            while ((line=reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');

                User user = new User();

                user.Email = data[0];
                user.Password = data[1];
                user.Name = data[2];
                user.LastName = data[3];
                user.PhoneNumber = data[4];
                user.UserType = data[5];
                user.UserId = data[6];
                userList.Add(user);
                

            }
            reader.Close();
            users = userList;
        }

        public void LoadBills()
        {
            List<Entity> billsList = new List<Entity>();

            if (!File.Exists("bill.txt"))
            {
                bills = billsList;
                return;
            }

            StreamReader reader = new StreamReader("bill.txt");
            string line;

            MedicationRepository repository = new MedicationRepository();

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');

                Bill bill = new Bill();

                bill.Password = data[0];
                bill.Pharmacist = data[1];
                bill.DateAndTime = DateTime.Parse(data[2]);
                bill.TotalPrice = double.Parse(data[3]);


                bill.MedicationsAndTheirQuantity = new Dictionary<Medication, double>();

                string[] dicItems = data[4].Split(',');

                foreach (string item in dicItems) 
                {
                    string[] dicData = item.Split(';');

                    bill.MedicationsAndTheirQuantity.Add(repository.Get(dicData[0]) as Medication, double.Parse(dicData[1]));
                }

                billsList.Add(bill);

            }
            reader.Close();
            bills = billsList;

        }

        public void LoadMedications()
        {
            List<Entity> medicationList = new List<Entity>();

            if (!File.Exists("medication.txt"))
            {
                medications = medicationList;
                return;
            }

            IngredientRepository repository = new IngredientRepository();

            StreamReader reader = new StreamReader("medication.txt");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');

                Medication medication = new Medication();

                medication.Password = data[0];
                medication.Name = data[1];
                medication.Producer = data[2];
                medication.Price = double.Parse(data[3]);
                medication.Quantity = double.Parse(data[4]);
                medication.Accepted = bool.Parse(data[5]);
                medication.Deleted = bool.Parse(data[6]);
                medication.Id = data[7];
                medication.Explonation = data[8];

                medication.Ingredients = new Dictionary<Ingredient, double>();

                if (data[9] != string.Empty) 
                {
                    string[] dicItems = data[9].Split(',');

                    foreach (string item in dicItems)
                    {
                        string[] dicData = item.Split(';');

                        medication.Ingredients.Add(repository.Get(dicData[0]) as Ingredient, double.Parse(dicData[1]));
                    }
                }

                medicationList.Add(medication);


            }
            reader.Close();
            medications = medicationList;
        }

        public void LoadIngredients()
        {
            List<Entity> ingredientList = new List<Entity>();

            if (!File.Exists("ingredient.txt"))
            {
                ingredients = ingredientList; 
                return;
            }

            StreamReader reader = new StreamReader("ingredient.txt");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split('|');

                Ingredient ingredient = new Ingredient();

                ingredient.Name = data[0];
                ingredient.Description = data[1];
                ingredient.Id = data[2];
                ingredientList.Add(ingredient);


            }
            reader.Close();
            ingredients = ingredientList;
        }

        public void Save() 
        {
            SaveUsers();
            SaveBills();
            SaveIngredients();
            SaveMedications();
            SaveLoginUsers();
        }


        public void SaveLoginUsers()
        {
            if (loginUsers == null)
            {
                return;
            }

            using (StreamWriter file = new StreamWriter("loginUser.txt"))
            {
                foreach (Entity entity in loginUsers)
                {
                    string line = string.Empty;

                    line += ((LogInUser)entity).LoginUsername + "|";
                    line += ((LogInUser)entity).LoginPassword + "|";
                

                    file.WriteLine(line);
                }
                file.Close();
            }
        }

        public void SaveUsers() 
        {
            if(users == null)
            {
                return;
            }

            using (StreamWriter file = new StreamWriter("user.txt"))
            {
                foreach (Entity entity in users)
                {
                    string line = string.Empty;

                    line += ((User)entity).Email + "|";
                    line += ((User)entity).Password + "|";
                    line += ((User)entity).Name + "|";
                    line += ((User)entity).LastName + "|";
                    line += ((User)entity).PhoneNumber + "|";
                    line += ((User)entity).UserType + "|";
                    line += ((User)entity).UserId + "|";

                    file.WriteLine(line);
                }
                file.Close();
            }    
        }

        public void SaveBills()
        {
            if (bills == null)
            {
                return;
            }

            using (StreamWriter file = new StreamWriter("bill.txt"))
            {
                foreach (Entity entity in bills)
                {
                    string line = string.Empty;

                    line += ((Bill)entity).Password + "|";
                    line += ((Bill)entity).Pharmacist + "|";
                    line += ((Bill)entity).DateAndTime.ToString() + "|";
                    line += ((Bill)entity).TotalPrice.ToString() + "|";

                    string newLine = string.Empty;


                    foreach (KeyValuePair<Medication, double> pair in ((Bill)entity).MedicationsAndTheirQuantity)
                    { 
                        newLine += pair.Key.Id + ";" + pair.Value.ToString() + ",";
                    }

                    if (newLine != string.Empty) 
                    {
                        newLine = newLine.Remove(newLine.Length - 1, 1);
                    }

                    line += newLine;

                    file.WriteLine(line);
                }
                file.Close();
            }
        }

        public void SaveMedications()
        {
            if (medications == null)
            {
                return;
            }

            using (StreamWriter file = new StreamWriter("medication.txt"))
            {
                foreach (Entity entity in medications)
                {
                    string line = string.Empty;

                    line += ((Medication)entity).Password + "|";
                    line += ((Medication)entity).Name + "|";
                    line += ((Medication)entity).Producer + "|";
                    line += ((Medication)entity).Price.ToString() + "|";
                    line += ((Medication)entity).Quantity.ToString() + "|";
                    line += ((Medication)entity).Accepted.ToString() + "|";
                    line += ((Medication)entity).Deleted.ToString() + "|";
                    line += ((Medication)entity).Id + "|";
                    line += ((Medication)entity).Explonation + "|";


                    string newLine = string.Empty;


                    foreach (KeyValuePair<Ingredient, double> pair in ((Medication)entity).Ingredients)
                    {
                        newLine += pair.Key.Id + ";" + pair.Value.ToString() + ",";
                    }

                    if (newLine != string.Empty)
                    {
                        newLine = newLine.Remove(newLine.Length - 1, 1);
                    }

                    line += newLine;


                    file.WriteLine(line);
                }
                file.Close();
            }
        }

        public void SaveIngredients()
        {
            if (ingredients == null)
            {
                return;
            }

            using (StreamWriter file = new StreamWriter("ingredient.txt"))
            {
                foreach (Entity entity in ingredients)
                {
                    string line = string.Empty;

                    line += ((Ingredient)entity).Name + "|";
                    line += ((Ingredient)entity).Description + "|";
                    line += ((Ingredient)entity).Id + "|";
                  

                    file.WriteLine(line);
                }
                file.Close();
            }
        }
    }
}
