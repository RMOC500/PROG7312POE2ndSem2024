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
    /// Interaction logic for OrderStatus.xaml
    /// </summary>
    public partial class OrderStatus : Page
    {
        public OrderStatus()
        {
            InitializeComponent();
        }

       

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Methods.ShowWarning();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Methods.ShowWarning();
        }
    }
}