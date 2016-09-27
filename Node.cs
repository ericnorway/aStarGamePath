using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarGameMap
{
	public enum Color
	{
		White,
		Gray,
		Black
	}

	public enum GridObject
	{
		Empty,
		Barrier,
		StartingPoint,
		Goal,
		Path
	}

	public class Node {
		public Point point { get; }
		public double g { get; set; }
		public double f { get; set; }
		public double h { get; set; }
		public Node prev { get; set; }
		public Color color { get; set; }
		public GridObject gridObj { get; set; }

		public Node(Point point) {
			this.point = point;
			f = int.MaxValue;
			g = int.MaxValue;
			h = int.MaxValue;
			prev = null;
			color = Color.White;
			gridObj = GridObject.Empty;
		}

		public double GetHeuristic(Point p2) {
			// If heuristic hasn't been calculated yet
			if (h == int.MaxValue) {
				h = Math.Sqrt(Math.Pow((point.X - p2.X), 2) + Math.Pow((point.Y - p2.Y), 2));
			}

			return h;
		}
	}
}
