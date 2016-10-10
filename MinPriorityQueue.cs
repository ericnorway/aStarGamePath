using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarGameMap
{
	public class MinPriorityQueue
	{
		private MinHeap minHeap;

		public MinPriorityQueue() {
			minHeap = new MinHeap();
		}

		public void Enqueue(Node n) {
			minHeap.Insert(n);
		}

		public Node Peek() {
			return minHeap.GetMin();
		}

		public Node Dequeue() {
			return minHeap.RemoveMin();
		}

		public int Count() {
			return minHeap.Count();
		}
	}
}
