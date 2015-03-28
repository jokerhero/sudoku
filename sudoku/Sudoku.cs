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
        String puzzleAsString;
        private int[][] _puzzleAsArray = new int[9][];
        public int[][] puzzleAsArray
        {
            set { this._puzzleAsArray = value; }
            get { return this._puzzleAsArray; }
        }

        public Sudoku()
        {
            puzzleAsString = "";
            puzzleAsArray[0] = new int[]{0,0,6,2,0,0,0,8,0};
            puzzleAsArray[1] = new int[]{0,0,8,9,7,0,0,0,0};
            puzzleAsArray[2] = new int[]{0,0,4,8,1,0,5,0,0};
            puzzleAsArray[3] = new int[]{0,0,0,0,6,0,0,0,2};
            puzzleAsArray[4] = new int[]{0,7,0,0,0,0,0,3,0};
            puzzleAsArray[5] = new int[]{6,0,0,0,5,0,0,0,0};
            puzzleAsArray[6] = new int[]{0,0,2,0,4,7,1,0,0};
            puzzleAsArray[7] = new int[]{0,0,3,0,2,8,4,0,0};
            puzzleAsArray[8] = new int[]{0,5,0,0,0,1,2,0,0};
        }

        public Sudoku(String puzzle)
        {
            puzzleAsString = puzzle;
        }

        public Sudoku(int[][] puzzle)
        {
            puzzleAsArray = puzzle;
        }

        private int[][] convertStringToArray(String puzzle)
        {
            int[][] returnPuz = new int[9][];



            return returnPuz;
        }

        private int[][] solve(int[][] puzzleToSolve)
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
            //first the row
            for (int i = 0; i < 9; i++)
                model.AddConstraint("rowConstraint" + i, Model.AllDifferent(grid[i]));

            //now column constraint
            for (int i = 0; i < 9; i++)
            {
                for (int first=0; first<8; first++)
                    for (int second=first+1; second<9; second++)
                        model.AddConstraint("colConstraint"+i+first+second, grid[first][i]!=grid[second][i]);
            }

            //now for each 3x3 grid
            for (int i = 0; i < 9; i += 3)
                for (int j = 0; j < 9; j += 3)
                    for (int a = i; a < i + 3; a++)
                        for (int b = j; b < j + 3; b++)
                            for (int x = i; x < i + 3; x++)
                                for (int y = j; y < j + 3; y++)
                                    model.AddConstraint("cubeConstraint" + a + b + x + y, grid[a][b] != grid[x][y]);
            
            //Setup the final puzzle to be checked
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (puzzleToSolve[i][j] != 0)
                        model.AddConstraint("v" + i + j, grid[i][j] == puzzleToSolve[i][j]);
            
            //now solve it
            Solution solution = problem.Solve();

            int[][] solvedPuzzle = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    solvedPuzzle[i][j] = (int)grid[i][j].GetDouble();
                }
            }
            return solvedPuzzle;
        }
    }
}
