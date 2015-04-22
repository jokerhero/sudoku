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
    public partial class frmNewGame : Form
    {
        private bool showmain = true;

        public frmNewGame()
        {
            InitializeComponent();
        }

        //Goes to Main Menu
        private void btnBack_Click(object sender, EventArgs e)
        {
            showmain = true;
            this.Close();
        }
        
        //Opens easy game
        private void btnEasy_Click(object sender, EventArgs e)
        {
            frmGameBoard game_easy = new frmGameBoard(Difficulty.EASY);
            game_easy.Show();
            showmain = false;
            this.Close();
        }
        //Opens medium game
        private void btnMedium_Click(object sender, EventArgs e)
        {
            frmGameBoard game_medium = new frmGameBoard(Difficulty.MEDIUM);
            game_medium.Show();
            showmain = false;
            this.Close();
        }
        //Opens hard game
        private void btnHard_Click(object sender, EventArgs e)
        {
            frmGameBoard game_hard = new frmGameBoard(Difficulty.HARD);
            game_hard.Show();
            showmain = false;
            this.Close();
        }

        // We need to differentiate between closing because of new game or not
        // We have to default to true to allow top right close to work properly
        private void frmNewGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (showmain)
            {
                frmSudokuMM mainmen = new frmSudokuMM();
                mainmen.Show();
            }
        }
    }
}
