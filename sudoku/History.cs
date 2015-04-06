using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sudoku
{
    class History
    {
        Dictionary<String, int[][]> history = new Dictionary<string, int[][]>();

        public void addPuzzle(int[][] puz)
        {
            String t = puzToString(puz);
            if (history.ContainsKey(t))
                return;
            history.Add(t, puz);
        }

        public void remPuzzle(int[][] puz)
        {
            String t = puzToString(puz);
            history.Remove(t);
        }

        private String puzToString(int[][] puz)
        {
            String t = "";
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    t = t + puz[i][j];
            return t;
        }
    }
}
