using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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
            if(UserStore.User?.Role < 1)
            {
                graphs.Visibility = Visibility.Collapsed;
                addNews.Visibility = Visibility.Collapsed;
            }
        }
    }
}
