using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class CustomButton : CustomButtonBase
    {
        private string _name;
        private string _id;
        private bool _isEndabled;
        private string _color;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        public bool IsEnabled
        {
            get => _isEndabled;
            set => SetProperty(ref _isEndabled, value);
        }
        public string Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }
    }
}
