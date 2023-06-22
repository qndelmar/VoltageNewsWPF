using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media;
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

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if(printDialog.ShowDialog() == true)
            {
                mainChart.Foreground = new SolidColorBrush(Colors.Black);
                printDialog.PrintVisual(mainChart, "Диаграмма");
            }
            mainChart.Foreground = new SolidColorBrush(Colors.White);
        }
    }
}
