using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sudoku
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sudoku sud = new Sudoku();
            int[][] puzzle = sud.generatePuzzle(Sudoku.Difficulty.HARD);
                        
            textBox1.Text = sud.getPuzzle();
        }
    }
}
