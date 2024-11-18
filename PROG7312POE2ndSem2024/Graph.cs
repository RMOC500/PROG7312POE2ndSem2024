using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312POE2ndSem2024
{
    public class Graph
    {
        private Dictionary<string, List<string>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<string, List<string>>();
        }
        // Add a node (status)
        public void AddStatus(string status)
        {
            if (!adjacencyList.ContainsKey(status))
            {
                adjacencyList[status] = new List<string>();
            }
        }

        // Add an edge (valid transition)
        public void AddTransition(string fromStatus, string toStatus)
        {
            if (adjacencyList.ContainsKey(fromStatus) && !adjacencyList[fromStatus].Contains(toStatus))
            {
                adjacencyList[fromStatus].Add(toStatus);
            }
        }

        // Check if a transition is valid
        public bool IsValidTransition(string fromStatus, string toStatus)
        {
            return adjacencyList.ContainsKey(fromStatus) && adjacencyList[fromStatus].Contains(toStatus);
        }

        // Get all valid transitions from a status
        public List<string> GetValidTransitions(string status)
        {
            return adjacencyList.ContainsKey(status) ? adjacencyList[status] : new List<string>();
        }
    }
}
    

