using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace candaBarcode.Forms
{
    public class CustomPicker:Picker
    {
        BindableProperty TextSizeProperty = BindableProperty.Create("TextSize",typeof(float),typeof(CustomPicker),(float)12);
        public float TextSize
        {
            get {
                return (float)base.GetValue(TextSizeProperty);
            }
            set {
                SetValue(TextSizeProperty, value);
            }
        }
    }
}
