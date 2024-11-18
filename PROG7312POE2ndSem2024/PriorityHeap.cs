using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312POE2ndSem2024
{
    public class PriorityHeap
    {
        // this list represents the heap
        private List<ServiceRequestData> heap = new List<ServiceRequestData>();

        // add a reported issue to the heap
        public void Enqueue(ServiceRequestData request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }

        // get all the issue and sort them by urgency
        public List<ServiceRequestData> getAll()
        {
            return heap.OrderByDescending(r => r.Priority).ToList();
        }

        // this method pyhyically moves the row down if its a higher ugrency rating
        private void HeapifyUp(int i)
        {
            while (i > 0)
            {
                int parentIndex = (i - 1) / 2;
                if (heap[i].Priority > heap[parentIndex].Priority)
                {
                    Switch(i, parentIndex);
                    i = parentIndex;
                }
                else break;
            }
        }
        // this method pyhyically moves the row down if its lower ugrency rating
        private void HeapifyDown(int index)
        {
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            int largest = index;

            if (leftChild < heap.Count && heap[leftChild].Priority > heap[largest].Priority)
                largest = leftChild;

            if (rightChild < heap.Count && heap[rightChild].Priority > heap[largest].Priority)
                largest = rightChild;

            if (largest != index)
            {
                Switch(index, largest);
                HeapifyDown(largest);
            }
        }

        // This is used to complete the swap of 2 issues in the list
        private void Switch(int indexA, int indexB)
        {
            var temp = heap[indexA];
            heap[indexA] = heap[indexB];
            heap[indexB] = temp;
        }
    }
}
