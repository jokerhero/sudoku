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
        Sudoku puzzle;
        TextBox[,] grid = new TextBox[9, 9];

        public frmGameBoard(Difficulty difficulty)
        {
            puzzle = new Sudoku();
            puzzle.generatePuzzle(difficulty);
            InitializeComponent();
            initGrid();
            startTimer();
        }

        public frmGameBoard()
        {
            puzzle = new Sudoku();
            InitializeComponent();
            initGrid();
            startTimer();
        }

        private void startTimer()
        {
            int time = puzzle.startTimer();
            toolStripMenuTimer.Text = "Time: " + time;
        }

        private void initGrid()
        {
            float fontSize = 30;
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = new TextBox();
                    grid[i, j].Text = puzzle.puzzle[i][j] == 0 ? "" : puzzle.puzzle[i][j].ToString();
                    grid[i, j].ForeColor = puzzle.puzzle[i][j] == 0 ? Color.DarkBlue : Color.Black;
                    grid[i, j].Font = puzzle.puzzle[i][j] == 0 ? new Font(grid[i, j].Font.FontFamily,fontSize, FontStyle.Regular) : new Font(grid[i, j].Font.FontFamily, fontSize, FontStyle.Bold);
                    grid[i, j].ReadOnly = true;
                    grid[i, j].TabStop = false;
                    //I would like to change the coloring here to be grid centric as well
                    if (i < 3 && (j < 3 || j > 5) || i > 5 && (j < 3 || j > 5) || (i > 2 && i < 6) && (j > 2 && j < 6))
                    {
                        grid[i, j].BackColor = (i + j) % 2 == 0 ? Color.LightBlue : Color.White;
                    }
                    else
                    {
                        grid[i, j].BackColor = (i + j) % 2 == 0 ? Color.LemonChiffon : Color.White;
                    }
                    
                    //if ((i>2||i<6) && (j>2||j<6))
                    //    grid[i, j].BackColor = (i + j) % 2 == 0 ? Color.LightBlue : Color.White;

                    grid[i, j].Multiline = true;
                    grid[i, j].Size = new Size(50, 50);
                    grid[i, j].Location = new Point(10 + (j*55), 30 + (i*55));
                    grid[i, j].TextAlign = HorizontalAlignment.Center;
                    grid[i, j].MouseDown += new MouseEventHandler(HandleInput);
                    grid[i, j].ShortcutsEnabled = false;
                    grid[i, j].Cursor = Cursors.Arrow;
                    if (puzzle.puzzle[i][j] == 0)
                    {
                        GridSelector selector = new GridSelector(grid[i,j]);
                        selector.Dock = DockStyle.Fill;
                        selector.Font = selector.Font;
                        selector.valueChanged += new EventHandler(HandleValueChanged);
                        selector.Name = "grid";
                        grid[i, j].Controls.Add(selector);
                    }
                    this.Controls.Add(grid[i, j]);
                }
        }

        private void fillGrid()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j].Text = puzzle.puzzle[i][j] == 0 ? "" : puzzle.puzzle[i][j].ToString();
                    if (puzzle.puzzle[i][j] == 0)
                    {
                        GridSelector selector = (GridSelector)grid[i, j].Controls.Find("grid",true)[0];
                        selector.reset();
                        selector.Visible = true;
                    }
                }
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
                frmNewGame new_game = new frmNewGame(this);
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
            closing();
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

        private void HandleInput(object sender, MouseEventArgs e)
        {
            //handle right and left clicks here
            if (e.Button == MouseButtons.Right)
            {
                TextBox textBox = (TextBox)sender;
                if (textBox.ForeColor != Color.Black)
                {
                    GridSelector selector = (GridSelector)textBox.Controls.Find("grid", true)[0];
                    selector.selectedNumber = 0;
                    selector.Visible = true;
                }
            }
        }

        private void frmGameBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            closing();
        }

        public void HandleValueChanged(object sender, EventArgs e)
        {
            //this handles the number in a gridbox changing
            GridSelector selector = (GridSelector) sender;
            int selected = selector.selectedNumber; //needed to prevent cs1690
            if (selected != 0)
            {
                selector.owner.Text = selected.ToString();
                selector.Visible = false;
            }
        }

        private void closing()
        {
            //todo add save
            //this.Close();
        }

        private void tsReset_Click(object sender, EventArgs e)
        {
            Console.Out.WriteLine("Resetting puzzle");
            puzzle.puzzle = puzzle.resetPuzzle();
            fillGrid();
        }

        private void tsDone_Click(object sender, EventArgs e)
        {
            //At this point, we need to check to see if the game is truly complete or not
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i = puzzle.getTimer();
            toolStripMenuTimer.Text = "Time: " + ((i / 60 / 60) < 10 ? "0" + (i / 60 / 60) : (i / 60 / 60).ToString()) + ":" + ((i / 60)<10?"0"+i / 60:(i / 60).ToString()) + ":" + ((i % 60)<10?"0"+i % 60:(i % 60).ToString());
        }

        private void frmGameBoard_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawLine(Pens.DarkGray,172,30,172,520);
            g.DrawLine(Pens.DarkGray, 337, 30, 337, 520);
            g.DrawLine(Pens.DarkGray, 10, 192, 500, 192);
            g.DrawLine(Pens.DarkGray, 10, 357, 500, 357);
        }
    }
}
