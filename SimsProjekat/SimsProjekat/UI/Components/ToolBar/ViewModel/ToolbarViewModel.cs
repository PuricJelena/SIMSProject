using SimsProjekat.CompositeCommon;
using SimsProjekat.Model;
using SimsProjekat.UI.Components.LogIn.ViewModel;
using SimsProjekat.UI.Dialogs.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimsProjekat.UI.Components.ToolBar.ViewModel
{
    public class ToolbarViewModel : ViewmodelBase
    {
        private RelayCommand userCommand;
        private RelayCommand medicationCommand;
        private RelayCommand billCommand;
        private RelayCommand ingredientCommand;
       

        private MainWindowViewModel mainWindowViewModel;
        public MainWindowViewModel MainWindowViewModel
        {
            get { return mainWindowViewModel; }
            set { mainWindowViewModel = value; }
        }

        public RelayCommand UserCommand
        {
            get
            {
                return userCommand ?? (userCommand = new RelayCommand(param => this.UserCommandExecute()));
            }
        }

        public RelayCommand BillCommand
        {
            get
            {
                return billCommand ?? (billCommand = new RelayCommand(param => this.BillCommandExecute()));
            }
        }

        public RelayCommand IngredientCommand
        {
            get
            {
                return ingredientCommand ?? (ingredientCommand = new RelayCommand(param => this.IngredientCommandExecute()));
            }
        }

        public RelayCommand MedicationCommand
        {
            get
            {
                return medicationCommand ?? (medicationCommand = new RelayCommand(param => this.MedicationCommandExecute()));
            }
        }

        public Visibility IsUserVisible 
        {
            get 
            {
                if (SIMSContext.Instance.User.UserType == "doktor")
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility IsBillVisible
        {
            get
            {
                if (SIMSContext.Instance.User.UserType == "pacijent")
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }



        public void UserCommandExecute()
        {
            UserView view = new UserView();
            view.ShowDialog();

        }

        public void BillCommandExecute()
        {
            BillView view = new BillView();
            view.ShowDialog();

        }

        public void IngredientCommandExecute()
        {
            IngredientView view = new IngredientView();
            view.ShowDialog();

        }

        public void MedicationCommandExecute()
        {
            MedicationView view = new MedicationView();
            view.ShowDialog();

        }
    }
}
