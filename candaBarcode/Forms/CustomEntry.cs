using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace candaBarcode.Forms
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty IsPickerProperty = BindableProperty.Create("IsPicker", typeof(bool), typeof(CustomEntry), false);
        public static readonly BindableProperty IsNumberProperty = BindableProperty.Create("IsNumber", typeof(bool), typeof(CustomEntry), false);
        public bool IsPicker
        {
            get
            {
                return (bool)base.GetValue(IsPickerProperty);
            }
            set
            {
                base.SetValue(IsPickerProperty, value);
            }
        }
        public bool IsNumber
        {
            get
            {
                return (bool)base.GetValue(IsNumberProperty);
            }
            set
            {
                base.SetValue(IsNumberProperty, value);
            }
        }
        public event EventHandler<ClickedEventArgs> Clicked;
    }
}
