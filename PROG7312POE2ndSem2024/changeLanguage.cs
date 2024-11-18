using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PROG7312POE2ndSem2024
{
    public class changeLanguage
    {
        private static bool restartApp = false;
        public static void SetLanguage(string cultureCode)
        {
            // Set the culture for the application
            CultureInfo culture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            
        }
    }
}
