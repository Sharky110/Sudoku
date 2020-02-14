using Game.Models;
using System.Collections.ObjectModel;

namespace Game.ViewModels
{
    public class SudokuGameVM : SudokuGameVMBase
    {
        private ObservableCollection<Cell> _cells;

        public ObservableCollection<Cell> Cells
        {
            get => _cells;
            set => SetProperty(ref _cells, value);
        }

        public SudokuGameVM()
        {
            var gameField = new GameField();

            var dict = gameField.Initialize(29);

            Cells = new ObservableCollection<Cell>(dict);
        }
    }
}
