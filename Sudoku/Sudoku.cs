using System;
using System.Collections.Generic;

namespace Sudoku
{
    public class Sudoku
    {
        private Sudoku _sudoku;
        private Field _field;

        private Sudoku() {}

        public Sudoku GetInstanse()
        {
            return _sudoku ?? new Sudoku();
        }

        public Dictionary<int,int> Initialize(int difficult)
        {
            _field.GenerateSudoku(2);
            _field.RemoveNums(difficult);

            return _field.ToDictionary();
        }

        public bool CheckCell(KeyValuePair<int,int> cell)
        {
            return _field.CheckCell(cell.Key, cell.Value);
        }
    }
}
