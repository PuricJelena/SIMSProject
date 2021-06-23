using SimsProjekat.CompositeCommon;
using SimsProjekat.Model;
using SimsProjekat.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SimsProjekat.UI.Components.LogIn.ViewModel
{
   public class LoginViewModel : ViewmodelBase
    {
        private string username;
        private string password;
        private RelayCommand loginCommand;
        private RelayCommand cancelCommand;
        private Window dialog;
        private PasswordBox passwordBox;
        private MainWindowViewModel mainViewModel;
        int counter = 0;

        public LoginViewModel(Window dialog, PasswordBox passwordBox)
        {
            this.dialog = dialog;
            this.passwordBox = passwordBox;
           
        }

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        #region Command Properties

        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new RelayCommand(param => LoginCommandExecute(), param => CanLoginCommandExecute()));
            }
        }


        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (loginCommand = new RelayCommand(param => CancelCommandExecute(), param => CanCancelCommandExecute()));
            }
        }

        #endregion

        private void LoginCommandExecute()
        {

            UserRepository repository = new UserRepository();
            User user = (User)repository.GetAllForUser(Username, Password);

            if (user == null)
            {
                MessageBox.Show("Wrong username or password!");
                counter++;
                if(counter ==3)
                {
                    System.Windows.Application.Current.Shutdown();
                }

                return;
            }

            SIMSContext.Instance.User = user;

            dialog.Close();
        }

        private bool CanLoginCommandExecute()
        {
            return !string.IsNullOrEmpty(Username);
        }

        private void CancelCommandExecute()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private bool CanCancelCommandExecute()
        {
            return true;
        }

    }
}
