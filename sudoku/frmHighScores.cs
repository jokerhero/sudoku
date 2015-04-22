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
            this.Close();
        }

        //Close application
        private void tsExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmHighScores_Load(object sender, EventArgs e)
        {
            List<Sudoku.Highscore> scores = Sudoku.getHighScores();
            foreach (Sudoku.Highscore hs in scores)
            {
                switch (hs.difficulty)
                {
                    case Difficulty.EASY:
                        txtEasyGamesPlayed.Text = hs.count.ToString();
                        int fastest = 0;
                        foreach (Sudoku.Entry entry in hs.entries)
                        {
                            fastest = fastest == 0 ? entry.time : fastest <= entry.time ? fastest : entry.time;
                            String temp = "";
                            DateTime date = new DateTime(entry.Date);
                            temp += date.ToString("d");
                            temp += ": ";
                            temp += entry.time;
                            lstEasy.Items.Add(temp);
                        }
                        txtEasyTime.Text = fastest.ToString();
                        break;
                    case Difficulty.MEDIUM:
                        txtMediumGamesPlayed.Text = hs.count.ToString();
                        fastest = 0;
                        foreach (Sudoku.Entry entry in hs.entries)
                        {
                            fastest = fastest == 0 ? entry.time : fastest <= entry.time ? fastest : entry.time;
                            String temp = "";
                            DateTime date = new DateTime(entry.Date);
                            temp += date.ToString("d");
                            temp += ": ";
                            temp += entry.time;
                            lstMedium.Items.Add(temp);
                        }
                        txtMediumTime.Text = fastest.ToString();
                        break;
                    case Difficulty.HARD:
                        txtHardGamesPlayed.Text = hs.count.ToString();
                        fastest = 0;
                        foreach (Sudoku.Entry entry in hs.entries)
                        {
                            fastest = fastest == 0 ? entry.time : fastest <= entry.time ? fastest : entry.time;
                            String temp = "";
                            DateTime date = new DateTime(entry.Date);
                            temp += date.ToString("d");
                            temp += ": ";
                            temp += entry.time;
                            lstHard.Items.Add(temp);
                        }
                        txtHardTime.Text = fastest.ToString();
                        break;
                }
            }
        }

        private void frmHighScores_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmSudokuMM main = new frmSudokuMM();
            main.Show();
            //this.Close();
        }
    }
}
