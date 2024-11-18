using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312POE2ndSem2024
{
    public class BinaryTrees
    {
        //  this is the node in the binary tree
        private class Node
        {
            public ServiceRequestData Request; // Stores the data
            public Node Left, Right;

            public Node(ServiceRequestData request)
            {
                Request = request;
                Left = Right = null; // make children equal to null
            }
        }

        private Node root; // Root node of the binary tree

        public void Insert(ServiceRequestData request) 
        {
            root = Insert(root, request); 
        }

        private Node Insert(Node node, ServiceRequestData request)
        {
            if (node == null) return new Node(request); // if spot empyty create a new node

            int comparison = string.Compare(request.RequestID, node.Request.RequestID, StringComparison.Ordinal);
            if (comparison < 0) node.Left = Insert(node.Left, request);
            else if (comparison > 0) node.Right = Insert(node.Right, request);

            return node;
        }

        // Search for a request in the tree via Tracking ID
        public ServiceRequestData Search(string requestId)
        {
            return Search(root, requestId);
        }

        private ServiceRequestData Search(Node node, string requestId)
        {
            if (node == null) return null;

            int comparison = string.Compare(requestId, node.Request.RequestID, StringComparison.Ordinal);
            if (comparison == 0) return node.Request;
            if (comparison < 0) return Search(node.Left, requestId); // Search left
            return Search(node.Right, requestId);// Search right
        }

        public List<ServiceRequestData> InOrderTraversal()
        {
            var requests = new List<ServiceRequestData>();
            InOrderTraversal(root, requests);
            return requests;
        }

        private void InOrderTraversal(Node node, List<ServiceRequestData> requests)
        {
            if (node == null) return;

            InOrderTraversal(node.Left, requests);
            requests.Add(node.Request);
            InOrderTraversal(node.Right, requests);
        }
    
}
}
