using Game.Models;
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

        public ICommand ButtonCommand { get; }
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
                   Id = $"{s.Key}",
                   IsEnabled = string.IsNullOrWhiteSpace(s.Value) ,
                    Color = string.IsNullOrWhiteSpace(s.Value) ? "White" : "LightGray"
               }));
        }

        public void BtnClick(Button btn)
        {
            var buttonId = (btn.DataContext as CustomButton).Id;

            if (!Buttons[Convert.ToInt32(buttonId)].IsEnabled)
                return;

            SetDefaultColor();

            var num = string.IsNullOrWhiteSpace((string)btn.Content) ? "0" : (string)btn.Content;

            var ff = Convert.ToInt32(num);

            btn.Content = num == "9" ? "1" : $"{ ff += 1}";

            var dd = _sudoku.CheckCell(new KeyValuePair<int, int>(Convert.ToInt32((btn.DataContext as CustomButton).Id), Convert.ToInt32(btn.Content)));

            foreach (var button in Buttons)
            {
                if (dd.Contains(Convert.ToInt32(button.Id)))
                    button.Color = "Red";
            }

        }

        public void SetDefaultColor()
        {
            foreach (var button in Buttons)
            {
                button.Color = button.IsEnabled ? "White" : "LightGray";
            }
        }
    }
}
