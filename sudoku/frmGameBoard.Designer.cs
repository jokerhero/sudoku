namespace sudoku
{
    partial class frmGameBoard
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.tsControls = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDone = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSolver = new System.Windows.Forms.ToolStripMenuItem();
            this.tsReset = new System.Windows.Forms.ToolStripMenuItem();
            this.tsHint = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.tsHelp,
            this.tsSolver,
            this.tsReset,
            this.tsHint});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(644, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMainMenu,
            this.tsNewGame,
            this.tsControls,
            this.tsDone,
            this.tsSave,
            this.tsExit});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // tsMainMenu
            // 
            this.tsMainMenu.Name = "tsMainMenu";
            this.tsMainMenu.Size = new System.Drawing.Size(152, 22);
            this.tsMainMenu.Text = "Main Menu";
            this.tsMainMenu.Click += new System.EventHandler(this.tsMainMenu_Click);
            // 
            // tsNewGame
            // 
            this.tsNewGame.Name = "tsNewGame";
            this.tsNewGame.Size = new System.Drawing.Size(152, 22);
            this.tsNewGame.Text = "New Game";
            this.tsNewGame.Click += new System.EventHandler(this.tsNewGame_Click);
            // 
            // tsControls
            // 
            this.tsControls.Name = "tsControls";
            this.tsControls.Size = new System.Drawing.Size(152, 22);
            this.tsControls.Text = "Controls";
            this.tsControls.Click += new System.EventHandler(this.tsControls_Click);
            // 
            // tsDone
            // 
            this.tsDone.Name = "tsDone";
            this.tsDone.Size = new System.Drawing.Size(152, 22);
            this.tsDone.Text = "Done";
            // 
            // tsSave
            // 
            this.tsSave.Name = "tsSave";
            this.tsSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsSave.Size = new System.Drawing.Size(152, 22);
            this.tsSave.Text = "Save";
            // 
            // tsExit
            // 
            this.tsExit.Name = "tsExit";
            this.tsExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.tsExit.Size = new System.Drawing.Size(152, 22);
            this.tsExit.Text = "Exit";
            this.tsExit.Click += new System.EventHandler(this.tsExit_Click);
            // 
            // tsHelp
            // 
            this.tsHelp.Name = "tsHelp";
            this.tsHelp.Size = new System.Drawing.Size(44, 20);
            this.tsHelp.Text = "Help";
            this.tsHelp.Click += new System.EventHandler(this.tsHelp_Click);
            // 
            // tsSolver
            // 
            this.tsSolver.Name = "tsSolver";
            this.tsSolver.Size = new System.Drawing.Size(51, 20);
            this.tsSolver.Text = "Solver";
            this.tsSolver.Click += new System.EventHandler(this.tsSolver_Click);
            // 
            // tsReset
            // 
            this.tsReset.Name = "tsReset";
            this.tsReset.Size = new System.Drawing.Size(47, 20);
            this.tsReset.Text = "Reset";
            // 
            // tsHint
            // 
            this.tsHint.Name = "tsHint";
            this.tsHint.Size = new System.Drawing.Size(42, 20);
            this.tsHint.Text = "Hint";
            // 
            // frmMediumGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 522);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMediumGame";
            this.Text = "Sudoku";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsMainMenu;
        private System.Windows.Forms.ToolStripMenuItem tsNewGame;
        private System.Windows.Forms.ToolStripMenuItem tsControls;
        private System.Windows.Forms.ToolStripMenuItem tsHelp;
        private System.Windows.Forms.ToolStripMenuItem tsSolver;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem tsDone;
        private System.Windows.Forms.ToolStripMenuItem tsSave;
        private System.Windows.Forms.ToolStripMenuItem tsExit;
        private System.Windows.Forms.ToolStripMenuItem tsReset;
        private System.Windows.Forms.ToolStripMenuItem tsHint;
    }
}