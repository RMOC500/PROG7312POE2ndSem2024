using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312POE2ndSem2024
{
    public static class ServiceRequestRepository
    {
        private static List<ServiceRequestData> serviceRequests = new List<ServiceRequestData>();

        public static void SaveServiceRequest(ServiceRequestData request)
        {
            serviceRequests.Add(request);
        }

        public static List<ServiceRequestData> GetServiceRequests()
        {
            return serviceRequests;
        }
    }

    public class ServiceRequestData
    {
        public string RequestID { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string MediaPath { get; set; }
        public string Status { get; set; }

        public ServiceRequestData(string location, string category, string description, string mediaPath)
        {
            RequestID = Guid.NewGuid().ToString().Substring(0, 8); // Generate unique ID
            Location = location;
            Category = category;
            Description = description;
            MediaPath = mediaPath;
            Status = "Pending"; // Default status
        }
    }
}