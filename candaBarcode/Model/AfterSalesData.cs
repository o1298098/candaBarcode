using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace candaBarcode.Model
{
    public class AfterSalesData : INotifyPropertyChanged
    {
        [JsonProperty("FBillNo")]
        public string FBillNo { get; set; }
        [JsonProperty("Contact")]
        public string Contact { get; set; }
        public string ExpNumback { get; set; }
        public string FID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
