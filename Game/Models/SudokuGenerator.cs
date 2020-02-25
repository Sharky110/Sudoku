using System;

namespace Game.Models
{
    class SudokuGenerator
    {
        static byte[,] sudoku = new byte[9, 9];

        public static byte[,] Generate()
        {
            Console.WriteLine($"Fast&Furious starts!");
            
            byte counter = 0;
            byte random;
            var rand = new Random();
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    random = (byte)(rand.Next(1, 10));

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
            return sudoku;
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

        static bool IsInCube(int x, int y, int value)
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
