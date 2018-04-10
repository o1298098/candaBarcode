using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using candaBarcode.Droid;
using candaBarcode.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
[assembly:ExportRenderer(typeof(CustomEntry),typeof(CustomEntryRenderer))]
namespace candaBarcode.Droid
{
    public class CustomEntryRenderer : EntryRenderer,TextView.IOnClickListener
    {
        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        public void OnClick(Android.Views.View v)
        {
            throw new NotImplementedException();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            CustomEntry customEntry =(CustomEntry)this.Element;
            GradientDrawable gd = new GradientDrawable();
            gd.SetColor(Android.Graphics.Color.White);
            Control.SetBackground(gd);
            if(customEntry.IsPicker)
            {
                Control.Clickable = true;
                Control.Focusable = false;
                Control.SetTextIsSelectable(false);
                Control.ContextClickable = true;
                Control.SetCursorVisible(false);
            }
            if (customEntry.IsNumber)
            {
                var keyboard = customEntry.Keyboard;
                keyboard = Keyboard.Numeric;
                Control.InputType = Control.InputType | Android.Text.InputTypes.ClassNumber;
            }           

         
        }
    }
}