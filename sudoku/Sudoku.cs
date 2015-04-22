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
    public class Sudoku
    {
        public class Entry
        {
            public long Date;
            public int time;
        }

        public class Highscore
        {
            public int count;
            public Difficulty difficulty;
            public List<Entry> entries;
        }

        int[][] puzzleSolution { get; set; }
        public int[][] puzzle { get; set; }
        public int[][] origPuzzle { get; set; }
        private int totalTime { get; set; }         //the total time taken so far - seconds
        private Difficulty selectedDifficulty {get; set; }

        public Sudoku()
        {
            totalTime = 0;
            puzzleSolution = copy(initializePuzzleArray());
            puzzle = copy(puzzleSolution);
            origPuzzle = copy(puzzle);
            totalTime = 0;
            //writeArrayToConsole(puzzleSolution);
        }

        private int[][] initializePuzzleArray()
        {
            int[][] puz = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                puz[i] = new int[9];
                for (int j = 0; j < 9; j++)
                    puz[i][j] = 0;
            }       
            return puz;
        }

        #region "Public Methods Related to normal Game Play"
        /* no longer used, was useful for testing
        private String getPuzzle()
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
        */

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

        public int[][] generatePuzzle(Difficulty difficulty)
        {
            //I understand this is not the best measurement for difficulty.
            //I chose this as it is the fastest way to generate puzzles on the fly
            //with a reasonable expectation of difficulty

            //We need to remove numbers until we have our desired difficulty
            //On hard, we remove all of one number and start from there
            puzzleSolution = copy(generateInitial());
            totalTime = 0;

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

        public static int[][] solveArray(int[][] puzzleToSolve)
        {
            int[][] ret = copy(solveArr(puzzleToSolve, false));
            return ret;
        }
        
        #endregion

        #region "Puzzle Generation"

        private bool checkSolution(int[][] toCheck)
        {
            //return true for a valid solution and false for an invalid solution
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (toCheck[i][j] != puzzleSolution[i][j] || puzzleSolution[i][j] == 0)
                        return false;

            return true;
        }

        private int[][] generateDifficulty(int wantedCells)
        {
            int[][] puz = copy(puzzle);
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

            writeArrayToConsole(puzzle);
            return puz;
        }

        private int[][] removeCell(int[][] puz, ref bool[][] overlay, ref int emptyCells)
        {
            int[][] tempPuz = puz;

            int count = 0;
            //first get a list of cells in which have not been attempted to remove
            for (int i = 0; i < 9; i++)
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
            int[][] p = copy(puz);
            int r = randomNum(1, 9);
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (p[i][j] == r)
                    {
                        p[i][j] = 0;
                    }
            return p;
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
            for (int i = randomNum(1, 500); i <= 500; i++)
            {
                //transpose the puzzle a min of 10 and max of 50
                for (int j = randomNum(1, 500); j <= 500; j++)
                {
                    puz = transpose(puz);
                    //swap a rowSection for each transpose
                    puz = swapRowSection(puz, randomNum(1, 3));
                }
            }

            return puz;
        }

        private int randomNum(int begin, int end)
        {
            //random numbers between two values inclusive
            Random rnd = new Random(DateTime.Now.Millisecond);
            return rnd.Next(begin, end + 1);
        }

        private int[][] swapRowSection(int[][] puzzle, int rowSection)
        {
            //used to swap rows for puzzle generation
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
            int[] row = puzzle[findRow(x, rowSection)];
            puzzle[findRow(x, rowSection)] = puzzle[findRow(y, rowSection)];
            puzzle[findRow(y, rowSection)] = row;
            return puzzle;
        }

        private int findRow(int row, int section)
        {
            //used to find actual row
            return 8 - (9 - ((section * 3) - (3 - row)));
        }

        private int[][] transpose(int[][] puzzle)
        {
            //used to turn the puzzle easily
            int[][] ret = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                ret[i] = new int[9];
                for (int j = 0; j < 9; j++)
                    ret[i][j] = puzzle[j][i];
            }

            //writeArrayToConsole(ret);
            return ret;
        }

        #endregion

        /* was useful for testing
        private static String solveString(int[][] puzzleToSolve)
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
        */

        #region "Solver Code"

        public static int solvedSolutions(int[][] puzzleToSolve)
        {
            writeArrayToConsole(puzzleToSolve);
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
            Solution solution = problem.Solve();

            if (count)
            {
                int ct = 0;

                while (solution.Quality == SolverQuality.Feasible)
                {
                    ++ct;
                    solution.GetNext();
                    if (ct >= 100)
                        break;
                }

                int[][] solutions = new int[1][];
                solutions[0] = new int[1];
                solutions[0][0] = ct;
                Console.Out.WriteLine("Solutions: " + ct);
                return solutions;
            }

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
                if (ct >= 100)
                    break;
            }
            return ct;
        }

        #endregion

        ///* was useful for testing
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
        //*/

        #region "Utility Methods"

        private static int[][] copy(int[][] arrayToCopy)
        {
            //we have to copy the array explicitly or it will only be a pointer
            int[][] local = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                local[i] = new int[9];
                for (int j = 0; j < 9; j++)
                    local[i][j] = arrayToCopy[i][j];
            }
            return local;
        }

        #endregion

        #region "Database Operations"

        private static String path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + path + "\\sudoku.sqlite;Version=3;");

        public void saveProgress()
        {
            checkDatabaseExists();
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
            String difficulty = selectedDifficulty.ToString();

            m_dbConnection.Open();
            String sql = "insert into games (date, solution, puzzle, original, time, difficulty) values (" + date.ToString() + ", '" + solution + "', '" + puzzle + "', '" + original + "', " + time + ", '" + difficulty + "')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

        public void saveHighScore()
        {
            checkDatabaseExists();
            Console.Out.WriteLine(path);
            //save the results of this game to the HighScores table if it warrants it
            //simple fields of difficulty, time, and date
            String difficulty = this.selectedDifficulty.ToString();
            int time = this.totalTime;
            DateTime now = DateTime.Now;
            long date = now.Ticks;

            m_dbConnection.Open();
            String sql = "insert into highscores (date, time, difficulty) values (" + date.ToString() + ", '" + time + "', '" + difficulty + "')";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }

        public int[][] loadGame(long dateTime)
        {
            checkDatabaseExists();
            //need to specify data type to return and what parameter to return
            //we need to retrieve the board from the database along with the time so far.
            m_dbConnection.Open();
            String sql = "select * from games where date=" + dateTime;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //Console.Out.WriteLine("Loading puzzle");
                this.puzzleSolution = convertStringToInt((String)reader["solution"]);
                writeArrayToConsole(this.puzzleSolution);
                this.puzzle = convertStringToInt((String)reader["puzzle"]);
                writeArrayToConsole(this.puzzle);
                this.origPuzzle = convertStringToInt((String)reader["original"]);
                writeArrayToConsole(this.origPuzzle);
                this.totalTime = Convert.ToInt32(reader["time"]);
                this.selectedDifficulty = (Difficulty) Enum.Parse(typeof(Difficulty), (String) reader["difficulty"], true);
            }
            reader.Close();
            command.Dispose();

            //now delete the game from the database
            sql = "DELETE FROM games WHERE date=" + dateTime;
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            command.Dispose();

            m_dbConnection.Close();
            return this.puzzle;
        }

        //this can be static since we do not need a sudoku object to use this
        public static List<long> getGames()
        {
            checkDatabaseExists();
            //returns a list of all games that are saved
            //games are sorted by date
            List<long> list = new List<long>();
            m_dbConnection.Open();
            String sql = "select * from games order by date desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                long date;
                long.TryParse((String)reader["date"], out date);
                Console.Out.WriteLine("Found: " + date);
                list.Add(date);
            }

            m_dbConnection.Close();
            return list;
        }

        //we do not need a sudoku object to use this
        public static Highscore getHighScore(Difficulty difficulty)
        {
            checkDatabaseExists();
            // returns the HighScores for the specified difficulty level
            // we are only returning the ten best
            // sorted by best time

            Highscore scores = new Highscore();
            scores.entries = new List<Entry>();

            m_dbConnection.Open();
            String sql = "select * from highscores where difficulty='" + difficulty.ToString() + "' order by time limit 10";
            Console.Out.WriteLine(sql);
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Entry entry = new Entry();
                entry.time = Convert.ToInt32(reader["time"]);
                entry.Date = Convert.ToInt64(reader["date"]);
                scores.entries.Add(entry);
            }

            reader.Dispose();
            command.Dispose();

            sql = "select count(*) as c from highscores where difficulty='" + difficulty.ToString() + "'";
            command = new SQLiteCommand(sql, m_dbConnection);
            reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = Convert.ToInt32(reader["c"]);
            }

            reader.Dispose();
            command.Dispose();
            m_dbConnection.Close();

            scores.difficulty = difficulty;
            scores.count = count;

            return scores;
        }

        //static as a sudoku object is not needed for this
        public static List<Highscore> getHighScores()
        {
            checkDatabaseExists();
            //returns top ten high scores for all difficulties
            //sorted by difficulty and best time
            List<Highscore> scores = new List<Highscore>();
            scores.Add(getHighScore(Difficulty.EASY));
            scores.Add(getHighScore(Difficulty.MEDIUM));
            scores.Add(getHighScore(Difficulty.HARD));
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
            int count = 0;
            for (int i = 0; i < 9; i++)
            {
                t[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    t[i][j] = Convert.ToInt32(data[count].ToString());
                    count += 1;
                }
            }
            return t;
        }

        private static void checkDatabaseExists()
        {
            //This is needed to be sure the database exists since we are not including an installer
            m_dbConnection.Open();
            string sql_high = "SELECT name FROM sqlite_master WHERE type='table' AND name='highscores';";
            string sql_game = "SELECT name FROM sqlite_master WHERE type='table' AND name='games';";
            SQLiteCommand command = new SQLiteCommand(sql_high, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                String sql = "CREATE TABLE highscores(date TEXT PRIMARY KEY, time TEXT, difficulty TEXT)";
                SQLiteCommand cmd = new SQLiteCommand(sql, m_dbConnection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            reader.Close();
            command.Dispose();
            command = new SQLiteCommand(sql_game, m_dbConnection);
            reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                String sql = "CREATE TABLE games (date TEXT PRIMARY KEY, solution TEXT, puzzle TEXT, original TEXT, time INTEGER, difficulty TEXT)";
                SQLiteCommand cmd = new SQLiteCommand(sql, m_dbConnection);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            reader.Close();
            command.Dispose();
            m_dbConnection.Close();

        }

        #endregion

        #region "Game Timer"

        private DateTime startTime;

        public int startTimer()
        {
            //resolution of DateTime is approximately 10ms, which is more than we need
            startTime = DateTime.UtcNow;
            return totalTime;
        }

        public int pauseTimer()
        {
            DateTime pausedTime = DateTime.UtcNow;
            int elapsedTime = (int)(pausedTime - startTime).TotalSeconds;
            totalTime += elapsedTime;
            return totalTime;
        }

        public int stopTimer()
        {
            //we are doing nothing different from pauseTimer
            return pauseTimer();
        }

        public int getTimer()
        {
            DateTime now = DateTime.UtcNow;
            int elapsedTime = (int)(now - startTime).TotalSeconds;
            //we do not want to set the totalTime, thats the start/pause functions job
            return totalTime + elapsedTime;
        }
        #endregion
    }
}
