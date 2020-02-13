using Game.Models;
using Game.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using SudokuEngine;
using System.Windows.Input;

namespace Game.ViewModels
{
    public class SudokuGameVM : SudokuGameVMBase
    {
        private Sudoku _sudoku;

        private ObservableCollection<Cell> _cells;

        public ObservableCollection<Cell> Cells
        {
            get => _cells;
            set => SetProperty(ref _cells, value);
        }

        #region Commands

        
        public ICommand SaveFileCommand { get; }

        #endregion

        public SudokuGameVM()
        {
            _sudoku = Sudoku.GetInstance();

            var dict = _sudoku.Initialize(29);



            Cells = new ObservableCollection<Cell>(
               dict.Select(s => new Cell()
               {
                   Value = $" {s.Value} ",
                   Id = s.Key,
                   IsEnabled = string.IsNullOrWhiteSpace(s.Value),
                   Color = string.IsNullOrWhiteSpace(s.Value) ? "White" : "LightGray",
                   ButtonCommand = new RelayCommand(ButtonClick)
        }));
        }

        public void ButtonClick(object sender)
        {
            var customButton = ((sender as Button)?.DataContext as Cell);

            if (!Cells[customButton.Id].IsEnabled)
                return;

            SetDefaultColor();

            var num = string.IsNullOrWhiteSpace(customButton.Value) ? "0" : customButton.Value;

            var ff = Convert.ToInt32(num);

            customButton.Value = num == "9" ? "1" : $"{ ff += 1}";
            customButton.Color = "LightGreen";

            _sudoku.SetValueToCell(new KeyValuePair<int, int>(customButton.Id, Convert.ToInt32(customButton.Value)));

            var dd = _sudoku.CheckCell(new KeyValuePair<int, int>(customButton.Id, Convert.ToInt32(customButton.Value)));

            foreach (var button in Cells)
            {
                if (dd.Contains(button.Id))
                    button.Color = "Red";
            }
        }

        public void SetDefaultColor()
        {
            foreach (var button in Cells)
            {
                button.Color = button.IsEnabled ? "White" : "LightGray";
                if (button.Color == "White" && !string.IsNullOrWhiteSpace(button.Value))
                    button.Color = "LightGreen";
            }
        }
    }
}
