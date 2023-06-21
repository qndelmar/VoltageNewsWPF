using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using VoltageNews.Models;

namespace VoltageNews.Views
{
    /// <summary>
    /// Interaction logic for Graphs.xaml
    /// </summary>
    public partial class Graphs : Page
    {
        public Graphs()
        {
            InitializeComponent();

            ((BarSeries)mainChart.Series[0]).ItemsSource = ViewsByDate.GetKeyValuePairs();
        }
    }
}
