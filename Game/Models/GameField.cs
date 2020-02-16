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

        private Cell[,] _testcells = new Cell[9,9];

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

        public IEnumerable<int> CheckCell(int id, string value)
        {
            var cell = _cells.Where(x => x.Id == id).FirstOrDefault();

            var ff = _cells.Where(x =>
                 (x.horPosition == cell.horPosition && x.Value == value) ||
                 (x.vertPosition == cell.vertPosition && x.Value == value) ||
                 (x.cubePosition == cell.cubePosition && x.Value == value)).Select(x => x.Id);

            return ff;
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
            var val = Convert.ToInt32(obj);

            var customButton = _cells[cid];

            SetDefaultColor();

            customButton.Value = val.ToString();
            customButton.Color = Brushes.LightGreen;

            SetValueToCell(customButton.Id, customButton.Value);

            var dd = CheckCell(customButton.Id, customButton.Value);

            foreach (var button in _cells)
            {
                if (dd.Contains(button.Id) && button.Id != customButton.Id)
                    button.Color = button.IsEnabled ? Brushes.LightYellow : Brushes.Yellow;
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

                if (!_cells.Any(x =>
                 (x.horPosition == _cells[i].horPosition && x.Value == randVal) ||
                 (x.vertPosition == _cells[i].vertPosition && x.Value == randVal) ||
                 (x.cubePosition == _cells[i].cubePosition && x.Value == randVal)))
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
                    foreach (var t in _cells)
                        t.Value = 0.ToString();
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

         bool IsInRow(int x, string value)
        {

            for (int y = 0; y < 9; y++)
                if (_testcells[x, y].Value == value)
                    return true;
            return false;
        }

         bool IsInCol(int y, string value)
        {
            for (int x = 0; x < 9; x++)
                if (_testcells[x, y].Value == value)
                    return true;
            return false;
        }

         bool IsInCube(int x, int y, string value)
        {
            var startX = x / 3 * 3;
            var startY = y / 3 * 3;
            var endX = startX + 3;
            var endY = startY + 3;

            for (int a = startX; a < endX; a++)
                for (int b = startY; b < endY; b++)
                    if (_testcells[a, b].Value == value)
                        return true;

            return false;
        }

        public void experimentalGeneration()
        {
            byte counter = 0;
            string random;
            var rand = new Random();
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    random = rand.Next(1, 10).ToString();

                    if (IsInRow(x, random) || IsInCol(y, random) || IsInCube(x, y, random))
                    {
                        counter += 1;
                        y -= 1;
                        if (counter > 40)
                        {
                            x = -1;
                            counter = 0;
                            _testcells = new Cell[9, 9];
                            break;
                        }
                    }
                    else
                    {
                        _testcells[x, y].Value = random;
                        counter = 0;
                    }

                }
            }
        }
    }
}
