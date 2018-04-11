using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace candaBarcode.Model
{
    public class AfterSalesData : INotifyPropertyChanged
    {
       
        private AfterSalesBillModel _Model;
        [JsonProperty("Creator")]
        public string Creator { get; set; }
        [JsonProperty("NeedUpDateFields")]
        public string[] NeedUpDateFields { get; set; }
        [JsonProperty("NeedReturnFields")]
        public string[] NeedReturnFields { get; set; }
        [JsonProperty("IsDeleteEntry")]
        public bool IsDeleteEntry { get; set; }
        [JsonProperty("SubSystemId")]
        public string SubSystemId { get; set; }
        [JsonProperty("Model")]
        public AfterSalesBillModel Model {
            get { return _Model; }
            set {
                _Model = value;
                OnPropertyChanged("Model");
            }
        }
           
        public event PropertyChangedEventHandler PropertyChanged;

        public AfterSalesData()
        {
            NeedUpDateFields = new string[] { "FEntityDetection", "F_XAY_Product", "F_XAY_InstockMaterial","F_XAY_REPRODUCT", "F_XAY_Flot", "F_XAY_DetQty", "F_XAY_isInStock", "F_XAY_inStock","F_QiH_Faulttypes", "F_QiH_FalutReason", "F_XAY_WAY", "F_XAY_ServiceInf", "F_XAY_MaterialName","F_XAY_Qty","F_XAY_OutStock","F_XAY_isOutStock", "F_XAY_OutMaterial" };
            Model = new AfterSalesBillModel();
            Model.FEntityDetection = new ObservableCollection<AfterSalesDetectionModel>();
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
