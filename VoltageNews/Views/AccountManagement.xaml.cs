using System;
using System.Windows.Controls;
using VoltageNews.ViewModels;

namespace VoltageNews.Views
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class AccountManagement : Page
    {
        public AccountManagement()
        {
            InitializeComponent();
            (DataContext as AccountManagementVM).Category = "Пользователь";
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            (DataContext as AccountManagementVM).Category = (sender as ComboBox).Text;
        }
    }
}
