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
        private string _type;
        private string _title;
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
        public string Type
        {
            get { return _type; }
            set {
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        public string Titel {
            get { return _title; }
            set {
                _title = value;
                OnPropertyChanged("Titel");
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
