using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace candaBarcode.Model
{
   public class InventoryData : INotifyPropertyChanged
    {
        public string FName { get; set; }
        public string FNumber { get; set; }
        public string FBaseQTY { get; set; }
        public string Stock { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
