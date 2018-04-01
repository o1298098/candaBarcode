using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace candaBarcode.Model
{
    public class AfterSalesData : INotifyPropertyChanged
    {
        private string _FBillNo;
        private string _Contact;
        private string _ExpNumback;

        [JsonProperty("FBillNo")]
        public string FBillNo
        {
            get { return _FBillNo; }
            set
            {
                _FBillNo = value;
                OnPropertyChanged("FBillNo");
            }
        }
        [JsonProperty("Contact")]
        public string Contact
        {
            get { return _Contact; }
            set
            {
                _Contact = value;
                OnPropertyChanged("Contact");
            }
        }
        public string ExpNumback
        {
            get { return _ExpNumback; }
            set
            {
                _ExpNumback = value;
                OnPropertyChanged("ExpNumback");
            }
        }
        public string FID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
