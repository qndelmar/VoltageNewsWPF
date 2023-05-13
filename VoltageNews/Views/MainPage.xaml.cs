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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VoltageNews.Helpers;
using VoltageNews.Models;

namespace VoltageNews.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            frame.Navigate(new HomePage());
            PageManager.helpFrame = frame;
            PageManager.helpFrame.JournalOwnership = JournalOwnership.OwnsJournal;
            PageManager.helpFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            if(UserStore.User.Role < 1)
            {
                graphs.Visibility = Visibility.Collapsed;
                addNews.Visibility = Visibility.Collapsed;
            }
        }
    }
}
