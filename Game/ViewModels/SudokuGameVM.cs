using Game.Models;
using Game.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using SudokuEngine;
using System.Windows.Input;

namespace Game.ViewModels
{
    public class SudokuGameVM : SudokuGameVMBase
    {
        private Sudoku _sudoku;

        private ObservableCollection<CustomButton> _buttons;

        public ObservableCollection<CustomButton> Buttons
        {
            get => _buttons;
            set => SetProperty(ref _buttons, value);
        }

        #region Commands

        
        public ICommand SaveFileCommand { get; }

        #endregion

        public SudokuGameVM()
        {
            _sudoku = Sudoku.GetInstance();

            var dict = _sudoku.Initialize(29);

            

           Buttons = new ObservableCollection<CustomButton>(
               dict.Select(s => new CustomButton()
               {
                   Name = $" {s.Value} ",
                   Id = s.Key,
                   IsEnabled = string.IsNullOrWhiteSpace(s.Value),
                   Color = string.IsNullOrWhiteSpace(s.Value) ? "White" : "LightGray",
                   ButtonCommand = new RelayCommand(ButtonClick)
        }));
        }

        public void ButtonClick(object sender)
        {
            var customButton = ((sender as Button)?.DataContext as CustomButton);

            if (!Buttons[customButton.Id].IsEnabled)
                return;

            SetDefaultColor();

            var num = string.IsNullOrWhiteSpace(customButton.Name) ? "0" : customButton.Name;

            var ff = Convert.ToInt32(num);

            customButton.Name = num == "9" ? "1" : $"{ ff += 1}";
            customButton.Color = "LightGreen";

            _sudoku.SetValueToCell(new KeyValuePair<int, int>(customButton.Id, Convert.ToInt32(customButton.Name)));

            var dd = _sudoku.CheckCell(new KeyValuePair<int, int>(customButton.Id, Convert.ToInt32(customButton.Name)));

            foreach (var button in Buttons)
            {
                if (dd.Contains(button.Id))
                    button.Color = "Red";
            }
        }

        public void SetDefaultColor()
        {
            foreach (var button in Buttons)
            {
                button.Color = button.IsEnabled ? "White" : "LightGray";
                if (button.Color == "White" && !string.IsNullOrWhiteSpace(button.Name))
                    button.Color = "LightGreen";
            }
        }
    }
}
