using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace candaBarcode.Model
{
   public class SeletionModel: INotifyPropertyChanged
    {
        private string _FID;
        private string _FName;
        public string FID
        {
            get { return _FID; }
            set {
                _FID = value;
                OnPropertyChanged("FID");
            }
        }
        public string FName {
            get { return _FName; }
            set { _FName = value;
                OnPropertyChanged("FName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
