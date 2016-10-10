using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarGameMap
{
	class GameGrid
	{
		private Node[,] grid;
		private int xScale;
		private int yScale;
		private Point startingPoint;
		private Point goal;
		private const int WIDTH_CELLS = 64;
		private const int HEIGHT_CELLS = 48;
		private List<Node> solution;
		private const double SQRT2 = 1.41421356;

		public GameGrid(int x, int y) {
			xScale = x / WIDTH_CELLS;
			yScale = y / HEIGHT_CELLS;
			CreateNewGrid();
			solution = new List<Node>();
		}

		public Point GetTileSize() {
			return new Point(xScale, yScale);
		}

		public void CreateNewGrid() {
			grid = new Node[WIDTH_CELLS, HEIGHT_CELLS];
			for (int i = 0; i < WIDTH_CELLS; i++) {
				for (int j = 0; j < HEIGHT_CELLS; j++) {
					grid[i, j] = new Node(new Point(i,j));
				}
			}

			startingPoint = new Point(-1, -1);
			goal = new Point(-1, -1);
		}

		public Point GetStartingPoint() {
			return new Point(startingPoint.X * xScale, startingPoint.Y * yScale);
		}

		public Point GetGoal() {
			return new Point(goal.X * xScale, goal.Y * yScale);
		}

		public Point UpdateGridCell(Point p, GridObject gridObj) {
			int newX = p.X / xScale;
			int newY = p.Y / yScale;

			// Make sure point is on the grid.
			if (
				newX < 0
				|| newX >= grid.GetLength(0) 
				|| newY < 0
				|| newY >= grid.GetLength(1)
				) {
				throw new ArgumentException(string.Format("Point outside the grid: ({0}, {1})", p.X, p.Y));
			}

			Point newPoint = new Point(newX, newY);

			// Clean up old starting point or goal
			if (newPoint == startingPoint) {
				startingPoint = new Point(-1, -1);
			}
			if (newPoint == goal) {
				goal = new Point(-1, -1);
			}

			// Add the new object to the grid.
			switch (gridObj) {
				case GridObject.Empty:
					// Fall through
				case GridObject.Barrier:
					grid[newX, newY].gridObj = gridObj;
					break;
				case GridObject.StartingPoint:
					startingPoint = newPoint;
					grid[newX, newY].gridObj = gridObj;
					break;
				case GridObject.Goal:
					goal = newPoint;
					grid[newX, newY].gridObj = gridObj;
					break;
				default:
					break;
			}

			// Returns the starting point of the object (not exactly where the user clicked).
			return new Point(newX * xScale, newY * yScale);
		}

		public List<Point> AStarFindPath() {
			if ((startingPoint.X == -1 && startingPoint.Y == -1) 
				|| (goal.X == -1 && goal.Y == -1)) {
				return null;
			}

			ClearPath();

			Direction[] directions = { Direction.N, Direction.NE, Direction.E, Direction.SE, Direction.S, Direction.SW, Direction.W, Direction.NW };

			MinPriorityQueue minQ = new MinPriorityQueue();
			minQ.Enqueue(grid[startingPoint.X, startingPoint.Y]);
			grid[startingPoint.X, startingPoint.Y].color = Color.Gray;
			grid[startingPoint.X, startingPoint.Y].g = 0;
			grid[startingPoint.X, startingPoint.Y].f = 
				grid[startingPoint.X, startingPoint.Y].g + 
				grid[startingPoint.X, startingPoint.Y].GetHeuristic((Point)goal);

			bool goalFound = false;

			while (minQ.Count() > 0) {
				Node temp = minQ.Dequeue();

				// If goal reached
				if (temp.point.X == goal.X && temp.point.Y == goal.Y && temp.f <= minQ.Peek().f) {
					goalFound = true;
					break;
				}

				foreach (Direction d in directions) {
					CheckAdjNode(temp, d, minQ);
				}

				temp.color = Color.Black;
			}

			if (goalFound) {
				BuildSolution(grid[goal.X, goal.Y].prev);
				List<Point> adjustedSolution = new List<Point>();
				foreach (Node n in solution) {
					adjustedSolution.Add(new Point(n.point.X * xScale, n.point.Y * yScale));
				}
				return adjustedSolution;
			}
			return null;
		}

		private void CheckAdjNode(Node current, Direction direction, MinPriorityQueue minQ) {
			Point adjPoint = new Point();
			Double edgeWeight = 1;

			// Get the location of the adjacent node.
			switch (direction) {
				case Direction.N:
					adjPoint.X = current.point.X;
					adjPoint.Y = current.point.Y - 1;
					edgeWeight = 1;
					break;
				case Direction.NE:
					adjPoint.X = current.point.X + 1;
					adjPoint.Y = current.point.Y - 1;
					edgeWeight = SQRT2;
					// Don't allow diagonal moves over a barrier.
					if (current.point.X + 1 >= WIDTH_CELLS
						|| grid[current.point.X + 1, current.point.Y].gridObj == GridObject.Barrier) {
						return;
					} else if (current.point.Y - 1 < 0
						|| grid[current.point.X, current.point.Y - 1].gridObj == GridObject.Barrier) {
						return;
					}
					break;
				case Direction.E:
					adjPoint.X = current.point.X + 1;
					adjPoint.Y = current.point.Y;
					edgeWeight = 1;
					break;
				case Direction.SE:
					adjPoint.X = current.point.X + 1;
					adjPoint.Y = current.point.Y + 1;
					edgeWeight = SQRT2;
					// Don't allow diagonal moves over a barrier.
					if (current.point.X + 1 >= WIDTH_CELLS
						|| grid[current.point.X + 1, current.point.Y].gridObj == GridObject.Barrier) {
						return;
					}
					else if (current.point.Y + 1 >= HEIGHT_CELLS
					  || grid[current.point.X, current.point.Y + 1].gridObj == GridObject.Barrier) {
						return;
					}
					break;
				case Direction.S:
					adjPoint.X = current.point.X;
					adjPoint.Y = current.point.Y + 1;
					edgeWeight = 1;
					break;
				case Direction.SW:
					adjPoint.X = current.point.X - 1;
					adjPoint.Y = current.point.Y + 1;
					edgeWeight = SQRT2;
					// Don't allow diagonal moves over a barrier.
					if (current.point.X - 1 < 0
						|| grid[current.point.X - 1, current.point.Y].gridObj == GridObject.Barrier) {
						return;
					}
					else if (current.point.Y + 1 >= HEIGHT_CELLS
					  || grid[current.point.X, current.point.Y + 1].gridObj == GridObject.Barrier) {
						return;
					}
					break;
				case Direction.W:
					adjPoint.X = current.point.X - 1;
					adjPoint.Y = current.point.Y;
					edgeWeight = 1;
					break;
				case Direction.NW:
					adjPoint.X = current.point.X - 1;
					adjPoint.Y = current.point.Y - 1;
					// Don't allow diagonal moves over a barrier.
					if (current.point.X - 1 < 0
						|| grid[current.point.X - 1, current.point.Y].gridObj == GridObject.Barrier) {
						return;
					}
					else if (current.point.Y - 1 < 0
					  || grid[current.point.X, current.point.Y - 1].gridObj == GridObject.Barrier) {
						return;
					}
					edgeWeight = SQRT2;
					break;
			}

			// If outside the game area
			if (adjPoint.X < 0 || adjPoint.X >= WIDTH_CELLS || adjPoint.Y < 0 || adjPoint.Y >= HEIGHT_CELLS) {
				return;
			}

			// If adjacent node already explored or is a barrier
			if (grid[adjPoint.X, adjPoint.Y].color == Color.Black 
				|| grid[adjPoint.X, adjPoint.Y].gridObj == GridObject.Barrier) {
				return;
			}

			Double tempG = current.g + edgeWeight;
			Double tempF = tempG + grid[adjPoint.X, adjPoint.Y].GetHeuristic((Point)goal);

			if (tempF < grid[adjPoint.X, adjPoint.Y].f) {
				grid[adjPoint.X, adjPoint.Y].f = tempF;
				grid[adjPoint.X, adjPoint.Y].g = tempG;
				grid[adjPoint.X, adjPoint.Y].color = Color.Gray;
				grid[adjPoint.X, adjPoint.Y].prev = current;
				minQ.Enqueue(grid[adjPoint.X, adjPoint.Y]);
			}
		}

		private void BuildSolution(Node n) {
			if (n == null || ( n.point.X == startingPoint.X && n.point.Y == startingPoint.Y ) ) {
				return;
			}
			solution.Add(n);
			BuildSolution(n.prev);
		}

		public void ClearPath() {
			for (int i = 0; i < WIDTH_CELLS; i++) {
				for (int j = 0; j < HEIGHT_CELLS; j++) {
					grid[i, j].color = Color.White;
					grid[i, j].f = int.MaxValue;
					grid[i, j].g = int.MaxValue;
					grid[i, j].h = int.MaxValue;
				}
			}

			solution = new List<Node>();
		}
	}

	public enum Direction
	{
		N,
		NE,
		E,
		SE,
		S,
		SW,
		W,
		NW
	}
}
