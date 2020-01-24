using System;
using System.Collections.Generic;

namespace SudokuGenerator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Field cells = new Field(new List<Cell>(81));
            while (true)
            {
                Console.WriteLine("\nYou wonna play? 1 - Yes");
                Console.WriteLine("1 - Generate sudoku");
                Console.WriteLine("2 - Print result");
                Console.WriteLine("3 - Remove nums");
                switch (Console.ReadLine())
                {
                    case "1":
                        cells.GenerateSudoku(2);
                        break;
                    case "2":
                        cells.PrintField();
                        break;
                    case "3":
                        cells.RemoveNums(5);
                        cells.PrintField();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
