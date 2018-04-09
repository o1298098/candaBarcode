using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using candaBarcode.Droid;
using candaBarcode.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker),typeof(CustomPickerRenderer))]
namespace candaBarcode.Droid
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Context context) : base(context)
        {

        }
        //protected override EditText CreateNativeControl()
        //{
        //    CustomPicker picker =(CustomPicker)this.Element;
        //    return new EditText(Context) { Focusable = false, Clickable = true, Tag = this ,TextSize=picker.TextSize,};
        //}
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            CustomPicker picker = (CustomPicker)this.Element;
            Control.SetTextSize(ComplexUnitType.Sp, picker.TextSize);
        }
    }
}