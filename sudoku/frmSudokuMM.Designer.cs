namespace sudoku
{
    partial class frmSudokuMM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSudokuMM));
            this.txtSudoku = new System.Windows.Forms.TextBox();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnSolver = new System.Windows.Forms.Button();
            this.btnHighScores = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSudoku
            // 
            this.txtSudoku.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSudoku.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSudoku.Enabled = false;
            this.txtSudoku.Font = new System.Drawing.Font("Segoe Marker", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSudoku.Location = new System.Drawing.Point(79, 45);
            this.txtSudoku.Name = "txtSudoku";
            this.txtSudoku.ReadOnly = true;
            this.txtSudoku.Size = new System.Drawing.Size(201, 38);
            this.txtSudoku.TabIndex = 0;
            this.txtSudoku.Text = "Sudoku";
            this.txtSudoku.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnNewGame
            // 
            this.btnNewGame.BackColor = System.Drawing.Color.Peru;
            this.btnNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNewGame.Font = new System.Drawing.Font("Segoe Marker", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewGame.Location = new System.Drawing.Point(101, 95);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(150, 50);
            this.btnNewGame.TabIndex = 1;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = false;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.BackColor = System.Drawing.Color.Peru;
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnContinue.Font = new System.Drawing.Font("Segoe Marker", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContinue.Location = new System.Drawing.Point(101, 156);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(150, 50);
            this.btnContinue.TabIndex = 2;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnSolver
            // 
            this.btnSolver.BackColor = System.Drawing.Color.Peru;
            this.btnSolver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSolver.Font = new System.Drawing.Font("Segoe Marker", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolver.Location = new System.Drawing.Point(101, 217);
            this.btnSolver.Name = "btnSolver";
            this.btnSolver.Size = new System.Drawing.Size(150, 50);
            this.btnSolver.TabIndex = 3;
            this.btnSolver.Text = "Solver";
            this.btnSolver.UseVisualStyleBackColor = false;
            this.btnSolver.Click += new System.EventHandler(this.btnSolver_Click);
            // 
            // btnHighScores
            // 
            this.btnHighScores.BackColor = System.Drawing.Color.Peru;
            this.btnHighScores.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHighScores.Font = new System.Drawing.Font("Segoe Marker", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHighScores.Location = new System.Drawing.Point(101, 277);
            this.btnHighScores.Name = "btnHighScores";
            this.btnHighScores.Size = new System.Drawing.Size(150, 50);
            this.btnHighScores.TabIndex = 4;
            this.btnHighScores.Text = "High Scores";
            this.btnHighScores.UseVisualStyleBackColor = false;
            this.btnHighScores.Click += new System.EventHandler(this.btnHighScores_Click);
            // 
            // frmSudokuMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(359, 365);
            this.Controls.Add(this.btnHighScores);
            this.Controls.Add(this.btnSolver);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.txtSudoku);
            this.Name = "frmSudokuMM";
            this.Text = "Sudoku";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSudoku;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnSolver;
        private System.Windows.Forms.Button btnHighScores;
    }
}

