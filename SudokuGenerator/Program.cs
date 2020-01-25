using System;
using System.Collections.Generic;
using SudokuEngine;

namespace SudokuGenerator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Sudoku d = Sudoku.GetInstanse();
            var f = d.Initialize(24);

            foreach (var cell in f)
            {
                Console.Write(cell.Value != 0 ? $"{cell.Value} " : " " + " ");
                if (cell.Key % 9 == 8)
                    Console.Write("\n");
            }

        }
    }
}
