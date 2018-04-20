using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace candaBarcode.Droid.model
{
    [Table("InfoTable")]
    public class EmsNum : INotifyPropertyChanged
    {

        private string NUM;
        private string _state;
        private string _datetime;

        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public EmsNum() { }
        public EmsNum(string value)
        {
            this.NUM = value;
        }
        public string EMSNUM
        {
            get { return NUM; }
            set
            {
                NUM = value;
                OnPropertyChanged("EMSNUM");

            }
        }
        public string state
        {
            get { return _state; }
            set {
                _state = value;
                OnPropertyChanged("state");
            }
        }
        public string datetime
        {
            get { return _datetime; }
            set {
                _datetime = value;
                OnPropertyChanged("datetime");
            }
        }
        public int index
        {
            get;
            set;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}