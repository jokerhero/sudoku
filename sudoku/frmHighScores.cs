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
     * tsExit
     * lstEasy
     * lstMedium
     * lstHard*/
    public partial class frmHighScores : Form
    {
        public frmHighScores()
        {
            InitializeComponent();
        }
        //Back to main menu
        private void tsMainMenu_Click_1(object sender, EventArgs e)
        {
            frmSudokuMM main = new frmSudokuMM();
            main.Show();
            this.Close();
        }
        //Close application
        private void tsExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
