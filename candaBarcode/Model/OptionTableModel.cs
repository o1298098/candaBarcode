using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;
using System.Collections;

namespace candaBarcode.Model
{
    [Table("Option")]
    public class OptionTableModel : INotifyPropertyChanged
    {
        private int _id;
        private string _key;
        private string _value;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return _id; }
            set {
                _id = value;
                OnPropertyChanged("Id");

            }
        }
        public string Key
        {
            get { return _key; }
            set {
                _key = value;
                OnPropertyChanged("Key");
            }
        }
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
        }
    }
}
