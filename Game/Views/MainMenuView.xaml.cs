using Game.ViewModels;
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
            InitializeComponent();
            DataContext = new MainMenuViewModel();
        }
    }
}
