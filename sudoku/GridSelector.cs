using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sudoku
{
    public partial class GridSelector : UserControl
    {
        public int selectedNumber = 0;
        public event EventHandler valueChanged;
        public TextBox owner;

        public GridSelector(TextBox owner)
        {
            InitializeComponent();
            this.reset();
            this.owner = owner;
        }

        public void reset()
        {
            selectedNumber = 0;
            foreach (Label lab in this.Controls)
            {
                lab.Text = "";
                lab.Name = "";
            }
        }

        private void label_Click(object sender, MouseEventArgs e)
        {
            //do stuff
            //we know the sender is a label
            Label temp = (Label)sender;
            if (e.Button == MouseButtons.Left)
            {
                //on left click eneable/disable numbers
                if (temp.Text == "" || temp.ForeColor == Color.DarkGray)
                {
                    temp.Name = "chosen";
                    temp.Text = temp.Tag.ToString();
                    temp.ForeColor = Color.Black;
                }
                else
                {
                    temp.Text = "";
                    temp.Name = "";
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                //Console.Out.WriteLine("Right clicked on: " + temp.Tag);
                //on right click select number
                if (temp.Text != "")
                {
                    selectedNumber = Convert.ToInt32(temp.Tag);
                    if (this.valueChanged != null)
                        this.valueChanged(this, new EventArgs());
                }
            }
        }

        private void label_hover(object sender, EventArgs e)
        {
            Label lab = (Label)sender;
            if (lab.Text == "" && lab.Name == "")
            {
                lab.Text = lab.Tag.ToString();
                lab.ForeColor = Color.DarkGray;
                lab.Name = "over";
            }
        }

        private void label_Leave(object sender, EventArgs e)
        {
            Label lab = (Label)sender;
            if (lab.Name == "over")
            {
                lab.Name = "";
                lab.Text = "";
                lab.ForeColor = Color.Black;
            }
        }
    }
}
