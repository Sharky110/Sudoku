﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SudokuGenerator
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

        public List<Cell> GenerateField()
        {
            var list = new List<Cell>(FIELD_SIZE);
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                var CubePosition = i % 9 / 3 + (i / 27) * 3;
                list.Add(new Cell(i, i % 9, i / 9, CubePosition));
            }
            return list;
        }

        public void PrintField()
        {
            foreach (var cell in _cells)
            {
                Console.Write(cell.value != 0 ? $"{cell.value} " : " " + " ");
                if (cell.id % 9 == 8)
                    Console.Write("\n");
            }
        }

        public void GenerateSudoku(int threads)
        {
            var start = DateTime.Now;

            var tasks = new Task<List<Cell>>[threads];

            for (int i = 0; i < threads; i++)
                tasks[i] = Task.Run(GenerateNums);

            while (!tasks.Any(x => x.IsCompleted))
                Thread.Sleep(10);
            
            var time = DateTime.Now - start;
            var rr = string.Format("{0}.{1}", time.Seconds, time.Milliseconds.ToString().PadLeft(3, '0'));
            Console.WriteLine($"Elapsed time: {rr}\n");

            _cells =  tasks.FirstOrDefault().Result;
        }

        public List<Cell> GenerateNums()
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
