using SimsProjekat.CompositeCommon;
using SimsProjekat.Model;
using SimsProjekat.Persistance;
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
    public class PatientViewModel : BaseDialogViewModel
    {
        private UserRepository repository = new UserRepository();
        private string searchTerm = string.Empty;
        private ObservableCollection<Entity> items;
        private List<ComboData<string>> typeOfUsers = new List<ComboData<string>>();




        public List<ComboData<string>> TypeOfUsers
        {
            get { return typeOfUsers; }
            set { typeOfUsers = value; }
        }

        public void LoadUserTypes()
        {
            typeOfUsers.Add(new ComboData<string>() { Name = "doktor", Value = "doktor" });
            typeOfUsers.Add(new ComboData<string>() { Name = "pacijent", Value = "pacijent" });
            typeOfUsers.Add(new ComboData<string>() { Name = "apotekar", Value = "apotekar" });


        }


        public PatientViewModel(PatientView view) : base(view, typeof(User))
        {
            Init();
            LoadUserTypes();
        }

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

        protected override void Init()
        {
            Items = new ObservableCollection<Entity>(repository.GetAll());
        }

        protected override void OkCommandExecute()
        {
            base.OkCommandExecute();

            SIMSContext.Instance.Users = new List<Entity>(Items);
            repository.Save();
            Init();
        }

        protected override Entity OkAfterAddDatabase()
        {
            return SelectedItem;
        }

        protected override Entity OkAfterEditDatabase()
        {
            repository.Save();
            return SelectedItem;
        }

        protected override bool DatabaseRemove(Entity item)
        {
            repository.Remove(item);
            repository.Save();
            return true;
        }

        protected override void DoSearch()
        {
            UserRepository repository = new UserRepository();
            Items = new ObservableCollection<Entity>(repository.Search(SearchTerm));
        }
    }
}
