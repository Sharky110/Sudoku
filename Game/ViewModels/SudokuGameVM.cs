﻿using Game.Commands;
using Game.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Game.ViewModels
{
    public class SudokuGameVM : Notificator
    {
        #region private variables
        private ObservableCollection<Cell> _cells;
        private int _difficult = 10;
        #endregion

        #region Properties
        public int Difficult
        {
            get => _difficult;
            set => SetProperty(ref _difficult, value);
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
            NewGame();
            NewGameCommand = new RelayCommand(c => NewGame());
            ClosePopupsCommand = new RelayCommand(c => ClosePopups());
        }

        private void NewGame()
        {
            var gameField = new GameField();
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
