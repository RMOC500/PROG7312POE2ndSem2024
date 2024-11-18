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
        // plus 1 node (status)
        public void AddStatus(string status)
        {
            if (!adjacencyList.ContainsKey(status))
            {
                adjacencyList[status] = new List<string>();
            }
        }

        // plus 1 edge 
        public void AddTransition(string fromStatus, string toStatus)
        {
            if (adjacencyList.ContainsKey(fromStatus) && !adjacencyList[fromStatus].Contains(toStatus))
            {
                adjacencyList[fromStatus].Add(toStatus);
            }
        }

        // check if its a valid transition or not
        public bool IsValidTransition(string fromStatus, string toStatus)
        {
            return adjacencyList.ContainsKey(fromStatus) && adjacencyList[fromStatus].Contains(toStatus);
        }

        //fetch all the valid tranasctions
        public List<string> GetValidTransitions(string status)
        {
            return adjacencyList.ContainsKey(status) ? adjacencyList[status] : new List<string>();
        }
    }
}
    

