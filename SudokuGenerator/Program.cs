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
            Console.WriteLine($"Fast&Furious starts!");
            Stopwatch watch = new Stopwatch();
            Stopwatch watch2 = new Stopwatch();

            watch.Start();

            int times = 1;
            byte counter = 0;
            byte random;
            int minTime = 1000000;
            int maxTime = 0;
            var rand = new Random();
            for (int i = 0; i < times; i++)
            {
                watch2.Start();
                for (int x = 0; x < 9; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {
                        //random = (byte)(watch.ElapsedTicks % 9 + 1);
                        random = (byte)(rand.Next(1,10));

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
                watch2.Stop();
                if((int)watch2.ElapsedTicks < minTime)
                    minTime = (int)watch2.ElapsedTicks;
                if ((int)watch2.ElapsedTicks > maxTime)
                    maxTime = (int)watch2.ElapsedTicks;
                watch2.Reset();
            }
            watch.Stop();

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    Console.Write(sudoku[x, y] + " ");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("It's over");
            Console.WriteLine($"Generated {times} times.");
            Console.WriteLine($"Full time: {watch.ElapsedMilliseconds} Ms, {watch.ElapsedTicks} Ticks");
            Console.WriteLine($"Average Ms: {watch.ElapsedMilliseconds / (double)times}");
            Console.WriteLine();
            Console.WriteLine($"Min ticks: {minTime}");
            Console.WriteLine($"Average ticks: {watch.ElapsedTicks / times}");
            Console.WriteLine($"Max ticks: {maxTime}");
            Console.ReadLine();
        }

        static bool IsInRow(int x, int value)
        {

            for (int y = 0; y < 9; y++)
                if (sudoku[x, y] == value)
                    return true;
            return false;
        }

        static bool IsInCol(int y, int value)
        {
            for (int x = 0; x < 9; x++)
                if (sudoku[x, y] == value)
                    return true;
            return false;
        }

        static bool IsInCube(int x,int y, int value)
        {
            var startX = x / 3 * 3;
            var startY = y / 3 * 3;
            var endX = startX + 3;
            var endY = startY + 3;

            for (int a = startX; a < endX; a++)
                for (int b = startY; b < endY; b++)
                    if (sudoku[a, b] == value)
                        return true;

            return false;
        }
    }
}
