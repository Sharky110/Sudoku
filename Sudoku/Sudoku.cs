using System;
using System.Collections.Generic;

namespace SudokuEngine
{
    public class Sudoku
    {
        private static Sudoku _instance;
        private Field _field;

        private Sudoku()
        {
            _field = new Field();
        }

        public static Sudoku GetInstance()
        {
            return _instance ?? (_instance = new Sudoku());
        }

        public Dictionary<int,string> Initialize(int difficult)
        {
            _field.GenerateSudoku(2);
            _field.RemoveNums(difficult);

            return _field.ToDictionary();
        }

        public IEnumerable<int> CheckCell(KeyValuePair<int,int> cell)
        {
            return _field.CheckCell(cell.Key, cell.Value);
        }
    }
}
