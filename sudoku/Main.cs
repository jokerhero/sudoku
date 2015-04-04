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
            /*
            int[][] puzzleAsArray = new int[9][];
            puzzleAsArray[0] = new int[] { 0, 5, 7, 1, 0, 3, 8, 0, 0 };
            puzzleAsArray[1] = new int[] { 0, 9, 0, 0, 5, 6, 0, 7, 0 };
            puzzleAsArray[2] = new int[] { 1, 3, 0, 8, 0, 0, 0, 0, 0 };
            puzzleAsArray[3] = new int[] { 2, 0, 0, 9, 0, 0, 0, 0, 7 };
            puzzleAsArray[4] = new int[] { 0, 0, 8, 6, 4, 2, 5, 0, 0 };
            puzzleAsArray[5] = new int[] { 9, 0, 0, 0, 0, 8, 0, 0, 4 };
            puzzleAsArray[6] = new int[] { 0, 0, 0, 0, 0, 7, 0, 5, 8 };
            puzzleAsArray[7] = new int[] { 0, 6, 0, 4, 8, 0, 0, 2, 0 };
            puzzleAsArray[8] = new int[] { 0, 0, 9, 5, 0, 1, 4, 3, 0 };
            Sudoku sud = new Sudoku();
            */
            Sudoku sud = new Sudoku();
                        
            textBox1.Text = Sudoku.solve(sud.puzzleSolution);
        }
    }
}
