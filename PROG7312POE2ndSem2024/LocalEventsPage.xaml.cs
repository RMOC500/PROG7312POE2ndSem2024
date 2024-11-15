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
    /// Interaction logic for LocalEventsPage.xaml
    /// </summary>
    public partial class LocalEventsPage : Page
    {
        //----------------------------------------------------------------------------------------------------------------
        //Declare Variables
        //----------------------------------------------------------------------------------------------------------------

        //Delcaring these variables here makes them accessible to the whole class.
        private SortedDictionary<DateTime, List<Event>> eventsDictionary; //Used to orginize events by their dateTime
        private HashSet<string> categories;  //No 2 categories can be the same so we use a hashSet
        private Queue<Event> searchResultsQueue; //Temporarily store the users filter/Search inputs

        //This dictionary is created to count how many times a user chooses a category
        //This will help me to create the algroithm to analyse user search patterns and preferences.
        private Dictionary<string, int> countCat;

        //----------------------------------------------------------------------------------------------------------------
        //call and run methods
        //----------------------------------------------------------------------------------------------------------------
        public LocalEventsPage()
        {
            InitializeComponent();
            InitializeDataStructures();
            PopulateCategory();

        }

//----------------------------------------------------------------------------------------------------------------
//Create Methods
//----------------------------------------------------------------------------------------------------------------
        private void InitializeDataStructures()
        {
            //Initialize sortedDictionary to store events in Ascending order by Date of the event
            //Date time is key used and List<Event> is the value
            eventsDictionary = new SortedDictionary<DateTime, List<Event>>(); 
            //Initialize hashSet
            categories = new HashSet<string>(); 
            //Stores categories as a unique string
            
            var dummyEvents = new List<Event>//Creates a list to store the dummy data i created
                { //NOTE: some the repetitive tasks for this part of code were created by AI
                    new Event { Date = new DateTime(2024, 11, 15), Description = "Road Works, Strand Street, CityBowl", Category = "Road Works" },
                    new Event { Date = new DateTime(2024, 12, 31), Description = "Fibre Installation, Rondebosch Main", Category = "Fibre Installation" },
                    new Event { Date = new DateTime(2025, 03, 22), Description = "Environmental Cleanup, Clifton 4th Beach", Category = "Environmental" },
                    new Event { Date = new DateTime(2024, 11, 9),  Description = "Community Service Trash cleanup, Constantia", Category = "Environmental" },
                    new Event { Date = new DateTime(2024, 11, 15), Description = "Traffic light repairs, Sea Point", Category = "Road Works" },
                    new Event { Date = new DateTime(2024, 11, 15), Description = "Road closed for cycling race, 1 Arbor Road Mouille Point", Category = "Community Event" },
                    new Event { Date = new DateTime(2024, 11, 15), Description = "Save the ocean event, Melkbos Strand", Category = "Environmental" },
                    new Event { Date = new DateTime(2025, 01, 05), Description = "Road Repairs, Main Road, Camps Bay", Category = "Road Works" },
                    new Event { Date = new DateTime(2025, 01, 20), Description = "Power Line Maintenance, Observatory", Category = "Maintenance" },
                    new Event { Date = new DateTime(2025, 01, 14), Description = "Water Pipe Installation, Woodstock", Category = "Water Works" },
                    new Event { Date = new DateTime(2024, 12, 12), Description = "Park Beautification, Green Point Urban Park", Category = "Beautification" },
                    new Event { Date = new DateTime(2024, 12, 15), Description = "Tree Planting Day, Newlands Forest", Category = "Environmental" },
                    new Event { Date = new DateTime(2024, 12, 22), Description = "Power Maintenance, Kloof Street", Category = "Maintenance" },
                    new Event { Date = new DateTime(2025, 02, 10), Description = "Road Repairs, Victoria Road, Hout Bay", Category = "Road Works" },
                    new Event { Date = new DateTime(2025, 03, 18), Description = "Water Supply Fix, Main Road, Kenilworth", Category = "Water Works" },
                    new Event { Date = new DateTime(2025, 04, 05), Description = "Community Street Market, Observatory", Category = "Community Event" },
                    new Event { Date = new DateTime(2025, 04, 18), Description = "Beach Cleanup, Fish Hoek Beach", Category = "Environmental" },
                    new Event { Date = new DateTime(2025, 04, 25), Description = "Roadworks and Re-paving, Somerset Road, De Waterkant", Category = "Road Works" },
                    new Event { Date = new DateTime(2025, 05, 02), Description = "Fibre Installation, Gardens Area", Category = "Fibre Installation" },
                    new Event { Date = new DateTime(2025, 05, 16), Description = "Community Fundraising Event, Claremont", Category = "Community Event" },
                    new Event { Date = new DateTime(2025, 05, 23), Description = "Park Restoration, Company Gardens", Category = "Beautification" },
                    new Event { Date = new DateTime(2024, 12, 20), Description = "Traffic Signal Maintenance, Mowbray", Category = "Maintenance" },
                    new Event { Date = new DateTime(2025, 01, 30), Description = "Storm Drain Cleanup, Athlone", Category = "Environmental" },
                    new Event { Date = new DateTime(2025, 03, 28), Description = "Footpath Repairs, Wynberg Park", Category = "Road Works" },
                    new Event { Date = new DateTime(2025, 04, 09), Description = "New Traffic Lights, Bellville", Category = "Road Works" },
                    new Event { Date = new DateTime(2025, 06, 01), Description = "Streetlight Maintenance, Sea Point Promenade", Category = "Maintenance" },
                    new Event { Date = new DateTime(2025, 06, 10), Description = "Fibre Cable Maintenance, Foreshore", Category = "Fibre Installation" },
                    new Event { Date = new DateTime(2025, 06, 17), Description = "Water Pipe Repairs, Tamboerskloof", Category = "Water Works" },
                    new Event { Date = new DateTime(2025, 06, 23), Description = "Community Picnic, Kirstenbosch Gardens", Category = "Community Event" },
                    new Event { Date = new DateTime(2025, 06, 30), Description = "Park Cleanup, Rondebosch Common", Category = "Environmental" },
                    new Event { Date = new DateTime(2025, 06, 25), Description = "Road Resurfacing, Main Road, Muizenberg", Category = "Road Works" },
                    new Event { Date = new DateTime(2024, 12, 05), Description = "Park Clean-up, De Waal Park", Category = "Environmental" },
                    new Event { Date = new DateTime(2024, 12, 18), Description = "Power Outage Repairs, Woodstock", Category = "Maintenance" },
                    new Event { Date = new DateTime(2025, 01, 02), Description = "Community New Year Celebration, V&A Waterfront", Category = "Community Event" },
                    new Event { Date = new DateTime(2025, 02, 15), Description = "Pothole Repairs, Liesbeek Parkway", Category = "Road Works" },
                    new Event { Date = new DateTime(2025, 02, 25), Description = "Water Pipeline Maintenance, Rondebosch", Category = "Water Works" },
                    new Event { Date = new DateTime(2025, 03, 05), Description = "Fibre Installation, Newlands Avenue", Category = "Fibre Installation" },
                    new Event { Date = new DateTime(2025, 03, 15), Description = "Heritage Walk Event, Bo-Kaap", Category = "Community Event" },
                    new Event { Date = new DateTime(2025, 04, 12), Description = "Roadworks and Re-paving, Kloof Nek Road", Category = "Road Works" },
                    new Event { Date = new DateTime(2025, 04, 30), Description = "Sidewalk Beautification, Long Street", Category = "Beautification" },
                    new Event { Date = new DateTime(2025, 05, 07), Description = "Streetlight Repair, Milnerton", Category = "Maintenance" },
                    new Event { Date = new DateTime(2025, 05, 15), Description = "Beach Restoration, Blouberg Beach", Category = "Environmental" },
                    new Event { Date = new DateTime(2025, 05, 25), Description = "Traffic Signal Installation, Durbanville", Category = "Road Works" },
                    new Event { Date = new DateTime(2025, 06, 04), Description = "Water Quality Testing, Zandvlei", Category = "Water Works" },
                    new Event { Date = new DateTime(2025, 06, 12), Description = "Fiber Installation, Century City", Category = "Fibre Installation" },
                    new Event { Date = new DateTime(2025, 06, 20), Description = "Park Upgrade, Harold Porter Botanical Gardens", Category = "Beautification" },
                    new Event { Date = new DateTime(2024, 12, 08), Description = "Road Resurfacing, Sir Lowry Road", Category = "Road Works" },
                    new Event { Date = new DateTime(2025, 01, 18), Description = "Environmental Awareness Workshop, Table Mountain", Category = "Environmental" },
                    new Event { Date = new DateTime(2025, 02, 01), Description = "Water Pipe Replacement, Camps Bay", Category = "Water Works" },
                    new Event { Date = new DateTime(2025, 02, 20), Description = "Power Line Repairs, Bellville", Category = "Maintenance" },
                    new Event { Date = new DateTime(2025, 06, 28), Description = "Community Fair, Greenmarket Square", Category = "Community Event" },
                    new Event { Date = new DateTime(2025, 01, 22), Description = "Sidewalk Repairs, Salt River", Category = "Road Works" }
                };


            //for each dateTie key in events list
            foreach (var v in dummyEvents)
            {
                // Verify whether the event date is already a SortedDictionary key.
                // If not, create a new record with a blank list of events for this date.
                if (!eventsDictionary.ContainsKey(v.Date))
                {
                    eventsDictionary[v.Date] = new List<Event>();
                }
                eventsDictionary[v.Date].Add(v);
                // Adds the Events respective categorys to the HashSET.
                categories.Add(v.Category);
            }
            //This initializes countCat after category population
            countCat = new Dictionary<string, int>();

            // set each categories count to zero
            foreach (var category in categories)
            {
                countCat[category] = 0;
            }


            // Display all events on the screen when user first opens Events page
            EventsListView.ItemsSource = dummyEvents;
        }
//----------------------------------------------------------------------------------------------------------------
//Populate the frontend combobox
//----------------------------------------------------------------------------------------------------------------       
        private void PopulateCategory()
        {
            Categorycmb.Items.Add("All Categories"); //Add a All categories category for user to view all events on or without a date
            foreach (var category in categories)
            {
                Categorycmb.Items.Add(category); //Takes each unique category in the HASHSET and adds it to the combo box in frontend
            }
            Categorycmb.SelectedIndex = 0; //Set the default selected item to "All Categories"
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = DateSearch.SelectedDate;
            string selectedCategory = Categorycmb.SelectedItem?.ToString();
            searchResultsQueue = new Queue<Event>();


            //---------------------------------------------------------------
            //Search/filtering functionality
            //---------------------------------------------------------------
            foreach (var date in eventsDictionary.Keys)
            {               
                // skip if date doesnt match user input
                if (selectedDate.HasValue && date.Date != selectedDate.Value.Date)
                {
                    continue;
                }

                foreach (var ev in eventsDictionary[date])
                {
                    // If a category is selected and does not match the event category, skip it
                    if (selectedCategory != "All Categories" &&
                        !ev.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    // If both conditions match, enqueue the event
                    searchResultsQueue.Enqueue(ev);
                }
            }
//-----------------------------------------------------------------------------------------
//Search/Filter RECCOMENDATIONS functionality
//-----------------------------------------------------------------------------------------
            if (selectedCategory != "All Categories" && selectedCategory != null)
            {
                
                if (countCat.ContainsKey(selectedCategory))
                {
                    countCat[selectedCategory]++;
                }
            }

            // POPULATE the ListView with the filtered events results (This is search bar functionailty)
            EventsListView.ItemsSource = searchResultsQueue.ToList();
            //update the recommended category highlight (this is search reccomendations)
            reccomendSearchWithBlu();
        }

        //This method calculates the category that gets clicked the most by the user
        //with this information i can promt that category to change to blue background for reccomendation
        private void reccomendSearchWithBlu()
        {
            // Determine the category with the most clicks
            string mostSelectedCategory = countCat.OrderByDescending(c => c.Value).FirstOrDefault().Key;
            // Set all backgrounds of the ComboBox items to their default (white)
            foreach (var item in Categorycmb.Items)
            {
                ComboBoxItem cmbItem = Categorycmb.ItemContainerGenerator.ContainerFromItem(item) as ComboBoxItem;
                if (cmbItem != null)
                {
                    cmbItem.Background = Brushes.White;
                }
            }
            // Recommend the most selected category by changing its ComboBox item background color to blue
            foreach (var item in Categorycmb.Items)
            {
                if (item.ToString() == mostSelectedCategory)
                {
                    ComboBoxItem cmbItem = Categorycmb.ItemContainerGenerator.ContainerFromItem(item) as ComboBoxItem;
                    if (cmbItem != null)
                    {
                        cmbItem.Background = Brushes.LightBlue; // Change background to blue for recommendation
                    }
                    break;
                }
            }
        }

        //This class Creates an object so each event can be treaded as a seperate entity
        public class Event
        {
            public DateTime Date { get; set; } //Key used for sortedDictionary
            public string Description { get; set; }
            public string Category { get; set; } //Used in hashSet to create unique categories
        }
    }
}
