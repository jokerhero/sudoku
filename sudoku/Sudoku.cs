using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sudoku
{
    class Sudoku
    {
        String puzzleAsString;
        int[,] puzzleAsArray;

        public Sudoku()
        {
            puzzleAsString = "";
            puzzleAsArray = new int[9, 9];
        }

        public Sudoku(String puzzle)
        {
            puzzleAsString = puzzle;
        }

        public Sudoku(int[,] puzzle)
        {
            puzzleAsArray = puzzle;
        }
    }
}
