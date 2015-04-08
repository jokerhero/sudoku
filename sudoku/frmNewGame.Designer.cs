namespace sudoku
{
    partial class frmNewGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewGame));
            this.txtSelect = new System.Windows.Forms.TextBox();
            this.btnEasy = new System.Windows.Forms.Button();
            this.btnMedium = new System.Windows.Forms.Button();
            this.btnHard = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSelect
            // 
            this.txtSelect.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtSelect.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSelect.Enabled = false;
            this.txtSelect.Font = new System.Drawing.Font("Segoe Marker", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSelect.Location = new System.Drawing.Point(47, 42);
            this.txtSelect.Name = "txtSelect";
            this.txtSelect.ReadOnly = true;
            this.txtSelect.Size = new System.Drawing.Size(267, 38);
            this.txtSelect.TabIndex = 0;
            this.txtSelect.Text = "Select Your Difficulty";
            this.txtSelect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnEasy
            // 
            this.btnEasy.BackColor = System.Drawing.Color.LimeGreen;
            this.btnEasy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEasy.Font = new System.Drawing.Font("Segoe Marker", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEasy.Location = new System.Drawing.Point(103, 115);
            this.btnEasy.Name = "btnEasy";
            this.btnEasy.Size = new System.Drawing.Size(150, 50);
            this.btnEasy.TabIndex = 1;
            this.btnEasy.Text = "Easy";
            this.btnEasy.UseVisualStyleBackColor = false;
            this.btnEasy.Click += new System.EventHandler(this.btnEasy_Click);
            // 
            // btnMedium
            // 
            this.btnMedium.BackColor = System.Drawing.Color.Gold;
            this.btnMedium.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMedium.Font = new System.Drawing.Font("Segoe Marker", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMedium.Location = new System.Drawing.Point(103, 196);
            this.btnMedium.Name = "btnMedium";
            this.btnMedium.Size = new System.Drawing.Size(150, 50);
            this.btnMedium.TabIndex = 2;
            this.btnMedium.Text = "Medium";
            this.btnMedium.UseVisualStyleBackColor = false;
            this.btnMedium.Click += new System.EventHandler(this.btnMedium_Click);
            // 
            // btnHard
            // 
            this.btnHard.BackColor = System.Drawing.Color.Red;
            this.btnHard.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHard.Font = new System.Drawing.Font("Segoe Marker", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHard.Location = new System.Drawing.Point(103, 273);
            this.btnHard.Name = "btnHard";
            this.btnHard.Size = new System.Drawing.Size(150, 50);
            this.btnHard.TabIndex = 3;
            this.btnHard.Text = "Hard";
            this.btnHard.UseVisualStyleBackColor = false;
            this.btnHard.Click += new System.EventHandler(this.btnHard_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Peru;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBack.Font = new System.Drawing.Font("Segoe Marker", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(3, 336);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(50, 20);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmNewGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(361, 359);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnHard);
            this.Controls.Add(this.btnMedium);
            this.Controls.Add(this.btnEasy);
            this.Controls.Add(this.txtSelect);
            this.Name = "frmNewGame";
            this.Text = "New Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSelect;
        private System.Windows.Forms.Button btnEasy;
        private System.Windows.Forms.Button btnMedium;
        private System.Windows.Forms.Button btnHard;
        private System.Windows.Forms.Button btnBack;
    }
}