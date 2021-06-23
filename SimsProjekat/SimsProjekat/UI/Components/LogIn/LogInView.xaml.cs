using SimsProjekat.UI.Components.LogIn.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SimsProjekat.UI.Components.LogIn
{
    /// <summary>
    /// Interaction logic for LogInView.xaml
    /// </summary>
    public partial class LogInView : Window
    {
        private LoginViewModel viewModel;

        public LogInView()
        {
            InitializeComponent();
            viewModel = new LoginViewModel(this, passwordBox);
            DataContext = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if((passwordBox != null))
            {
                ((DataContext as LoginViewModel)).Password = passwordBox.Password;
            }
        }

        public LoginViewModel ViewModel
        {
            get { return viewModel; }
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public void Dispose()
        {
            this.Close();
        }
       
    }
}
