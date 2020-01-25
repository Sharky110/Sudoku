using System;
using System.Collections.Generic;
using Sudoku;

namespace SudokuGenerator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            
            while (true)
            {
                Console.WriteLine("\nYou wonna play? 1 - Yes");
                Console.WriteLine("1 - Generate sudoku");
                Console.WriteLine("2 - Print result");
                Console.WriteLine("3 - Remove nums");
            }
        }
    }
}
