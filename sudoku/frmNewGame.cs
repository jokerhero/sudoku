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
        public frmNewGame()
        {
            InitializeComponent();
        }
        //Goes to Main Menu
        private void btnBack_Click(object sender, EventArgs e)
        {
            frmSudokuMM main = new frmSudokuMM();
            main.Show();
            this.Close();
            //Reopens hidden Main Menu
            //this.RefTofrmSudokuMM.Show();
        }
        //Opens easy game
        private void btnEasy_Click(object sender, EventArgs e)
        {
            frmGameBoard game_easy = new frmGameBoard();
            game_easy.Show();
            this.Close();
        }
        //Opens medium game
        private void btnMedium_Click(object sender, EventArgs e)
        {
            frmGameBoard game_medium = new frmGameBoard();
            game_medium.Show();
            this.Close();
        }
        //Opens hard game
        private void btnHard_Click(object sender, EventArgs e)
        {
            frmGameBoard game_hard = new frmGameBoard();
            game_hard.Show();
            this.Close();
        }
    }
}
