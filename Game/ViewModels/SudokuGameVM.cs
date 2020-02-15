using Game.Models;
using System.Collections.ObjectModel;

namespace Game.ViewModels
{
    public class SudokuGameVM : SudokuGameVMBase
    {
        

        private ObservableCollection<Cell> _cells;
        private int _difficult = 10;
        private string _currentDifficult = "Текущая сложность: ";

        public int Difficult 
        {
            get => _difficult;
            set => SetProperty(ref _difficult, value);
        }

        public string CurrentDifficult 
        {
            get => _currentDifficult;
            set => SetProperty(ref _currentDifficult, value);
        }

        public ObservableCollection<Cell> Cells
        {
            get => _cells;
            set => SetProperty(ref _cells, value);
        }

        public SudokuGameVM()
        {
            var gameField = new GameField();
            Cells = new ObservableCollection<Cell>(gameField.Init(Difficult));
            CurrentDifficult += Difficult.ToString();
        }
    }
}
