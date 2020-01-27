using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

using Game.ViewModels;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            context = new SudokuGameVM();
            DataContext = context;
        }

        public SudokuGameVM context { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            context.BtnClick((Button)sender);
            //for (int i = 0; i < 1; i++)
            //    Buttons[Convert.ToInt32(ddd.ToList()[i].Id)].Color = Brushes.Red;
            

        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
           /// var num = string.IsNullOrWhiteSpace((string)(sender as Button).Content) ? "0" : (string)(sender as Button).Content;
         //   var ff = Convert.ToInt32(num);
          //  (sender as Button).Content = num == "1" ? "9" : $"{ ff -= 1}";
        }
    }
}
