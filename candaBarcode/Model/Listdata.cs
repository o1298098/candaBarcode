using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace candaBarcode.Model
{
    [Serializable]
    public class Listdata
    {
        public int Index { get; set; }
        public string Num { get; set; }
        public string State { get; set; }
    }
    
}
