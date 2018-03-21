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
using Newtonsoft.Json;
using static candaBarcode.Droid.model.ListJson;

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
            list.Adapter = new SearchAdapter(this, items);
            Button searchbtn = FindViewById<Button>(Resource.Id.Searchbtn);
            EditText num = FindViewById<EditText>(Resource.Id.numtxt);
            EditText date = FindViewById<EditText>(Resource.Id.datetxt);
            searchbtn.Click += delegate {
                // SQliteHelper sql = new SQliteHelper();
                //items= sql.selectAsync(num.Text, date.Text);
                // RunOnUiThread(() => { list.Adapter = new SearchAdapter(this, items); });
                List<object> Parameters = new List<object>();
                items = new List<model.InfoTable>();
                Parameters.Add(num.Text);
                Parameters.Add(date.Text);
                string result = apiHelper.InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.GetList", Parameters);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("{result:");
                stringBuilder.Append(result);
                stringBuilder.Append("}");
                JsonClass s = JsonConvert.DeserializeObject<JsonClass>(stringBuilder.ToString());
                for (int i = 0; i < s.result.Count; i++)
                {
                    items.Add(new model.InfoTable {Id=i,EmsNum=s.result[i].FLOGISTICNUM,DateTime=s.result[i].F_XAY_SCANDATE,state=s.result[i].F_XAY_IFSCAN.ToString() });
                }
                list.Adapter = new SearchAdapter(this, items);

            };
            // Create your application here
        }
    }
}