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

namespace sudoku
{
    class Sudoku
    {
        public int[][] puzzleSolution { get; set; }

        public Sudoku()
        {
            puzzleSolution = generatePuzzle();
        }

        private int[][] generatePuzzle()
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

        public static String solve(int[][] puzzleToSolve)
        {
            int[][] puzzle = solveArr(puzzleToSolve);
            String ret = "";
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    ret += puzzle[i][j];
                ret += Environment.NewLine;
            }
            return ret;
        }

        private static int[][] solveArr(int[][] puzzleToSolve)
        {
            //This solver is based on code provided by James McCaffrey:
            //https://msdn.microsoft.com/en-us/magazine/dn759446.aspx
            //with some changes

            //setting up the solver
            Decision[][] grid = new Decision[9][];
            SolverContext problem = SolverContext.GetContext();
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
            //int numSolutions = NumberSolutions(problem);
            //Console.WriteLine("\nThere are " + numSolutions + " solutions\n");

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
        
    }
}
