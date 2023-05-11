using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageNews.Helpers
{
    internal class ChangeWindowSize
    {
        public static void ChangingWindowSize(double width, double height, System.Windows.Window window)
        {
            window.Width = width;
            window.Height = height;
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            window.Top = (screenHeight / 2) - (height / 2);
            window.Left = (screenWidth / 2) - (width / 2);
        }
    }
}
