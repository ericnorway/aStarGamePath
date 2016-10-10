using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarGameMap
{
	public class HashTable
	{
		private int size = 8;
		private int count = 0;
		LinkedList<Point>[] list;

		public HashTable() {
			list = new LinkedList<Point>[size];
		}

		public void Insert(Point p) {
			int pos = GetPosition(p, size);

			if (list[pos] == null) {
				list[pos] = new LinkedList<Point>();
			}

			list[pos].AddLast(p);
			count++;

			if (count >= size * 3) {
				GrowList();
			}
		}

		public bool Find(Point p) {
			int pos = GetPosition(p, size);

			if (list[pos] == null || list[pos].Find(p) == null) {
				return false;
			}

			return true;
		}

		public void Remove(Point p) {
			int pos = GetPosition(p, size);

			if (list[pos] == null) {
				return;
			}

			list[pos].Remove(p);
		}

		private int GetPosition(Point p, int arraySize) {
			return Math.Abs((p.GetHashCode() % arraySize));
		}

		private void GrowList() {
			int tempSize = size * 2;
			LinkedList<Point>[] temp = new LinkedList<Point>[tempSize];

			for (int i = 0; i < size; i++) {
				if (list[i] != null) {
					foreach (Point p in list[i]) {
						int pos = GetPosition(p, tempSize);

						if (temp[pos] == null) {
							temp[pos] = new LinkedList<Point>();
						}

						temp[pos].AddLast(p);
					}
				}
			}

			list = temp;
			size = tempSize;
		}
	}
}
