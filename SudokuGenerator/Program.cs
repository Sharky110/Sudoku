using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SudokuGenerator
{
    class Program
    {
        static byte[,] sudoku = new byte[9, 9];

        [STAThread]
        static void Main(string[] args)
        {

            
            Stopwatch watch = new Stopwatch();
            watch.Start();

            byte counter = 0;
            byte random = 0;
            for (int i = 0; i < 10000; i++)
            {
                for (int x = 0; x < 9; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        random = (byte)(watch.ElapsedTicks % 9 + 1);

                        if (IsInRow(x, random) || IsInCol(y, random) || IsInCube(x, y, random))
                        {
                            counter += 1;
                            y -= 1; 
                            if (counter > 40)
                            {
                                x = -1;
                                counter = 0;
                                sudoku = new byte[9, 9];
                                break;
                            }
                        }
                        else
                        {
                            sudoku[x, y] = random;
                            counter = 0;
                        }

                    }
                }
            }
            watch.Stop();
            Console.WriteLine("It's over");
            Console.WriteLine($"Average Ms: {watch.ElapsedMilliseconds / 10000}");
            Console.ReadLine();
        }

        static bool IsInRow(int x, int value)
        {

            for (int y = 0; y < 9; y++)
            {
                if (sudoku[x, y] == value)
                    return true;
            }
            return false;
        }

        static bool IsInCol(int y, int value)
        {
            for (int x = 0; x < 9; x++)
            {
                if (sudoku[x, y] == value)
                    return true;
            }
            return false;
        }

        static bool IsInCube(int x,int y, int value)
        {
            var startX = x / 3 * 3;
            var startY = y / 3 * 3;
            var endX = startX + 3;
            var endY = startY + 3;
            for (int a = startX; a < endX; a++)
            {
                for (int b = startY; b < endY; b++)
                {
                    if (sudoku[a, b] == value)
                        return true;
                }
            }
            return false;
        }
    }
}
