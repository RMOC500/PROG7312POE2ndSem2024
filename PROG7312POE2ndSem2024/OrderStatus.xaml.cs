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

namespace PROG7312POE2ndSem2024
{
    /// <summary>
    /// Interaction logic for OrderStatus.xaml
    /// </summary>
    public partial class OrderStatus : Page
    {
        private BinaryTrees requestTree = new BinaryTrees(); // Instantiate the BST

        public OrderStatus()
        {
            InitializeComponent();
            LoadServiceRequests(); // Load requests on page load
        }

        // Load all service requests into the ListView
        private void LoadServiceRequests()
        {
            // Clear the tree before reloading
            requestTree = new BinaryTrees();

            // Insert all service requests into the tree
            foreach (var request in ServiceRequestRepository.GetServiceRequests())
            {
                requestTree.Insert(request);
            }

            // Use in-order traversal to display data in sorted order
            ServiceRequestListView.ItemsSource = requestTree.InOrderTraversal();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadServiceRequests(); // Refresh the ListView with updated data
            MessageBox.Show("List refreshed.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchID = SearchTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchID))
            {
                MessageBox.Show("Please enter a Tracking ID.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var foundRequest = requestTree.Search(searchID);
            if (foundRequest != null)
            {
                MessageBox.Show($"Request Found!\n\nTracking ID: {foundRequest.RequestID}\n" +
                                $"Details: {foundRequest.Description}\nStatus: {foundRequest.Status}",
                                "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No request found with the given Tracking ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
