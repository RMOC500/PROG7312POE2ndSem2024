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
        private PriorityHeap ph = new PriorityHeap();

        public OrderStatus()
        {
            InitializeComponent();
            LoadServiceRequests(); 
        }

        // put it into a listview
        private void LoadServiceRequests()
        {
            
            ph = new PriorityHeap();

            // put reported issues inside priority queue
            foreach (var request in ServiceRequestRepository.GetServiceRequests())
            {
                ph.Enqueue(request); 
            }

            
            ServiceRequestListView.ItemsSource = ph.getAll();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadServiceRequests(); // refresh listview
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

            var foundRequest = ph.getAll().FirstOrDefault(r => r.RequestID == searchID); //Search throug the heap
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
