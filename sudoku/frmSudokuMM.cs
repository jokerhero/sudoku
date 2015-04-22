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
            new_game.Show();
            this.Hide();
        }
        //Opens Solver
        private void btnSolver_Click(object sender, EventArgs e)
        {
            frmGameBoard solver = new frmGameBoard();
            solver.Show();
            this.Hide();
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
            // We should actually present a window with all games available
            // That window can have a last game played button
            Sudoku sudoku = new Sudoku();
            List<long> games = Sudoku.getGames();
            if (games.Count > 0)
            {
                long gameToLoad = games[0];
                Console.Out.WriteLine("Loading game: " + gameToLoad);
                sudoku.loadGame(gameToLoad);
                frmGameBoard frmGame = new frmGameBoard(sudoku);
                frmGame.Show();
                this.Hide();
            }
            else
            {
                String message = "There are no pending games";
                String caption = "No Games";
                MessageBox.Show(message, caption);
            }
        }

        private void frmSudokuMM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Console.Out.WriteLine("Closing Application");
                Application.Exit();
            }
        }
    }
}
