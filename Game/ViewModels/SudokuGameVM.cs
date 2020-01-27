using Game.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using SudokuEngine;

namespace Game.ViewModels
{
    public class SudokuGameVM : SudokuGameVMBase
    {
        private Sudoku _sudoku;

        public ObservableCollection<CustomButton> Buttons { get; set; }
        
        public SudokuGameVM()
        {
            _sudoku = Sudoku.GetInstanse();

            var dict = _sudoku.Initialize(29);

            Buttons = new ObservableCollection<CustomButton>(
               dict.Select(s => new CustomButton()
               {
                   Name = $" {s.Value} ",
                   Id = $"{s.Key}",
                   IsEnabled = true,// string.IsNullOrWhiteSpace(s.Value) ,
                    Color = "Blue"
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

            Buttons[0].Color = "Red"; 
        }
    }
}
