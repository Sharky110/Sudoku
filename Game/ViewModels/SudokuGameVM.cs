using Game.Commands;
using Game.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Game.ViewModels
{
    public class SudokuGameVM : Notificator
    {
        #region private variables
        private ObservableCollection<Cell> _cells;
        private int _difficult = 10;
        private GameField gameField;
        #endregion

        #region Properties
        public int Difficult
        {
            get => _difficult;
            set
            {
                var val = Math.Min(Math.Max(value, 1), 80);
                SetProperty(ref _difficult, val);
            }
        } 

        public ObservableCollection<Cell> Cells
        {
            get => _cells;
            set => SetProperty(ref _cells, value);
        }
        #endregion

        #region Commands
        public ICommand NewGameCommand { get; set; }
        public ICommand ClosePopupsCommand { get; set; }
        #endregion

        public SudokuGameVM()
        {
            gameField = new GameField();

            NewGame();

            NewGameCommand = new RelayCommand(c => NewGame());
            ClosePopupsCommand = new RelayCommand(c => ClosePopups());
        }

        private void NewGame()
        {
            Cells = new ObservableCollection<Cell>(gameField.Init(Difficult));
        }

        private void ClosePopups()
        {
            foreach (var cell in Cells)
            {
                cell.IsButtonPushed = false;
            }
        }
    }
}
