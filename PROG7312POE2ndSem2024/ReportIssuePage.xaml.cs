using Microsoft.Win32;
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
    /// Interaction logic for ReportIssuePage.xaml
    /// </summary>
    public partial class ReportIssuePage : Page
    {
        private string filePath;
        public ReportIssuePage()
        {
            InitializeComponent();
        }

        //Update for part 3 so that it can be cleared after issue was submitted
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Category drop-down list functionality
            ComboBox comboBox = sender as ComboBox;

            if (comboBox.SelectedItem is ComboBoxItem selectedItem) // Check if selectedItem is not null
            {
                string selectedText = selectedItem.Content.ToString();

                // User feedback: Popup message to confirm user's choice
                MessageBox.Show("You selected: " + selectedText);
            }
            else
            {
                // Optionally handle the case when no item is selected
                MessageBox.Show("No category selected.");
            }
        }



        private void btnAttachMedia_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Images files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|Document files (*.pdf;*.docx)|*.pdf;*.docx";
            
            if (openFile.ShowDialog() == true)
            {
                filePath = openFile.FileName;
                //3.User Feedback
                //Confirms the file choice by showing user the file path they chose
                MessageBox.Show("File chosen: " + filePath);
            }
        }
        //Submit the attached media


        //-----------------------------------------------------------------------------------
        //***I updated the code for this button for PART 3***
        //***This is so it can generate output the TrackingID***
        //-----------------------------------------------------------------------------------
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // Save all user input to temporary variables
            string location = txtLocation.Text;
            ComboBoxItem chosenCat = comboBox.SelectedItem as ComboBoxItem;
            string category = chosenCat != null ? chosenCat.Content.ToString() : string.Empty;
            string description = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(location) || string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(description))
            {
                MessageBox.Show("Please fill in all fields before submitting the report.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Create a new ServiceRequest object
            ServiceRequestData newRequest = new ServiceRequestData(location, category, description, filePath);

            // Save the new request to the central repository
            ServiceRequestRepository.SaveServiceRequest(newRequest);

            // Output saved data to the user
            MessageBox.Show($"Report submitted successfully!\n\nTracking ID: {newRequest.RequestID}\n" +
                            $"Location: {newRequest.Location}\nCategory: {newRequest.Category}\n" +
                            $"Description: {newRequest.Description}\nMedia Evidence: {newRequest.MediaPath}");

            // Clear form for new submission
            ClearForm();

            // Debug message to check count in ServiceRequestRepository
            MessageBox.Show($"Total Requests Stored: {ServiceRequestRepository.GetServiceRequests().Count}");
        }



        // Temporary storage for service requests (could be replaced with a database later)
        private static List<ServiceRequestData> serviceRequests = new List<ServiceRequestData>();

        private void SaveServiceRequest(ServiceRequestData request)
        {
            serviceRequests.Add(request); // Add the new request to the list
        }

        // Method to clear the form for another issue to be reported
        //I implemented this in PART 3
        private void ClearForm()
        {
            // Temporarily detach the SelectionChanged event
            comboBox.SelectionChanged -= ComboBox_SelectionChanged;

            txtLocation.Clear(); // Clear location textbox
            comboBox.SelectedIndex = -1; // Reset the dropdown selection
            richTextBox.Document.Blocks.Clear(); // Clear description
            filePath = null; // clear attached media

            // Reattach the SelectionChanged event
            //This stops popup error
            comboBox.SelectionChanged += ComboBox_SelectionChanged;
        }


    }

    }

