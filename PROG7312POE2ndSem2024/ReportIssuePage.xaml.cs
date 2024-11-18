using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace PROG7312POE2ndSem2024
{
    /// <summary>
    /// Interaction logic for ReportIssuePage.xaml
    /// </summary>
    public partial class ReportIssuePage : Page
    {
        private string filePath;

        public ReportIssuePage()
        {
            InitializeComponent();
        }

        // Event to show category selection message
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedText = selectedItem.Content.ToString();
                MessageBox.Show("You selected: " + selectedText);
            }
        }

        // Attach media file
        private void btnAttachMedia_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "Images files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|Document files (*.pdf;*.docx)|*.pdf;*.docx"
            };

            if (openFile.ShowDialog() == true)
            {
                filePath = openFile.FileName;
                MessageBox.Show("File chosen: " + filePath);
            }
        }

        // Submit report
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Collect form inputs
            string location = txtLocation.Text;
            ComboBoxItem chosenCat = comboBox.SelectedItem as ComboBoxItem;
            string category = chosenCat?.Content.ToString() ?? string.Empty;
            string description = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text.Trim();

            // Validate inputs
            if (string.IsNullOrWhiteSpace(location) || string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Please fill in all fields before submitting the report.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Determine priority and create new service request
            int priority = DeterminePriority(category);
            var newRequest = new ServiceRequestData(location, category, description, filePath, priority);

            // Save and provide feedback
            ServiceRequestRepository.SaveServiceRequest(newRequest);
            MessageBox.Show($"Report submitted successfully!\n\nTracking ID: {newRequest.RequestID}\n" +
                            $"Location: {newRequest.Location}\nCategory: {newRequest.Category}\n" +
                            $"Priority: {priority}\nDescription: {newRequest.Description}\nMedia: {newRequest.MediaPath}");

            // Clear form for next submission
            ClearForm();
            MessageBox.Show($"Total Requests Stored: {ServiceRequestRepository.GetServiceRequests().Count}");
        }

        // Method to clear form inputs
        private void ClearForm()
        {
            comboBox.SelectionChanged -= ComboBox_SelectionChanged;

            txtLocation.Clear();
            comboBox.SelectedIndex = -1;
            richTextBox.Document.Blocks.Clear();
            filePath = null;

            comboBox.SelectionChanged += ComboBox_SelectionChanged;
        }

        // Assign priority based on category
        private int DeterminePriority(string category)
        {
            //This switch statements ranks the categories of the issues based on urgency
            //3 being most urgent and need to be fixed ASAP and
            //1 being least urgent and will take some time
            switch (category)
            {
                case "Flooding":
                case "Explosion":
                case "Contaminated Water":
                    return 3;

                case "Broken Traffic Light":
                case "Burst Pipe":
                case "Road Collapse":
                case "Electricity Outage":
                case "No Water":
                case "Gas Leak":
                    return 2;

                case "Basic Roadworks":
                case "potholes":
                case "basic utilities":
                case "minor sanitation problems":
                case "Streetlight Not Working":
                case "Trash Overflow":
                case "Graffiti":
                case "litter":               
                case "Noise pollution":
                    return 1;

                default:
                    return 1;
            }
        }
    }
}
