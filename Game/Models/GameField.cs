using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Models
{
    class GameField
    {
        private Cell[] _cells = new Cell[FIELD_SIZE];

        private const int FIELD_SIZE = 81;

        public void RemoveNums(int numsToRemove)
        {
            var random = new Random();
            for (int i = 0; i < numsToRemove; i++)
            {
                var randNum = random.Next(FIELD_SIZE);

                if (string.IsNullOrEmpty(_cells[randNum].Value))
                    i -= 1;
                else
                    _cells[randNum].Value = string.Empty;
            }
        }

        public void GenerateSudoku(int threads)
        {
            var tasks = new Task<Cell[]>[threads];

            for (int i = 0; i < threads; i++)
                tasks[i] = Task.Run(GenerateNums);

            while (!tasks.Any(x => x.IsCompleted))
                Thread.Sleep(1);

            _cells = tasks.FirstOrDefault().Result;
        }

        public Dictionary<int, string> ToDictionary()
        {
            return _cells.ToDictionary(k => k.Id, v => v.Value);
        }

        public IEnumerable<int> CheckCell(int id, string value)
        {
            var cell = _cells.Where(x => x.Id == id).FirstOrDefault();

            var ff = _cells.Where(x =>
                 (x.horPosition == cell.horPosition && x.Value == value) ||
                 (x.vertPosition == cell.vertPosition && x.Value == value) ||
                 (x.cubePosition == cell.cubePosition && x.Value == value)).Select(x => x.Id);

            return ff;
        }

        public Dictionary<int, string> Initialize(int difficult)
        {
            GenerateSudoku(2);
            RemoveNums(difficult);

            return _field.ToDictionary();
        }

        public void SetValueToCell(int id, string value)
        {
            _cells[id].Value = value;
        }

        private Cell[] GenerateField()
        {
            var list = new Cell[FIELD_SIZE];
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                var CubePosition = i % 9 / 3 + (i / 27) * 3;
                list[i] = new Cell(i, i % 9, i / 9, CubePosition);
            }
            return list;
        }

        private Cell[] GenerateNums()
        {
            var SudokuField = GenerateField();

            var random = new Random();
            var counter = 0;

            for (int i = 0; i < SudokuField.Length; i++)
            {
                var randVal = (random.Next(100) % 9 + 1).ToString();

                if (!SudokuField.Any(x =>
                 (x.horPosition == SudokuField[i].horPosition && x.Value == randVal) ||
                 (x.vertPosition == SudokuField[i].vertPosition && x.Value == randVal) ||
                 (x.cubePosition == SudokuField[i].cubePosition && x.Value == randVal)))
                {
                    counter = 0;
                    SudokuField[i].Value = randVal;
                }
                else
                {
                    i -= 1;
                    counter += 1;
                }
                if (counter > 90)
                {
                    i = -1;
                    foreach (var t in SudokuField)
                        t.Value = 0.ToString();
                }
            }

            return SudokuField;
        }

        private string ToStging(int num)
        {
            return num == 0 ? " " : num.ToString();
        }
    }
}
