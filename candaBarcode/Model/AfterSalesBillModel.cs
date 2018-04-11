using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace candaBarcode.Model
{
   
   public class AfterSalesBillModel: INotifyPropertyChanged
    {
        private string _FBillNo;
        private string _Contact;
        private string _ExpNumback;
    /// <summary>
    /// 单据编号
    /// </summary>
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
        /// <summary>
        /// 联系人
        /// </summary>
        [JsonProperty("F_QiH_Contact")]
        public string Contact
        {
            get { return _Contact; }
            set
            {
                _Contact = value;
               OnPropertyChanged("Contact");
            }
        }
        /// <summary>
        /// 退回单号
        /// </summary>
        [JsonProperty("F_XAY_ExpNumback")]
        public string ExpNumback
        {
            get { return _ExpNumback; }
            set
            {
                _ExpNumback = value;
               OnPropertyChanged("ExpNumback");
            }
        }
        /// <summary>
        /// 单据主键
        /// </summary>
        [JsonProperty("FID")]
        public long FID { get; set; }
        /// <summary>
        /// 退回分录
        /// </summary>
        [JsonProperty("FEntityDetection")]
        public ObservableCollection<AfterSalesDetectionModel> FEntityDetection { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
    }
}
