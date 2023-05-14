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
    }
}
