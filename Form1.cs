using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStarGameMap
{
	public partial class AStarGameForm : Form
	{
		private GameGrid grid;
		private const int PANEL_WIDTH = 640;
		private const int PANEL_HEIGHT = 480;
		private GridObject currentSelection = GridObject.Empty;
		private Point startingPoint;
		private Point goal;
		private Point tileSize;
		private bool solved = false;
		List<Point> path;

		public AStarGameForm() {
			InitializeComponent();
			startingPoint = new Point(-1, -1);
			goal = new Point(-1, -1);
			pnlGameScreen.Width = PANEL_WIDTH;
			pnlGameScreen.Height = PANEL_HEIGHT;
			grid = new GameGrid(pnlGameScreen.Width, pnlGameScreen.Height);
			tileSize = grid.GetTileSize();
		}

		private void btnBarrier_Click(object sender, EventArgs e) {
			currentSelection = GridObject.Barrier;
		}

		private void btnStart_Click(object sender, EventArgs e) {
			currentSelection = GridObject.StartingPoint;
		}

		private void btnGoal_Click(object sender, EventArgs e) {
			currentSelection = GridObject.Goal;
		}

		private void btnErase_Click(object sender, EventArgs e) {
			currentSelection = GridObject.Empty;
		}

		private void btnClear_Click(object sender, EventArgs e) {
			Graphics graphics = pnlGameScreen.CreateGraphics();
			Rectangle rectangle = new Rectangle(0, 0, pnlGameScreen.Size.Width, pnlGameScreen.Size.Height);
			graphics.FillRectangle(Brushes.White, rectangle);
			grid.CreateNewGrid();
			startingPoint = grid.GetStartingPoint();
			goal = grid.GetGoal();
		}

		private void btnFindPath_Click(object sender, EventArgs e) {
			path = grid.AStarFindPath();

			if (path != null) {
				solved = true;
				foreach (Point p in path) {
					DrawOnPanel(p, GridObject.Path);
				}
			}
		}

		private void btnClearPath_Click(object sender, EventArgs e) {
			ClearPath();
			solved = false;
		}

		private void pnlGameScreen_MouseMove(object sender, MouseEventArgs e) {
			// Only allow dragging mouse to draw barriers or erase.
			// No point in drawing a line of starting points or goals.
			if (
				e.Button == MouseButtons.Left 
				&& (currentSelection == GridObject.Barrier 
					|| currentSelection == GridObject.Empty)
				) {
				Point p;
				try {
					p = grid.UpdateGridCell(e.Location, currentSelection);
				} catch (ArgumentException ex) {
					//MessageBox.Show(ex.ToString());
					return;
				}

				// Update the starting point and goal in case they were drawn over.
				startingPoint = grid.GetStartingPoint();
				goal = grid.GetGoal();

				DrawOnPanel(p, currentSelection);
			}
		}

		private void pnlGameScreen_MouseDown(object sender, MouseEventArgs e) {
			// If user is updating the grid after it's been solved.
			if (solved == true) {
				// Erase the old solution.
				ClearPath();
				solved = false;
			}

			Point p;

			// Draw on a click.
			try {
				p = grid.UpdateGridCell(e.Location, currentSelection);
			} catch (ArgumentException ex) {
				//MessageBox.Show(ex.ToString());
				return;
			}

			// If drawing a starting point and one already exists.
			if (currentSelection == GridObject.StartingPoint && startingPoint.X != -1 && startingPoint.Y != -1) {
				// Erase the old starting point.
				DrawOnPanel(startingPoint, GridObject.Empty);
			}

			// If drawing the goal and one alreadt exists.
			if (currentSelection == GridObject.Goal && goal.X != -1 && goal.Y != -1) {
				// Erase the old goal.
				DrawOnPanel(goal, GridObject.Empty);
			}

			// Update the starting point and goal in case they changed or where drawn over.
			startingPoint = grid.GetStartingPoint();
			goal = grid.GetGoal();
			DrawOnPanel(p, currentSelection);
		}

		private void DrawOnPanel(Point p, GridObject gridObj) {
			Brush brush;

			// Select the brush color based on grid object.
			switch(gridObj) {
				case GridObject.Barrier:
					brush = Brushes.Black;
					break;
				case GridObject.Empty:
					brush = Brushes.White;
					break;
				case GridObject.StartingPoint:
					brush = Brushes.Green;
					break;
				case GridObject.Goal:
					brush = Brushes.Blue;
					break;
				case GridObject.Path:
					brush = Brushes.Red;
					break;
				default:
					brush = Brushes.Gray;
					break;
			}

			Graphics graphics = pnlGameScreen.CreateGraphics();
			Rectangle rectangle = new Rectangle(p.X, p.Y, tileSize.X, tileSize.Y);
			graphics.FillRectangle(brush, rectangle);
		}

		private void ClearPath() {
			if (path == null) {
				return;
			}

			foreach(Point p in path) {
				grid.UpdateGridCell(p, GridObject.Empty);
				DrawOnPanel(p, GridObject.Empty);
			}
		}
	}
}
