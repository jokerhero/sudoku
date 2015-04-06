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
        Sudoku sud;
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sud = new Sudoku();
            int[][] puzzle = sud.generatePuzzle(Sudoku.Difficulty.HARD);
                        
            textBox1.Text = sud.getPuzzle();
            button4.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sud = new Sudoku();
            int[][] puzzle = sud.generatePuzzle(Sudoku.Difficulty.MEDIUM);

            textBox1.Text = sud.getPuzzle();
            button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sud = new Sudoku();
            int[][] puzzle = sud.generatePuzzle(Sudoku.Difficulty.EASY);

            textBox1.Text = sud.getPuzzle();
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sud.hint();
            textBox1.Text = sud.getPuzzle();
        }
    }
}
