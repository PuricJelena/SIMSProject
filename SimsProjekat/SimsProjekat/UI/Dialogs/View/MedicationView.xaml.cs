using SimsProjekat.UI.Dialogs.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimsProjekat.UI.Dialogs.View
{
    /// <summary>
    /// Interaction logic for MedicationView.xaml
    /// </summary>
    public partial class MedicationView : Window
    {
        public MedicationView()
        {
            InitializeComponent();
            DataContext = new MedicationViewModel(this);
        }

     
    }
}
