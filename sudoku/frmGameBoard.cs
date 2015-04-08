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
    /* tsMainMenu
     * tsNewGame
     * tsControls
     * tsDone
     * tsSave
     * tsExit
     * tsHelp
     * tsSolver
     * tsReset
     * tsHint*/
    public partial class frmGameBoard : Form
    {
        public frmGameBoard()
        {
            InitializeComponent();
        }

        private void tsMainMenu_Click(object sender, EventArgs e)
        {
            frmSudokuMM main = new frmSudokuMM();
            main.Show();
            this.Close();
        }

        private void tsNewGame_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to start a new game?", "Start new game?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                frmNewGame new_game = new frmNewGame();
                new_game.Show();
                this.Close();
            }
        }

        private void tsControls_Click(object sender, EventArgs e)
        {
            frmControls controls = new frmControls();
            controls.Show();
        }

        private void tsExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Black numbers within the grid are clues." + "\n" + "Blue numbers within the grid are entries.");
        }

        private void tsSolver_Click(object sender, EventArgs e)
        {
            frmGameBoard solver = new frmGameBoard();
            solver.Show();
        }
    }
}
