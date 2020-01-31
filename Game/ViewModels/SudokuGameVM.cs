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
using Game.Commands;

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
                   IsEnabled = true,// string.IsNullOrWhiteSpace(s.Value) ,
                    Color = "White"
               }));
        }

        public void BtnClick(Button btn)
        {
            //(btn.DataContext as CustomButton).Id;
            
            var num = string.IsNullOrWhiteSpace((string)btn.Content) ? "0" : (string)btn.Content;

            var ff = Convert.ToInt32(num);

            btn.Content = num == "9" ? "1" : $"{ ff += 1}";

            var dd = _sudoku.CheckCell(new KeyValuePair<int, int>(Convert.ToInt32((btn.DataContext as CustomButton).Id), Convert.ToInt32(btn.Content)));

            var ddd = Buttons.Where(x => dd.Contains(Convert.ToInt32(x.Id)));

            foreach (var t in Buttons)
                t.Color = "Red";
        }
    }
}
