﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Field
    {
        private List<Cell> _cells;

        private const int FIELD_SIZE = 81;

        public Field(List<Cell> cells)
        {
            _cells = cells;
        }

        public void RemoveNums(int numsToRemove)
        {
            var random = new Random();
            for (int i = 0; i < numsToRemove; i++)
            {
                var randNum = random.Next(FIELD_SIZE);

                if (_cells[randNum].value != 0)
                    _cells[randNum].value = 0;
                else
                    i -= 1;
            }
        }

        public void GenerateSudoku(int threads)
        {
            var tasks = new Task<List<Cell>>[threads];

            for (int i = 0; i < threads; i++)
                tasks[i] = Task.Run(GenerateNums);

            while (!tasks.Any(x => x.IsCompleted))
                Thread.Sleep(10);

            _cells = tasks.FirstOrDefault().Result;
        }

        public Dictionary<int, int> ToDictionary()
        {
            return _cells.ToDictionary(x => x.id, i => i.value);
        }

        public bool CheckCell(int id, int value)
        {
            return _cells[id].value == value;
        }

        private List<Cell> GenerateField()
        {
            var list = new List<Cell>(FIELD_SIZE);
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                var CubePosition = i % 9 / 3 + (i / 27) * 3;
                list.Add(new Cell(i, i % 9, i / 9, CubePosition));
            }
            return list;
        }

        private List<Cell> GenerateNums()
        {
            var SudokuField = GenerateField();

            var random = new Random();
            var counter = 0;

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
                    foreach (var t in SudokuField)
                        t.value = 0;
                }
            }

            return SudokuField;
        }
    }
}