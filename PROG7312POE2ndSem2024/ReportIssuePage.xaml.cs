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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Catgory drop down list functionality
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;
            string selectedText = selectedItem.Content.ToString();
            //3.User Feedback
            //PopUp message to confirm users choice. 
            MessageBox.Show("You selected: " + selectedText);
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
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //Save all user input to temp variables
            string location = txtLocation.Text; 
            ComboBoxItem chosenCat = comboBox.SelectedItem as ComboBoxItem;
            //Category can't be null user must choose an option
            string category = chosenCat != null ? chosenCat.Content.ToString() : string.Empty; //conver and store chose as string
            string description = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;


            //Output saved data to user 
            //3.user Feedback

            //I will output using messagebox since database has not yet been created in Part1
            MessageBox.Show($"Location: {location}\nCategory: {category}\nDescription: {description}\nMedia Evidence: {filePath}\n\nReport submitted succesfully!");
        }
    }

    }

