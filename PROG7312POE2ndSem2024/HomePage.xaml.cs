﻿using System;
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

namespace PROG7312POE2ndSem2024
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private void ServiceImg_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //When user clicks on image they are directed to the next page
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new ReportIssuePage());
        }
        private void imgEvent_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //When user clicks on image they are directed to the next page
            NavigationService nav2 = NavigationService.GetNavigationService(this);
            nav2.Navigate(new LocalEventsPage());
        }
        private void imgStatus_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav3 = NavigationService.GetNavigationService(this);
            nav3.Navigate(new OrderStatus());
            //ShowWarning();
        }
        /*public static void ShowWarning()
        {
            // Display a popup message with a yellow warning triangle in WPF
            MessageBox.Show(
                "Currently under construction", "Warning",MessageBoxButton.OK, MessageBoxImage.Warning // This sets the yellow triangle icon
            );
        }
        */  //Made a class for methods so they are accesible to all the xaml.cs 
        }
}