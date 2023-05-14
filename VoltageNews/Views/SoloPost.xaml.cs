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

namespace VoltageNews.Views
{
    /// <summary>
    /// Interaction logic for SoloPost.xaml
    /// </summary>
    public partial class SoloPost : Page
    {
        private string Id;
        public SoloPost(int id)
        {
            InitializeComponent();
            Id = id.ToString();
            textbl.Text = id.ToString();
        }
    }
}
