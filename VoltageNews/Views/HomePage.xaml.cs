using System;
using System.Windows.Controls;
using System.Windows.Input;
using VoltageNews.Helpers;
using VoltageNews.Models;
using VoltageNews.ViewModels;

namespace VoltageNews.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            (DataContext as HomePageViewModel).Init();
        }

        private void StackPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp = (StackPanel)sender;
            int id = (sp.DataContext as Article).NewsId;
            PageManager.helpFrame.Navigate(new SoloPost(id));
            PageManager.helpFrame?.RemoveBackEntry();
        }

        private void Pagination_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            (DataContext as HomePageViewModel).Pagination(e.Info, 9);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox_DropDownClosed(object sender, EventArgs e)
        {
            if((sender as ComboBox).Text == "Default")
            {
                (DataContext as HomePageViewModel).Pagination(1, 9);
                return;
            }
            (DataContext as HomePageViewModel).SortData((sender as ComboBox).Text);

        }
    }
}
