/*
 This solver is based on Solver Foundation provided by Microsoft.
 The necessary library must be installed from:
 https://msdn.microsoft.com/en-US/devlabs/hh145003
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SolverFoundation.Services;
using System.Data.SQLite;

namespace sudoku
{
    class Sudoku
    {
        public enum Difficulty
        {
            EASY,
            MEDIUM,
            HARD
        }

        public class Highscore
        {
            public long Date;
            public String Difficulty;
            public int time;
        }

        int[][] puzzleSolution { get; set; }
        public int[][] puzzle { get; set; }
        private int[][] origPuzzle { get; set; }
        private int totalTime { get; set; }         //the total time taken so far - seconds
        private int currentTime { get; set; }       //time this session - seconds
        private Difficulty selectedDifficulty {get; set; }

        private SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=Sudoku.sqlite;Version=3;");

        public Sudoku()
        {
            puzzleSolution = copy(generateInitial());
            //writeArrayToConsole(puzzleSolution);
        }

        public String getPuzzle()
        {
            String ret = "";
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    ret += puzzle[i][j];
                ret += Environment.NewLine;
            }
            return ret;
        }

        public int[][] hint()
        {
            //randomly pick a 0 and set it to the real value
            //or fix an incorrect entry
            int[][] puz = copy(puzzle);

            //writeArrayToConsole(puzzleSolution);

            int count = 0;
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (puz[i][j] == 0 || puz[i][j]!=puzzleSolution[i][j])
                        count += 1;
            if (count == 0)
            {
                //Console.WriteLine("No hints to give.");
                return puzzle;
            }

            int t = randomNum(1, count);

            count = 0;
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (puz[i][j] == 0 || puz[i][j] != puzzleSolution[i][j])
                    {
                        count += 1;
                        if (count == t)
                        {
                            //Console.WriteLine("Hint: " + puzzleSolution[i][j]);
                            puz[i][j] = puzzleSolution[i][j];
                            puzzle = copy(puz);
                        }
                    }

            return puzzle;
        }

        public int[][] resetPuzzle()
        {
            puzzle = copy(origPuzzle);
            return puzzle;
        }

        public bool checkSolution()
        {
            return checkSolution(puzzle);
        }

        private bool checkSolution(int[][] toCheck)
        {
            //return true for a valid solution and false for an invalid solution
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (toCheck[i][j] != puzzleSolution[i][j])
                        return false;

            return true;
        }

        public int[][] generatePuzzle(Difficulty difficulty)
        {
            //I understand this is not the best measurement for difficulty.
            //I chose this as it is the fastest way to generate puzzles on the fly
            //with a reasonable expectation of difficulty

            //We need to remove numbers until we have our desired difficulty
            //On hard, we remove all of one number and start from there
            puzzle = copy(puzzleSolution);
            selectedDifficulty = difficulty;

            //writeArrayToConsole(puzzleSolution);

            switch (difficulty)
            {
                case Difficulty.EASY:
                    puzzle = copy(generateDifficulty(40));
                    break;
                case Difficulty.MEDIUM:
                    puzzle = copy(generateDifficulty(45));
                    break;
                case Difficulty.HARD:
                    //first we will remove a number, this in itself will leave a solveable puzzle
                    puzzle = copy(removeRandomNumberAll(puzzle));
                    puzzle = copy(generateDifficulty(50));
                    break;
            }
            //save for reset if needed
            origPuzzle = copy(puzzle);

            //writeArrayToConsole(puzzleSolution);

            return puzzle;
        }

        private int[][] generateDifficulty(int wantedCells)
        {
            int[][] puz = copy(puzzleSolution);
            int emptyCells = 0;

            bool[][] puzTried = new bool[9][];
            for (int i = 0; i < 9; i++)
            {
                puzTried[i] = new bool[9];
                for (int j = 0; j < 9; j++)
                {
                    puzTried[i][j] = false;
                }
            }

            while (wantedCells > emptyCells)
            {
                //Console.WriteLine("emptyCells: " + emptyCells);
                puz = removeCell(puz, ref puzTried, ref emptyCells);
            }
            
            return puz;
        }

        private int[][] removeCell(int[][] puz, ref bool[][] overlay, ref int emptyCells)
        {
            int[][] tempPuz = puz;
            
            int count = 0;
            //first get a list of cells in which have not been attempted to remove
            for (int i=0; i<9; i++)
                for (int j = 0; j < 9; j++)
                    if (tempPuz[i][j] > 0 && !overlay[i][j])
                    {
                        count += 1;
                    }

            //if count is 0 then we have tried all possibilities to no avail
            //so for now just increment the total missing cells until we end
            if (count == 0)
            {
                emptyCells += 1;
                return tempPuz;
            }

            //now pick one randomly
            int t = randomNum(1, count);

            //now run back through and fix this guy up, being sure to test his uniqueness
            count = 0;
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    //Console.WriteLine("Field: " + tempPuz[i][j] + " overlay: " + overlay[i][j]);
                    if (tempPuz[i][j] > 0 && !overlay[i][j])
                    {
                        count += 1;
                        if (count == t)
                        {
                            int temp = tempPuz[i][j];
                            tempPuz[i][j] = 0;
                            int sols = solvedSolutions(tempPuz);
                            //Console.WriteLine("tempPuz: " + temp + " overlay: " + overlay[i][j] + " solutions: " + sols);
                            if (sols != 1)
                            {
                                //Console.WriteLine("tempPuz: " + temp + " overlay: " + overlay[i][j] + " solutions: " + sols);
                                tempPuz[i][j] = temp;
                                overlay[i][j] = true;
                            }
                            else
                            {
                                //Console.WriteLine("tempPuz: " + temp + " overlay: " + overlay[i][j] + " solutions: " + sols);
                                overlay[i][j] = true;
                                emptyCells += 1;
                            }
                        }
                    }
                }

            return tempPuz;
        }

        private int[][] removeRandomNumberAll(int[][] puz)
        {
            //this sucker just removes all of a random number from the puzzle
            int r = randomNum(1, 9);
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (puz[i][j] == r)
                        puz[i][j] = 0;
            return puz;
        }
        
        private int[][] generateInitial()
        {
            //fill a base puzzle that is guaranteed to be solvable
            int[][] puz = new int[9][];
            puz[0] = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            puz[1] = new int[] { 4, 5, 6, 7, 8, 9, 1, 2, 3 };
            puz[2] = new int[] { 7, 8, 9, 1, 2, 3, 4, 5, 6 };
            puz[3] = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 };
            puz[4] = new int[] { 5, 6, 7, 8, 9, 1, 2, 3, 4 };
            puz[5] = new int[] { 8, 9, 1, 2, 3, 4, 5, 6, 7 };
            puz[6] = new int[] { 3, 4, 5, 6, 7, 8, 9, 1, 2 };
            puz[7] = new int[] { 6, 7, 8, 9, 1, 2, 3, 4, 5 };
            puz[8] = new int[] { 9, 1, 2, 3, 4, 5, 6, 7, 8 };

            //minimum of 50 iterations max of 100
            for (int i=randomNum(1,50); i<=100; i++)
            {
                //transpose the puzzle a min of 10 and max of 50
                for (int j = randomNum(1, 40); j <= 50; j++)
                {
                    puz = transpose(puz);
                    //swap a rowSection for each transpose
                    puz = swapRowSection(puz, randomNum(1, 3));
                }
            }

            return puz;
        }

        //random numbers between two values inclusive
        private int randomNum(int begin, int end)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            return rnd.Next(begin, end + 1);
        }

        //used to swap rows for puzzle generation
        private int[][] swapRowSection(int[][] puzzle, int rowSection)
        {
            //this function randomly picks a row in the section and swaps it with another row in the section
            //row section is 1 ,2, or 3 where 1 is rows 1,2,3: 2 is rows 4,5,6: 3 is 7,8,9
            int x = randomNum(1, 3);
            int y = randomNum(1, 2);
            //Console.WriteLine("x: " + x + " ,y: " + y);
            if (x == y)
            {
                y = randomNum(1, 2);
                switch (x)
                {
                    case 1:
                        y = x + y;
                        break;
                    case 2:
                        if (y == 2)
                            y = x + 1;
                        break;
                    case 3:
                        y = x - y;
                        break;
                }
            }
            //Console.WriteLine("x: " + x + " ,y: " + y);
            int[] row = puzzle[findRow(x,rowSection)];
            puzzle[findRow(x,rowSection)] = puzzle[findRow(y,rowSection)];
            puzzle[findRow(y,rowSection)] = row;
            return puzzle;
        }

        //used to find actual row
        private int findRow(int row, int section)
        {
            return 8-(9-((section*3)-(3-row)));
        }

        //used to turn the puzzle easily
        private int[][] transpose(int[][] puzzle)
        {
            int[][] ret = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                ret[i] = new int[9];
                for (int j=0; j<9; j++)
                    ret[i][j] = puzzle[j][i];
            }

            //writeArrayToConsole(ret);
            return ret;
        }

        public static String solveString(int[][] puzzleToSolve)
        {
            int[][] puzzle = solveArr(puzzleToSolve, false);
            String ret = "";
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    ret += puzzle[i][j];
                ret += Environment.NewLine;
            }
            return ret;
        }

        public static int[][] solveArray(int[][] puzzleToSolve)
        {
            int[][] ret = copy(solveArr(puzzleToSolve, false));
            return ret;
        }

        private static int solvedSolutions(int[][] puzzleToSolve)
        {
            int[][] solutions = solveArr(puzzleToSolve, true);
            return solutions[0][0];
        }

        private static int[][] solveArr(int[][] puzzleToSolve, bool count)
        {
            //This solver is based on code provided by James McCaffrey:
            //https://msdn.microsoft.com/en-us/magazine/dn759446.aspx
            //with some changes

            //setting up the solver
            Decision[][] grid = new Decision[9][];
            SolverContext problem = SolverContext.GetContext();
            problem.ClearModel();
            Model model = problem.CreateModel();

            for (int i = 0; i < 9; i++)
                grid[i] = new Decision[9];

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    grid[i][j] = new Decision(Domain.IntegerRange(1, 9), "grid" + i + j);

            for (int i = 0; i < 9; i++)
                model.AddDecisions(grid[i]);

            //setting up the constraint for the solver
            //first the rows
            for (int i = 0; i < 9; i++)
                model.AddConstraint("rowConstraint" + i, Model.AllDifferent(grid[i]));

            //now column constraint
            for (int i = 0; i < 9; i++)
                for (int first = 0; first < 8; first++)
                    for (int second = first + 1; second < 9; second++)
                        model.AddConstraint("colConstraint" + i + first + second, grid[first][i] != grid[second][i]);

            //now for each 3x3 grid
            for (int i = 0; i < 9; i += 3)
                for (int j = 0; j < 9; j += 3)
                    for (int a = i; a < i + 3; a++)
                        for (int b = j; b < j + 3; b++)
                            for (int x = i; x < i + 3; x++)
                                for (int y = j; y < j + 3; y++)
                                    if ((x == a && y > b) || (x > a))
                                        model.AddConstraint("cubeConstraint" + a + b + x + y, grid[a][b] != grid[x][y]);
            
            //constrain to the actual puzzle values
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (puzzleToSolve[i][j] != 0)
                        model.AddConstraint("v" + i + j, grid[i][j] == puzzleToSolve[i][j]);
            
            //now solve it
            if (count)
            {
                int numSolutions = NumberSolutions(problem);
                int[][] solutions = new int[1][];
                solutions[0] = new int[1];
                solutions[0][0] = numSolutions;
                return solutions;
            }

            Solution solution = problem.Solve();

            int[][] solvedPuzzle = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                solvedPuzzle[i] = new int[9];
                for (int j = 0; j < 9; j++)
                    solvedPuzzle[i][j] = (int)grid[i][j].GetDouble();
            }

            problem.ClearModel();

            return solvedPuzzle;
        }

        private static int NumberSolutions(SolverContext problem)
        {
            int ct = 0;
            Solution soln = problem.Solve();
            while (soln.Quality == SolverQuality.Feasible)
            {
                ++ct;
                soln.GetNext();
            }
            return ct;
        }

        
        private static void writeArrayToConsole(int[][] array)
        {
            for (int i = 0; i < 9; i++)
            {
                String line = "";
                for (int j = 0; j < 9; j++)
                {
                    line += array[i][j];
                }
                Console.WriteLine(line);
            }
        }

        //we have to copy the array explicitly or it will only be a pointer
        private static int[][] copy(int[][] arrayToCopy)
        {
            int[][] local = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                local[i] = new int[9];
                for (int j = 0; j < 9; j++)
                    local[i][j] = arrayToCopy[i][j];
            }
            return local;
        }

        //Database Operations
        public void saveProgress()
        {
            //The game is exiting, so we save it
            //we need to turn the board into a String
            //to store it. We also need a time from the
            //GUI. We also need to save the date.

            String solution = convertIntToString(this.puzzleSolution);
            String puzzle = convertIntToString(this.puzzle);
            String original = convertIntToString(this.origPuzzle);
            int time = this.totalTime;
            DateTime now = DateTime.Now;
            long date = now.Ticks;

            m_dbConnection.Open();
            String sql = "insert into games (date, solution, puzzle, original, time) values (" + date + ", " + solution + ", " + puzzle + ", " + original + ", " + time + ")";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

        public void saveHighScore()
        {
            //save the results of this game to the HighScores table if it warrants it
            //simple fields of difficulty, time, and date
            String difficulty = this.selectedDifficulty.ToString();
            int time = this.totalTime;
            DateTime now = DateTime.Now;
            long date = now.Ticks;

            m_dbConnection.Open();
            String sql = "insert into highscores (date, time, difficulty) values (" + date + ", " + time + ", " + difficulty + ")";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

        public int[][] loadGame(long dateTime)
        {
            //need to specify data type to return and what parameter to return
            //we need to retrieve the board from the database along with the time so far.
            m_dbConnection.Open();
            String sql = "select * from games where date=" + dateTime;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                this.puzzleSolution = convertStringToInt((String)reader["solution"]);
                this.puzzle = convertStringToInt((String)reader["puzzle"]);
                this.origPuzzle = convertStringToInt((String)reader["original"]);
                this.totalTime = Convert.ToInt32(reader["time"]);
            }

            m_dbConnection.Close();
            return this.puzzle;
        }

        public List<long> getGames()
        {
            //returns a list of all games that are saved
            //games are sorted by date
            List<long> list = new List<long>();
            m_dbConnection.Open();
            String sql = "select * from games order by date desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add((long)reader["date"]);
            }

            m_dbConnection.Close();
            return list;
        }

        public List<Highscore> getHighScore(Difficulty difficulty)
        {
            //returns the HighScores for the specified difficulty level
            // we are only returning the ten best
            //sorted by best time

            List<Highscore> scores = new List<Highscore>();

            m_dbConnection.Open();
            String sql = "select * from highscores where difficulty=" + difficulty.ToString() + " order by time limit 10";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Highscore tScore = new Highscore();
                tScore.time = (int)reader["time"];
                tScore.Difficulty = (String)reader["difficulty"];
                tScore.Date = (long)reader["date"];

                scores.Add(tScore);
            }

            m_dbConnection.Close();

            return scores;
        }

        public List<Highscore> getHighScores()
        {
            //returns top ten high scores for all difficulties
            //sorted by difficulty and best time
            List<Highscore> scores = new List<Highscore>();
            scores.AddRange(getHighScore(Difficulty.EASY));
            scores.AddRange(getHighScore(Difficulty.MEDIUM));
            scores.AddRange(getHighScore(Difficulty.HARD));
            return scores;
        }

        private String convertIntToString(int[][] data)
        {
            //this is used to convert our games to String for saving in Database
            //we do not need to worry about seperators as we only deal in single digits

            String t = "";
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    t = t + data[i][j];

            return t;
        }

        private int[][] convertStringToInt(String data)
        {
            int[][] t = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                t[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    t[i][j] = Convert.ToInt32(data[((i+1)*(j+1))-1]);
                }
            }
            return t;
        }
    }
}
