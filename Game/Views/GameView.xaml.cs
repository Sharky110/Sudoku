using Game.ViewModels;
using System;
using System.Windows;

namespace Game
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameView : Window
    {
        private static GameView _instance;
        public static GameView GetInstance()
        {
            _instance ??= new GameView();
            return _instance;
        }
        private GameView()
        {
            InitializeComponent();
            DataContext = new GameViewModel();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
    }
}
