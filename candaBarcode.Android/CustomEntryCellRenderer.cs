using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using candaBarcode.Droid;
using candaBarcode.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntryCell), typeof(CustomEntryCellRenderer))]
namespace candaBarcode.Droid
{
    public class CustomEntryCellRenderer:EntryCellRenderer
    {
        EntryCellView _view;
        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            _view= (EntryCellView)base.GetCellCore(item, convertView, parent, context);
            var entryCell = (CustomEntryCell)Cell;
            GradientDrawable gd = new GradientDrawable();
            gd.SetColor(Android.Graphics.Color.White);
            gd.SetCornerRadius(10);
            gd.SetStroke(2, Android.Graphics.Color.LightGray);
            _view.EditText.SetBackground(gd);
            return _view;
        }

       
    }
}