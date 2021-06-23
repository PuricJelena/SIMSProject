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
    public class MedicationViewModel : BaseDialogViewModel
    {
        private MedicationRepository repository = new MedicationRepository();
        private List<ComboData<Ingredient>> ingredients = new List<ComboData<Ingredient>>();
        private double ingredientQuantity;
        private RelayCommand addIngredientCommand;
        private Ingredient ingredient;
        private double priceFrom;
        private double priceTo;
        private RelayCommand searchByPriceCommand;
        private string searchTerm = string.Empty;
        private RelayCommand searchIngredientsCommand;
        public RelayCommand showBuyMedicationCommand;
        private double selectedQuantity;
        private Bill selectedBill = new Bill();
        private RelayCommand finishShoppingCommand;
        private MedicationService medicationService = new MedicationService();

        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                searchTerm = value;
                OnPropertyChanged(nameof(SearchTerm));
                // DoSearch();
            }

        }

        public MedicationViewModel(MedicationView view) : base(view, typeof(Medication))
        {
            Init();
            LoadIngredients();
        }

        public void LoadIngredients()
        {
            foreach (Ingredient ingredient in SIMSContext.Instance.Ingredients)
            {
                ingredients.Add(new ComboData<Ingredient>() { Name = ingredient.name + "  ", Value = ingredient });
            }
        }

        protected override void Init()
        {
        
            if (SIMSContext.Instance.User.UserType == "apotekar")
            {
                Items = new ObservableCollection<Entity>(medicationService.GetAllService());
            }
            else
            {
                Items = new ObservableCollection<Entity>(medicationService.GetAllForDeletedService());
            }
        }

        public RelayCommand FinishShoppingCommand
        {
            get
            {
                return finishShoppingCommand ?? (finishShoppingCommand = new RelayCommand(param => this.FinishShoppingCommandExecute(), param => this.CanFinishShoppingCommandExecute()));
            }
        }

        public RelayCommand ShowBuyMedicationCommand
        {
            get
            {
                return showBuyMedicationCommand ?? (showBuyMedicationCommand = new RelayCommand(param => this.ShowBuyMedicationCommandExecute(), param => this.CanShowBuyMedicationCommandExecute()));
            }
        }

        public RelayCommand SearchByPriceCommand
        {
            get
            {
                return searchByPriceCommand ?? (searchByPriceCommand = new RelayCommand(param => this.SearchByPriceCommandExecute(), param => this.CanSearchByPriceCommandExecute()));
            }
        }

        public RelayCommand SearchIngredientsCommand
        {
            get
            {
                return searchIngredientsCommand ?? (searchIngredientsCommand = new RelayCommand(param => this.SearchIngredientsCommandExecute(), param => this.CanSearchIngredientsCommandExecute()));
            }
        }

        public RelayCommand AddIngredientCommand
        {
            get
            {
                return addIngredientCommand ?? (addIngredientCommand = new RelayCommand(param => this.AddIngredientCommandExecute(), param => this.CanAddIngredientCommandExecute()));
            }
        }

        public void ShowBuyMedicationCommandExecute()
        {
            BuyMedication view = new BuyMedication();
            view.DataContext = new BuyMedicationViewModel(view, this);
            view.ShowDialog();

            if (selectedBill.MedicationsAndTheirQuantity.ContainsKey(((Medication)SelectedItem)))
            {
                selectedBill.MedicationsAndTheirQuantity[((Medication)SelectedItem)] = SelectedQuantity;
            }
            else 
            {
                selectedBill.MedicationsAndTheirQuantity.Add(((Medication)SelectedItem), SelectedQuantity);
            }

            selectedBill.Pharmacist = SIMSContext.Instance.User.Name + " " + SIMSContext.Instance.User.LastName;
            selectedBill.TotalPrice += ((Medication)SelectedItem).Price * selectedQuantity;
            ((Medication)SelectedItem).Quantity = ((Medication)SelectedItem).Quantity - selectedQuantity;


        }

        public bool CanShowBuyMedicationCommandExecute()
        {
            return true;
        }

        public void FinishShoppingCommandExecute()
        {
            int id = 0;

            foreach (Bill bill in SIMSContext.Instance.Bills) 
            {
                int billId = int.Parse(bill.Password);

                if (billId > id) 
                {
                    id = billId;
                }
            }

            selectedBill.Password = (id + 1).ToString();
            selectedBill.DateAndTime = DateTime.Now;
            SIMSContext.Instance.Bills.Add(selectedBill);
            SIMSContext.Instance.Save();
            selectedBill = new Bill();
        }

        public bool CanFinishShoppingCommandExecute()
        {
            return true;
        }

        public Ingredient Ingredient
        {
            get { return ingredient; }
            set { ingredient = value; }
        }

        public double SelectedQuantity
        {
            get { return selectedQuantity; }
            set { selectedQuantity = value; }
        }

        public double PriceFrom
        {
            get { return priceFrom; }
            set { priceFrom = value; }
        }

        public double PriceTo
        {
            get { return priceTo; }
            set { priceTo = value; }
        }

        public List<object> IngredientList 
        {
            get 
            {
                List<object> list = new List<object>();

                if (SelectedItem != null) 
                {
                    foreach (KeyValuePair<Ingredient, double> pair in ((Medication)SelectedItem).Ingredients) 
                    {
                        list.Add(new { Ingredient= pair.Key.name, Quantity = pair.Value });
                    }
                }

                return list;
            }
        }

        public void SearchIngredientsCommandExecute()
        {
            MedicationRepository repository = new MedicationRepository();
            Items = new ObservableCollection<Entity>(medicationService.SearchIngredientsService(SearchTerm));
        }

        public bool CanSearchIngredientsCommandExecute()
        {
            return true;
        }

        public ObservableCollection<IngredientItem> MedicationItems
        {
            get
            {
                ObservableCollection<IngredientItem> medicationItems = new ObservableCollection<IngredientItem>();

                if (SelectedItem == null)
                {
                    return medicationItems;

                }
                if (SelectedItem != null)
                {
                    foreach (KeyValuePair<Ingredient, double> pair in ((Medication)SelectedItem).Ingredients)
                    {
                        medicationItems.Add(new IngredientItem() { Ingredient = pair.Key.name, IngQuantity = pair.Value.ToString() });
                    }
                }

                return medicationItems;
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
                    OnPropertyChanged(nameof(MedicationItems));
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                if (SIMSContext.Instance.User.UserType == "doktor" || SIMSContext.Instance.User.UserType =="pacijent")
                {
                    return false;
                }


                return true;
            }

           
        }

        public bool IsEnabledForPharmacist
        {
            get
            {
                if (SIMSContext.Instance.User.UserType == "apotekar" || SIMSContext.Instance.User.UserType == "doktor")
                {
                    return false;
                }


                return true;
            }
        }

        public bool IsEnabledForPatient
        {
            get
            {
                if (SIMSContext.Instance.User.UserType == "pacijent" || SIMSContext.Instance.User.UserType == "apotekar")
                {
                    return false;
                }


                return true;
            }
        }

        public bool IsEnabledForOnlyPatient
        {
            get
            {
                if (SIMSContext.Instance.User.UserType == "pacijent")
                {
                    return false;
                }


                return true;
            }
        }


        public List<ComboData<Ingredient>> Ingredients
        {
            get { return ingredients; }
            set { ingredients = value; }
        }

        public double IngredientQuantity
        {
            get { return ingredientQuantity; }
            set { ingredientQuantity = value; }
        }

        protected override void OkCommandExecute()
        {
            base.OkCommandExecute();

            SIMSContext.Instance.Medications = new List<Entity>(Items);
            medicationService.SaveService();
            Init();
        }

        protected void AddIngredientCommandExecute()
        {
            if (((Medication)SelectedItem).Ingredients.ContainsKey(Ingredient))
            {
                ((Medication)SelectedItem).Ingredients[Ingredient] = ingredientQuantity;
            }
            else 
            {
                ((Medication)SelectedItem).Ingredients.Add(Ingredient, ingredientQuantity);
            }

            OnPropertyChanged(nameof(IngredientList));
            OnPropertyChanged(nameof(MedicationItems));
            medicationService.SaveService();
        }

        protected bool CanAddIngredientCommandExecute() 
        {
            return Ingredient != null && ingredientQuantity > 0 && SelectedItem != null; 
        }

        protected void SearchByPriceCommandExecute()
        {
            Items = new ObservableCollection<Entity>(medicationService.SearchPriceService(priceFrom, priceTo));
        }

        protected bool CanSearchByPriceCommandExecute()
        {
            return priceTo != priceFrom && priceTo > priceFrom;
        }
        protected override Entity OkAfterAddDatabase()
        {
            return SelectedItem;
        }

        protected override Entity OkAfterEditDatabase()
        {
            medicationService.SaveService();
            return SelectedItem;
        }

        protected override bool DatabaseRemove(Entity item)
        {
            medicationService.RemoveService(item);
            medicationService.SaveService();
            return true;
        }

        protected override void DoSearch()
        {
            Items = new ObservableCollection<Entity>(medicationService.SearchService(Search));
        }

    }
}
