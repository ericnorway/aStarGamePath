using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarGameMap
{
	public class MinHeap
	{
		private Node[] arr;
		private int size = 16;
		private int count;
		private HashTable hashTable;

		public MinHeap() {
			arr = new Node[size];
			count = 0;
			hashTable = new HashTable();
		}

		public void Insert(Node value) {
			if (hashTable.Find(value.point)) {
				return;
			}

			hashTable.Insert(value.point);

			if (count >= size) {
				size *= 2;
				Node[] temp = new Node[size];
				arr.CopyTo(temp, 0);
				arr = temp;
			}

			int pos = count;

			arr[pos] = value;
			count++;

			int parentPos = (pos - 1) / 2;

			while (pos > 0) {
				if (arr[pos].f < arr[parentPos].f) {
					Swap(pos, parentPos);
					pos = parentPos;
					parentPos = (pos - 1) / 2;
				} else {
					break;
				}
			}
		}

		public Node GetMin() {
			return arr[0];
		}

		public Node RemoveMin() {
			Node temp = arr[0];
			arr[0] = arr[count - 1];
			count--;

			Heapify(0);

			hashTable.Remove(temp.point);

			return temp;
		}

		public int Count() {
			return count;
		}

		private void Heapify(int pos) {
			int leftPos = pos * 2 + 1;
			int rightPos = pos * 2 + 2;
			int minPos = pos;

			if (leftPos <= count && arr[leftPos].f < arr[minPos].f) {
				minPos = leftPos;
			}
			if (rightPos <= count && arr[rightPos].f < arr[minPos].f) {
				minPos = rightPos;
			}

			if (minPos != pos) {
				Swap(pos, minPos);
				Heapify(minPos);
			}
		}

		private void Swap(int p1, int p2) {
			Node temp = arr[p1];
			arr[p1] = arr[p2];
			arr[p2] = temp;
		}
	}
}
