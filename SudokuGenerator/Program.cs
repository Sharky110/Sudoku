using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AddLogToFile
{
    public class Cell
    {
        public int value;
        public int vertPosition;
        public int horPosition;
        public int cubePosition;
        public int id;
        public Cell(int id, int hor, int vert, int cube)
        {
            this.id = id;
            vertPosition = vert;
            horPosition = hor;
            cubePosition = cube;
        }
    }

    public class CellsEnumerator : IEnumerator
    {
        private List<Cell> _cells;
        int position = -1;

        public CellsEnumerator(List<Cell> cells)
        {
            _cells = cells;
        }
        public object Current
        {
            get
            {
                if (position == -1 || position >= _cells.Count)
                    throw new InvalidOperationException();
                return _cells[position];
            }
        }
        public bool MoveNext()
        {
            if (position < _cells.Count - 1)
            {
                position++;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            position = -1;
        }
    }

    public class Cells : IEnumerable
    {
        private List<Cell> _cells;
        public int Count
        {
            get => _cells.Count;
        }

        public Cells(List<Cell> cells)
        {
            _cells = cells;
        }

        public void RemoveNums(int numsToRemove)
        {
            Random random = new Random();
            for (int i = 0; i < numsToRemove; i++)
            {
                var randNum = random.Next(81);

                if (_cells[randNum].value != 0)
                    _cells[randNum].value = 0;
                else
                    i -= 1;
            }
        }

        public void GenerateField()
        {
            for (int i = 0; i < 81; i++)
            {
                var CubePosition = i % 9 / 3 + (i / 27) * 3;
                _cells.Add(new Cell(i, i % 9, i / 9, CubePosition));
            }
        }

        public void PrintSudoku()
        {
            foreach (var cell in _cells)
            {
                Console.Write(cell.value != 0 ? $"{cell.value} " : " " + " ");
                if (cell.id % 9 == 8)
                    Console.Write("\n");
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new CellsEnumerator(_cells);
        }

        public Cell this[int index]
        {
            get => _cells[index];
            set => _cells[index] = value;
        }
    
        internal bool Any(Func<Cell, bool> p)
        {
            return _cells.Any(p);
        }
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Cells cells = new Cells(new List<Cell>(81));
            while (true)
            {
                Console.WriteLine("\nYou wonna play? 1 - Yes");
                Console.WriteLine("1 - Generate sudoku");
                Console.WriteLine("2 - Print result");
                Console.WriteLine("3 - Remove nums");
                switch (Console.ReadLine())
                {
                    case "1":
                        cells = GenerateSudoku(2);
                        break;
                    case "2":
                        cells.PrintSudoku();
                        break;
                    case "3":
                        cells.RemoveNums(10);
                        cells.PrintSudoku();
                        break;
                    default:
                        break;
                }
            }
        }

        public static Cells GenerateSudoku(int threads)
        {
            var start = DateTime.Now;

            Task<Cells>[] tasks = new Task<Cells>[threads];
            for (int i = 0; i < threads; i++)
                tasks[i] = Task.Run(GenerateNums);

            while (!tasks.Any(x => x.IsCompleted))
            {
                Thread.Sleep(50);
            }

            var time = DateTime.Now - start;
            var rr = String.Format("{0}.{1}", time.Seconds, time.Milliseconds.ToString().PadLeft(3, '0'));
            Console.WriteLine($"Elapsed time: {rr}\n");

            return tasks.FirstOrDefault().Result;
        }

        public static Cells GenerateNums()
        {
            var SudokuField = new Cells(new List<Cell>(81));

            SudokuField.GenerateField();

            Random random = new Random();
            int counter = 0;

            for (int i = 0; i < SudokuField.Count; i++)
            {
                var randVal = random.Next(100) % 9 + 1;
                if (!SudokuField.Any(x =>
                 (x.horPosition == SudokuField[i].horPosition && x.value == randVal) ||
                 (x.vertPosition == SudokuField[i].vertPosition && x.value == randVal) ||
                 (x.cubePosition == SudokuField[i].cubePosition && x.value == randVal)))
                {
                    counter = 0;
                    SudokuField[i].value = randVal;
                }
                else
                {
                    i -= 1;
                    counter += 1;
                }
                if (counter > 90)
                {
                    i = -1;
                    foreach (Cell t in SudokuField)
                        t.value = 0;
                }
            }

            return SudokuField;
        }
    }
}
