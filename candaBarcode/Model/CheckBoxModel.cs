using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace candaBarcode.Model
{
    public class CheckBoxModel: INotifyPropertyChanged
    {
        private bool _isChecked;
        private string _textCheckBox;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                TextCheckBox = _isChecked ? "Is Enabled" : "Is Disabled";
                OnPropertyChanged();
            }
        }

        public string TextCheckBox
        {
            get => _textCheckBox;
            set
            {
                _textCheckBox = value;
                OnPropertyChanged();
            }
        }

        public Command OnCheckedChanged { get; set; }

        public CheckBoxModel()
        {
            OnCheckedChanged = new Command(OnCheckBoxChanged);
        }

        private void OnCheckBoxChanged()
        {
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
