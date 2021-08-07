using System.Windows.Input;
using System.Windows.Media;

namespace Game.Models
{
    public class Cell : Notificator
    {
        #region Private Variables
        private string _value;
        private int _id;
        private bool _isEnabled;
        private SolidColorBrush _color;

        private bool _isButtonPushed = false;
        #endregion

        #region Properties
        public string Value
        {
            get => _value;
            set => SetProperty(ref _value, value, "Value");
        }
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value, "Id");
        }
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value, "IsEnabled");
        }
        public SolidColorBrush Color
        {
            get => _color;
            set => SetProperty(ref _color, value, "Color");
        }
        public bool IsButtonPushed
        {
            get => _isButtonPushed;
            set => SetProperty(ref _isButtonPushed, value, "IsButtonPushed");
        }
        public int vertPosition { get; }
        public int horPosition { get; }
        public int cubePosition { get; }
        #endregion

        #region Commands
        public ICommand LeftClickCommand { get; set; }
        public ICommand button2Command { get; set; }
        #endregion

        public Cell(int id, int hor, int vert, int cube)
        {
            Id = id;
            vertPosition = vert;
            horPosition = hor;
            cubePosition = cube;
        }
    }
}
