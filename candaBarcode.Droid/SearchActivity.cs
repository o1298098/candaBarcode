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
using candaBarcode.Droid.Action;

namespace candaBarcode.Droid
{
    [Activity(Label = "扫描记录")]
    public class SearchActivity : Activity
    {
       List<model.InfoTable> items;
       SearchAdapter listAdapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SeachListView);
            items = new List<model.InfoTable>();
            ListView list = FindViewById<ListView>(Resource.Id.SearchlistView);
            listAdapter = new SearchAdapter(this, items);
            list.Adapter = listAdapter;
            Button searchbtn = FindViewById<Button>(Resource.Id.Searchbtn);
            EditText num = FindViewById<EditText>(Resource.Id.numtxt);
            EditText date = FindViewById<EditText>(Resource.Id.datetxt);
            searchbtn.Click += delegate {
                SQliteHelper sql = new SQliteHelper();
               items= sql.selectAsync(num.Text, date.Text);
                RunOnUiThread(() => { list.Adapter = new SearchAdapter(this, items); });
               
            };
            // Create your application here
        }
    }
}