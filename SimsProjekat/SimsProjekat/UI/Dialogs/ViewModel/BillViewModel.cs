using SimsProjekat.CompositeCommon;
using SimsProjekat.Model;
using SimsProjekat.Persistance;
using SimsProjekat.Service;
using SimsProjekat.UI.Dialogs.Model;
using SimsProjekat.UI.Dialogs.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.UI.Dialogs.ViewModel
{
    public class BillViewModel : BaseDialogViewModel
    {
        private BillRepository repository = new BillRepository();
        private List<ComboData<User>> pharmacists = new List<ComboData<User>>();
        private List<ComboData<Medication>> medications = new List<ComboData<Medication>>();
        private double medicationQuantity;
        private Medication medication;
        private RelayCommand addMedicationCommand;
        private string searchTerm = string.Empty;
        private BillService billService = new BillService();

        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                searchTerm = value;
                OnPropertyChanged(nameof(SearchTerm));
                DoSearch();
            }

        }

        public BillViewModel(BillView view) : base(view, typeof(Bill))
        {
            Init();
            LoadPharmacists();
            LoadMedications();
        }

        public void LoadMedications()
        {
            foreach (Medication medication in SIMSContext.Instance.Medications)
            {
                medications.Add(new ComboData<Medication>() { Name = medication.name + "  ", Value = medication });
            }    
        }

        public void LoadPharmacists() 
        {
            foreach (User user in SIMSContext.Instance.Users) 
            {
                pharmacists.Add(new ComboData<User>() { Name = user.name + " " + user.lastName, Value = user });
            }
        }

        public RelayCommand AddMedicationCommand
        {
            get
            {
                return addMedicationCommand ?? (addMedicationCommand = new RelayCommand(param => this.AddMedicationCommandExecute(), param => this.CanAddMedicationCommandExecute()));
            }
        }

        public double MedicationQuantity
        {
            get { return medicationQuantity; }
            set { medicationQuantity = value; }
        }

        public List<ComboData<User>> Pharmacists 
        {
            get { return pharmacists; }
            set { pharmacists = value; }
        }

        public List<ComboData<Medication>> Medications
        {
            get { return medications; }
            set { medications = value; }
        }

        public Medication Medication
        {
            get { return medication; }
            set { medication = value; }
        }

        protected override void Init()
        {
            Items = new ObservableCollection<Entity>(billService.GetAllService());
        }

        protected override void OkCommandExecute()
        {
            base.OkCommandExecute();

            SIMSContext.Instance.Bills = new List<Entity>(Items);
            billService.SaveService();
            Init();
        }

        public List<object> MedicationList
        {
            get
            {
                List<object> list = new List<object>();

                if (SelectedItem != null)
                {
                    foreach (KeyValuePair<Medication, double> pair in ((Bill)SelectedItem).MedicationsAndTheirQuantity)
                    {
                        list.Add(new { Medication = pair.Key.name, Quantity = pair.Value });
                    }
                }

                return list;
            }
        }



        protected void AddMedicationCommandExecute()
        {
            if (((Bill)SelectedItem).MedicationsAndTheirQuantity.ContainsKey(Medication))
            {
                ((Bill)SelectedItem).MedicationsAndTheirQuantity[Medication] = medicationQuantity;
            }
            else
            {
                ((Bill)SelectedItem).MedicationsAndTheirQuantity.Add(Medication, MedicationQuantity);
                billService.SaveService();
            }

           
            OnPropertyChanged(nameof(MedicationList));
            OnPropertyChanged(nameof(BillItems));
        }

        protected bool CanAddMedicationCommandExecute()
        {
            return SelectedItem != null && Medication != null && medicationQuantity > 0;
        }

        protected override Entity OkAfterAddDatabase()
        {
            return SelectedItem;
        }

        protected override Entity OkAfterEditDatabase()
        {
            billService.SaveService();
            return SelectedItem;
        }

        protected override bool DatabaseRemove(Entity item)
        {
            billService.RemoveService(item);
            billService.SaveService();
            return true;
        }

        public ObservableCollection<BillItem> BillItems
        {
            get
            {
                ObservableCollection<BillItem> billItems = new ObservableCollection<BillItem>();

                if (SelectedItem == null)
                {
                    return billItems;
                   
                }
               if(SelectedItem != null )
                {
                    foreach (KeyValuePair<Medication, double> pair in ((Bill)SelectedItem).MedicationsAndTheirQuantity)
                    {
                        billItems.Add(new  BillItem() { Medication = pair.Key.name, MedQuantity = pair.Value.ToString() });
                    }
                }

                return billItems;
            }
        }

        public override Entity SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                    OnPropertyChanged(nameof(BillItems));
                }
            }
        }

        protected override void DoSearch()
        {
            BillRepository repository = new BillRepository();
            Items = new ObservableCollection<Entity>(billService.SearchService(SearchTerm));
        }
    }
}
