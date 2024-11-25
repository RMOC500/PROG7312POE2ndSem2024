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
    /// Interaction logic for AdminStatus.xaml
    /// </summary>
    public partial class AdminStatus : Page
    {

        private ServiceRequestData thisR; // Holds the current service request being worked on
        public AdminStatus()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the tracking ID user input
            string trackingID = TrackingIDTextBox.Text.Trim();
            // Check if tracking ID input is empty/null
            if (string.IsNullOrWhiteSpace(trackingID))
            {
                MessageBox.Show("Please enter a valid Tracking ID.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Search for the request via the tracking ID my repository
            thisR = ServiceRequestRepository.GetServiceRequests().FirstOrDefault(r => r.RequestID == trackingID);

            if (thisR != null)
            {
                MessageBox.Show($"Request Found!\n\nTracking ID: {thisR.RequestID}\n" +
                                $"Details: {thisR.Description}\nStatus: {thisR.Status}",
                                "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);

                // change status text in cmb box
                StatusComboBox.SelectedItem = StatusComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(i => i.Content.ToString() == thisR.Status);
            }
            else
            {
                MessageBox.Show("No request with this ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                thisR = null; 
            }
        }

        private void UpdateStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (thisR == null)
            {
                MessageBox.Show("Please search for a request before updating the status.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Get the selected status from the combo box
            ComboBoxItem selectedStatus = StatusComboBox.SelectedItem as ComboBoxItem;

            if (selectedStatus == null)
            {
                MessageBox.Show("Please select a status to update.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string newStatus = selectedStatus.Content.ToString();
            thisR.Status = newStatus;

            MessageBox.Show($"Status updated to '{newStatus}' for Tracking ID: {thisR.RequestID}.", "Update Successful", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

        
    }
