using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PROG7312POE2ndSem2024
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Graph StatusGraph { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitializeStatusGraph();
        }

        private void InitializeStatusGraph()
        {
            StatusGraph = new Graph();
            StatusGraph.AddStatus("Pending");
            StatusGraph.AddStatus("Under Review");
            StatusGraph.AddStatus("In Progress");
            StatusGraph.AddStatus("Completed");
            StatusGraph.AddStatus("Rejected");

            StatusGraph.AddTransition("Pending", "Under Review");
            StatusGraph.AddTransition("Under Review", "In Progress");
            StatusGraph.AddTransition("In Progress", "Completed");
            StatusGraph.AddTransition("Under Review", "Rejected");
            StatusGraph.AddTransition("In Progress", "Rejected");
        }
    }
}
    