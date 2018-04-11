using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace candaBarcode.Model
{
   public class SeletionModel: INotifyPropertyChanged
    {
        private string _FNumber;
        private string _FName;
        private string _FID;
        public string FNumber
        {
            get { return _FNumber; }
            set {
                _FNumber = value;
                OnPropertyChanged("FNumber");
            }
        }
        public string FName {
            get { return _FName; }
            set { _FName = value;
                OnPropertyChanged("FName");
            }
        }
        public string FID
        {
            get { return _FID; }
            set
            {
                _FID = value;
                OnPropertyChanged("FID");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
