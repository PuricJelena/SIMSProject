using SimsProjekat.CompositeCommon;
using SimsProjekat.Model;
using SimsProjekat.Persistance;
using SimsProjekat.UI.Dialogs.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimsProjekat.UI.Dialogs.ViewModel
{
    public class BuyMedicationViewModel : ViewmodelBase
    {
        private MedicationViewModel medicationViewModel;
        private double quantity;
        private BuyMedication dialog;
        private RelayCommand okButtonCommand;
        private RelayCommand cancelButtonCommand;
        private BillRepository repository = new BillRepository();

        public BuyMedicationViewModel(BuyMedication dialog, MedicationViewModel medicationViewModel)
        {
            this.dialog = dialog;
            this.medicationViewModel = medicationViewModel;
        }

        public double Quantity 
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        public RelayCommand OkButtonCommand
        {
            get
            {
                return okButtonCommand ?? (okButtonCommand = new RelayCommand(param => this.OkButtonCommandExecute(), param => this.CanOkButtonCommandExecute()));
            }
        }

        public RelayCommand CancelButtonCommand
        {
            get
            {
                return cancelButtonCommand ?? (cancelButtonCommand = new RelayCommand(param => this.CancelButtonCommandExecute()));
            }
        }

        public void OkButtonCommandExecute()
        {
            medicationViewModel.SelectedQuantity = Quantity;
      
            dialog.Close();
        }

        public bool CanOkButtonCommandExecute()
        {
            if (quantity > 5 || repository.CountBoughtMedication() > 50)
            {
               
                return false;
            }

            return true;
        }

        public void CancelButtonCommandExecute()
        {
            dialog.Close();
        }
    }
}
