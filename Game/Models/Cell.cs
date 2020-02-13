using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Game.Models
{
    public class Cell : INotifyPropertyChanged
    {
        private string _value;
        private int _id;
        private bool _isEnabled;
        private string _color;

        public int vertPosition;
        public int horPosition;
        public int cubePosition;

        public Cell(int id, int hor, int vert, int cube)
        {
            Id = id;
            vertPosition = vert;
            horPosition = hor;
            cubePosition = cube;
        }

        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }
        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public ICommand ButtonCommand { get; set; }
    }
}
