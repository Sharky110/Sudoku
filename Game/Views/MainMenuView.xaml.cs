using Game.ViewModels;
using System;
using System.Windows;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : Window
    {
        private static MainMenuView _instance;
        public static MainMenuView GetInstance()
        {
            _instance ??= new MainMenuView();
            return _instance;
        }
        public MainMenuView()
        {
            _instance = this;
            InitializeComponent();
            DataContext = new MainMenuViewModel();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
    }
}
