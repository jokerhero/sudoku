using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudoku
{
    public partial class frmSudokuMM : Form
    {
        public frmSudokuMM()
        {
            InitializeComponent();
        //    this.FormClosing += frmSudokuMM.FormClosing;
        }
        //Make a reference to this form
        //public Form RefTofrmSudokuMM { get; set;}

        //Opens New Game
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            frmNewGame new_game = new frmNewGame();
            //Gives reference to new window and hide this while opening next
            //new_game.RefTofrmSudokuMM = this;
            //this.Visible = false;
            new_game.Show();
            this.Hide();
        }
        //Opens Solver
        private void btnSolver_Click(object sender, EventArgs e)
        {
            frmGameBoard solver = new frmGameBoard();
            solver.Show();
        }
        //Opens High Scores
        private void btnHighScores_Click(object sender, EventArgs e)
        {
            frmHighScores scores = new frmHighScores();
            scores.Show();
            this.Hide();
        }
        //Continue last saved game
        private void btnContinue_Click(object sender, EventArgs e)
        {

        }
        //Upon clicking [X] application will ask if wanting to close and gives options.
        //private void frmSudokuMM_Closing(object sender, FormClosingEventArgs e)
        //{ 
        //DialogResult result = MessageBox.Show("Do you wish to close?", string.Empty, MessageBoxButtons.YesNo);
        //if (result == DialogResult.No)
        //{ 
        //    e.Cancel = true;
        //}
        //}
    }
}
