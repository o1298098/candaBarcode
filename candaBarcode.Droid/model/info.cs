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

namespace candaBarcode.Droid.model
{
   public class info : INotifyPropertyChanged
    {
        private string NUM;

        public event PropertyChangedEventHandler PropertyChanged;
        public info() { }
        public info(string value)
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
            get;
            set;
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