using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SudokuEngine;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Sudoku _sudoku;
        public MainWindow()
        {
            InitializeComponent();

            _sudoku = Sudoku.GetInstanse();
            var dict = _sudoku.Initialize(29);

            Buttons = new ObservableCollection<MyButton>(
                dict.Select(s => new MyButton()
                { 
                    Name = $" {s.Value} ", 
                    Id = $"{s.Key}", 
                    IsEnabled = true,// string.IsNullOrWhiteSpace(s.Value) ,
                    Color = Brushes.Gray
                }));
            DataContext = this;
        }

        public ObservableCollection<MyButton> Buttons { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            var num = string.IsNullOrWhiteSpace((string)(sender as Button).Content) ? "0" : (string)(sender as Button).Content;
            var ff = Convert.ToInt32(num);
            (sender as Button).Content = num == "9" ? "1" : $"{ ff+=1}";
            var dd = _sudoku.CheckCell(new KeyValuePair<int, int>(Convert.ToInt32(((sender as Button).DataContext as MyButton).Id), Convert.ToInt32((sender as Button).Content)));
            Buttons.Where(x => dd.Contains(Convert.ToInt32(x.Id))).Select(f => f.Color = Brushes.Red);
            for (int i = 0; i < 1; i++)
                Buttons[dd.ToList()[i]].Name = "Wow";

        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var num = string.IsNullOrWhiteSpace((string)(sender as Button).Content) ? "0" : (string)(sender as Button).Content;
            var ff = Convert.ToInt32(num);
            (sender as Button).Content = num == "1" ? "9" : $"{ ff -= 1}";
        }
    }

    public class MyButton
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public bool IsEnabled { get; set; }

        public SolidColorBrush Color { get; set; }
    }
}
