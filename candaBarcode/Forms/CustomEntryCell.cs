using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace candaBarcode.Forms
{
    public class CustomEntryCell : EntryCell
    {
        public static readonly BindableProperty TextLineColorProperty = BindableProperty.Create("TextLineColor",typeof(Color),typeof(CustomEntryCell),Color.AliceBlue);

        public Color TextLineColor
        {
            get {
                return (Color)base.GetValue(TextLineColorProperty);
            }
            set {
                base.SetValue(TextLineColorProperty,value);
            }
        }
    }
}
