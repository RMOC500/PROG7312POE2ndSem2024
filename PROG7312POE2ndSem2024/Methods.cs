using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PROG7312POE2ndSem2024
{
    internal class Methods
    {
        public static void ShowWarning()
        {
            // Display a popup message with a yellow warning triangle in WPF
            MessageBox.Show(
                "Currently under construction", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning // This sets the yellow triangle icon
            );
        }

    }
}
