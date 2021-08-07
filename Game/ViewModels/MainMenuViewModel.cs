using Game.Commands;
using Game.Models;
using System.Windows.Input;

namespace Game.ViewModels
{
    class MainMenuViewModel : Notificator
    {
        #region Commands
        public ICommand NewGameCommand { get; set; }
        #endregion

        public MainMenuViewModel()
        {
            NewGameCommand = new RelayCommand(c => NewGame());
        }

        private void NewGame()
        {
            var gameWindow = GameView.GetInstance();
            var mainMenuWindow = MainMenuView.GetInstance();
            gameWindow.Show();
            mainMenuWindow.Hide();
        }
    }
}
