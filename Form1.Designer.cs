namespace AStarGameMap
{
	partial class AStarGameForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.btnBarrier = new System.Windows.Forms.Button();
			this.pnlGameScreen = new System.Windows.Forms.Panel();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnGoal = new System.Windows.Forms.Button();
			this.btnErase = new System.Windows.Forms.Button();
			this.btnClearAll = new System.Windows.Forms.Button();
			this.btnFindPath = new System.Windows.Forms.Button();
			this.btnClearPath = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnBarrier
			// 
			this.btnBarrier.Location = new System.Drawing.Point(12, 61);
			this.btnBarrier.Name = "btnBarrier";
			this.btnBarrier.Size = new System.Drawing.Size(121, 43);
			this.btnBarrier.TabIndex = 1;
			this.btnBarrier.Text = "Draw Barrier";
			this.btnBarrier.UseVisualStyleBackColor = true;
			this.btnBarrier.Click += new System.EventHandler(this.btnBarrier_Click);
			// 
			// pnlGameScreen
			// 
			this.pnlGameScreen.BackColor = System.Drawing.Color.White;
			this.pnlGameScreen.Location = new System.Drawing.Point(143, 11);
			this.pnlGameScreen.Name = "pnlGameScreen";
			this.pnlGameScreen.Size = new System.Drawing.Size(640, 480);
			this.pnlGameScreen.TabIndex = 7;
			this.pnlGameScreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlGameScreen_MouseDown);
			this.pnlGameScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlGameScreen_MouseMove);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(12, 110);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(121, 43);
			this.btnStart.TabIndex = 2;
			this.btnStart.Text = "Starting Point";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnGoal
			// 
			this.btnGoal.Location = new System.Drawing.Point(12, 159);
			this.btnGoal.Name = "btnGoal";
			this.btnGoal.Size = new System.Drawing.Size(121, 43);
			this.btnGoal.TabIndex = 3;
			this.btnGoal.Text = "Goal";
			this.btnGoal.UseVisualStyleBackColor = true;
			this.btnGoal.Click += new System.EventHandler(this.btnGoal_Click);
			// 
			// btnErase
			// 
			this.btnErase.Location = new System.Drawing.Point(12, 12);
			this.btnErase.Name = "btnErase";
			this.btnErase.Size = new System.Drawing.Size(121, 43);
			this.btnErase.TabIndex = 0;
			this.btnErase.Text = "Erase";
			this.btnErase.UseVisualStyleBackColor = true;
			this.btnErase.Click += new System.EventHandler(this.btnErase_Click);
			// 
			// btnClearAll
			// 
			this.btnClearAll.Location = new System.Drawing.Point(12, 306);
			this.btnClearAll.Name = "btnClearAll";
			this.btnClearAll.Size = new System.Drawing.Size(121, 43);
			this.btnClearAll.TabIndex = 6;
			this.btnClearAll.Text = "Clear All";
			this.btnClearAll.UseVisualStyleBackColor = true;
			this.btnClearAll.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// btnFindPath
			// 
			this.btnFindPath.Location = new System.Drawing.Point(12, 208);
			this.btnFindPath.Name = "btnFindPath";
			this.btnFindPath.Size = new System.Drawing.Size(121, 43);
			this.btnFindPath.TabIndex = 4;
			this.btnFindPath.Text = "Find Path";
			this.btnFindPath.UseVisualStyleBackColor = true;
			this.btnFindPath.Click += new System.EventHandler(this.btnFindPath_Click);
			// 
			// btnClearPath
			// 
			this.btnClearPath.Location = new System.Drawing.Point(12, 257);
			this.btnClearPath.Name = "btnClearPath";
			this.btnClearPath.Size = new System.Drawing.Size(121, 43);
			this.btnClearPath.TabIndex = 5;
			this.btnClearPath.Text = "Clear Path";
			this.btnClearPath.UseVisualStyleBackColor = true;
			this.btnClearPath.Click += new System.EventHandler(this.btnClearPath_Click);
			// 
			// AStarGameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1039, 621);
			this.Controls.Add(this.btnClearPath);
			this.Controls.Add(this.btnFindPath);
			this.Controls.Add(this.btnClearAll);
			this.Controls.Add(this.btnErase);
			this.Controls.Add(this.btnGoal);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.pnlGameScreen);
			this.Controls.Add(this.btnBarrier);
			this.Name = "AStarGameForm";
			this.Text = "A* Path Finding In Game";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnBarrier;
		private System.Windows.Forms.Panel pnlGameScreen;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnGoal;
		private System.Windows.Forms.Button btnErase;
		private System.Windows.Forms.Button btnClearAll;
		private System.Windows.Forms.Button btnFindPath;
		private System.Windows.Forms.Button btnClearPath;
	}
}

