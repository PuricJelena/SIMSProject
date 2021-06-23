using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimsProjekat.Persistance;
using SimsProjekat.Model;
using System.Collections.ObjectModel;
using SimsProjekat.UI.Dialogs.View;
using SimsProjekat.UI.Dialogs.Model;
using SimsProjekat.CompositeCommon.Enums;
using System.Windows;
using SimsProjekat.Service;

namespace SimsProjekat.UI.Dialogs.ViewModel
{
    public class UserViewModel : BaseDialogViewModel
    {
        private UserRepository repository = new UserRepository();
        private string searchTerm = string.Empty;
        private ObservableCollection<Entity> items;
        private List<ComboData<string>> typeOfUsers = new List<ComboData<string>>();
        private UserService userService = new UserService();

    


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


        public UserViewModel(UserView view) : base(view, typeof(User))
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

        public Visibility UserTypeVIsible 
        {
            get
            {
                if (SIMSContext.Instance.User.UserType == "doktor") 
                {
                    return Visibility.Collapsed;
                }

                return Visibility.Visible;
            }
        }


        protected override void AddCommandExecute()
        {
            SelectedItem = GetInstance();

            if (SIMSContext.Instance.User.UserType == "doktor") 
            {
                ((User)SelectedItem).UserType = "pacijent";
            }

            DialogState = DialogState.Add;
        }


        protected override void Init()
        {
            Items = new ObservableCollection<Entity>(userService.GetAllService());
          
                if(SIMSContext.Instance.User.UserType == "doktor")
                {
                    Items = new ObservableCollection<Entity>(userService.GetAllForUserTypeService("pacijent"));
                }
            
        }

        protected override bool OkAfterAdd()
        {

            if (OkAfterAddDatabase() == null)
            {
                return false;
            }

            SIMSContext.Instance.Users.Add(SelectedItem);
            return true;
        }

        protected override void OkCommandExecute()
        {
            base.OkCommandExecute();

            userService.SaveService();
            Init();
        }

        protected override Entity OkAfterAddDatabase()
        {
            User selectedUser = SelectedItem as User;
            foreach (User user in SIMSContext.Instance.Users)
            {
               if(selectedUser.Email == user.Email || selectedUser.UserId == user.UserId)
                {
                    MessageBox.Show("This user already exists!");
                    SelectedItem = null;
                }
            }
            return SelectedItem;
        }

        protected override Entity OkAfterEditDatabase()
        {
            userService.SaveService();
            return SelectedItem;
        }

        protected override bool DatabaseRemove(Entity item)
        {
            userService.RemoveService(item);
            userService.SaveService();
            return true;
        }

        protected override void DoSearch()
        {
            UserRepository repository = new UserRepository();
            Items = new ObservableCollection<Entity>(userService.SearchService(SearchTerm));
        }
    }
}
