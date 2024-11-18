using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG7312POE2ndSem2024
{
    public class PriorityHeap
    {
        private List<ServiceRequestData> heap = new List<ServiceRequestData>();

        public void Enqueue(ServiceRequestData request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }

        public List<ServiceRequestData> getAll()
        {
            return heap.OrderByDescending(r => r.Priority).ToList();
        }

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

        private void Switch(int indexA, int indexB)
        {
            var temp = heap[indexA];
            heap[indexA] = heap[indexB];
            heap[indexB] = temp;
        }
    }
}
