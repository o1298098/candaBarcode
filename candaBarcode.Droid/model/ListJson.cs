using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace candaBarcode.Droid.model
{
   public class ListJson
    {
        public struct JsonClass
        {
            [JsonProperty("result")]
            public List<scanData> result { get; set; }

        }
        public class scanData
        {
            [JsonProperty("F_XAY_IFSCAN")]
            public int F_XAY_IFSCAN { get; set; }
            [JsonProperty("F_XAY_SCANDATE")]
            public string F_XAY_SCANDATE { get; set; }
            [JsonProperty("FLOGISTICNUM")]
            public string FLOGISTICNUM { get; set; }
        }
    }
}