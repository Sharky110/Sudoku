using System;
using System.Collections.Generic;

namespace SudokuEngine
{
    public class Sudoku
    {
        private static Sudoku _instanse;
        private Field _field;

        private Sudoku()
        {
            _field = new Field();
        }

        public static Sudoku GetInstanse()
        {
            return _instanse ?? (_instanse = new Sudoku());
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
