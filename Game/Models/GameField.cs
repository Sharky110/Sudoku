using Game.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

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

        public Dictionary<int, string> ToDictionary()
        {
            return _cells.ToDictionary(k => k.Id, v => v.Value);
        }

        public IEnumerable<int> GetDuplicateIdCells(int id, string value)
        {
            return _cells.Where(cell => IsCellValueExist(cell, _cells[id], value)).Select(x => x.Id);
        }

        public Cell[] Init(int difficult)
        {
            GenerateNums();
            RemoveNums(difficult);

            foreach (var cell in _cells)
            {
                cell.IsEnabled = string.IsNullOrWhiteSpace(cell.Value);
                cell.Color = string.IsNullOrWhiteSpace(cell.Value) ? Brushes.White : Brushes.LightGray;
                cell.ButtonCommand = new RelayCommand(ButtonClick);
                cell.button2Command = new RelayCommand(button2click);
            }
            return _cells;
        }

        private void button2click(object obj)
        {
            var customButton = _cells[cid];

            SetDefaultColor();

            customButton.Value = obj.ToString();
            customButton.Color = Brushes.LightGreen;

            SetValueToCell(customButton.Id, customButton.Value);

            var idCells = GetDuplicateIdCells(customButton.Id, customButton.Value);

            foreach (var cell in _cells)
            {
                if (idCells.Contains(cell.Id) && cell.Id != customButton.Id)
                    cell.Color = cell.IsEnabled ? Brushes.Yellow : Brushes.Orange;
            }
            customButton.IsButtonPushed = false;
        }

        public static int cid;

        public void ButtonClick(object id)
        {
            cid = Convert.ToInt32(id);
            var customButton = _cells[cid];

            if (!_cells[customButton.Id].IsEnabled)
                return;

            CloseAllPopups();
            customButton.IsButtonPushed = true;
        }

        public void SetDefaultColor()
        {
            foreach (var button in _cells)
            {
                button.Color = button.IsEnabled ? Brushes.White : Brushes.LightGray;
                if (button.Color == Brushes.White && !string.IsNullOrWhiteSpace(button.Value))
                    button.Color = Brushes.LightGreen;
            }
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

        private void GenerateNums()
        {
            _cells = GenerateField();

            var random = new Random();
            var counter = 0;

            for (int i = 0; i < _cells.Length; i++)
            {
                var randVal = (random.Next(100) % 9 + 1).ToString();

                if (!_cells.Any(cell => IsCellValueExist(cell, _cells[i], randVal)))
                {
                    counter = 0;
                    _cells[i].Value = randVal;
                }
                else
                {
                    i -= 1;
                    counter += 1;
                }
                if (counter > 40)
                {
                    i = -1;
                    _cells = GenerateField();
                }
            }
        }

        private void CloseAllPopups()
        {
            foreach (var cell in _cells)
            {
                cell.IsButtonPushed = false;
            }
        }

        bool IsCellValueExist(Cell cell, Cell newCell, string newValue)
        {
            if (cell.Value == newValue)
                if (cell.horPosition == newCell.horPosition)
                    return true;
                else if (cell.vertPosition == newCell.vertPosition)
                    return true;
                else if (cell.cubePosition == newCell.cubePosition)
                    return true;
            return false;
        }
    }
}
