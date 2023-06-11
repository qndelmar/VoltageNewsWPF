using System.Windows.Controls;
using VoltageNews.ViewModels;

namespace VoltageNews.Views
{
    /// <summary>
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        public EditPage(int id)
        {
            InitializeComponent();
            (DataContext as EditPageVM).Init(id);
        }
    }
}
