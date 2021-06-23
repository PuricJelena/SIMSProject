using SimsProjekat.CompositeCommon;
using SimsProjekat.Model;
using SimsProjekat.Persistance;
using SimsProjekat.Service;
using SimsProjekat.UI.Dialogs.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.UI.Dialogs.ViewModel
{
    public class IngredientViewModel : BaseDialogViewModel
    {
        private IngredientRepository repository = new IngredientRepository();
        private string searchTerm = string.Empty;
        private ObservableCollection<Entity> items;
        private RelayCommand sortIngredientsCommand;
        private IngredientService ingredientService = new IngredientService();
        private RelayCommand searchMedicationCommand;

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

        public ObservableCollection<Entity> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public RelayCommand SortIngredientsCommand
        {
            get
            {
                return sortIngredientsCommand ?? (sortIngredientsCommand = new RelayCommand(param => this.SortIngredientsCommandExecute()));
            }
        }

        public RelayCommand SearchMedicationCommand
        {
            get
            {
                return searchMedicationCommand ?? (searchMedicationCommand = new RelayCommand(param => this.SearchMedicationCommandExecute(), param => this.CanSearchMedicationCommandExecute()));
            }
        }

        public void SearchMedicationCommandExecute()
        {
            IngredientRepository repository = new IngredientRepository();
        }

        public bool CanSearchMedicationCommandExecute()
        {
            return true;
        }

        public void SortIngredientsCommandExecute()
        {
            IngredientRepository repository = new IngredientRepository();
            Items = new ObservableCollection<Entity>(ingredientService.SortService());
        }


        public IngredientViewModel(IngredientView view) : base(view, typeof(Ingredient))
        {
            Init();
        }

        protected override void Init()
        {
            Items = new ObservableCollection<Entity>(ingredientService.GetAllService());
        }

        protected override void OkCommandExecute()
        {
            base.OkCommandExecute();

            SIMSContext.Instance.Ingredients = new List<Entity>(Items);
            ingredientService.SaveService();
            Init();
        }

        protected override Entity OkAfterAddDatabase()
        {
            return SelectedItem;
        }

        protected override Entity OkAfterEditDatabase()
        {
            ingredientService.SaveService();
            return SelectedItem;
        }

        protected override bool DatabaseRemove(Entity item)
        {
            ingredientService.RemoveService(item);
            ingredientService.SaveService();
            return true;
        }

        protected override void DoSearch()
        {
            IngredientRepository repository = new IngredientRepository();
            Items = new ObservableCollection<Entity>(ingredientService.SearchService(SearchTerm));
        }
    }
}
