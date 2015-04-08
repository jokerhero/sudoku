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
        TextBox[][] puzzleGrid = new TextBox[9][];

        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sud = new Sudoku();
            int[][] puzzle = sud.generatePuzzle(Sudoku.Difficulty.HARD);

            fillGrid(puzzle);

            //textBox1.Text = sud.getPuzzle();
            button4.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sud = new Sudoku();
            int[][] puzzle = sud.generatePuzzle(Sudoku.Difficulty.MEDIUM);

            fillGrid(puzzle);
            button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sud = new Sudoku();
            int[][] puzzle = sud.generatePuzzle(Sudoku.Difficulty.EASY);

            fillGrid(puzzle);
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sud.hint();
            fillGrid(sud.puzzle);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                puzzleGrid[i] = new TextBox[9];
                for (int j = 0; j < 9; j++)
                {
                    puzzleGrid[i][j] = new TextBox();
                    puzzleGrid[i][j].Text = (i+1).ToString();
                    puzzleGrid[i][j].Size = new System.Drawing.Size(20, 20);
                    puzzleGrid[i][j].Location = new Point((i * 20)+30, (j * 20)+30);
                    puzzleGrid[i][j].TextAlign = HorizontalAlignment.Center;
                    this.Controls.Add(puzzleGrid[i][j]);
                }

            }
        }

        private void fillGrid(int[][] puz)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (puz[i][j] > 0)
                    {
                        puzzleGrid[i][j].Text = puz[i][j].ToString();
                    }
                    else
                    {
                        puzzleGrid[i][j].Text = "";
                    }
                }
            }
        }
    }
}
