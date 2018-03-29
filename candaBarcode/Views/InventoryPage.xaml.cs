using candaBarcode.action;
using candaBarcode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace candaBarcode.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InventoryPage : ContentPage
	{

		public InventoryPage ()
        {
            InitializeComponent();
            List<InventoryData> listdata = new List<InventoryData>();
            string content = "{\"FormId\":\"STK_Inventory\",\"FieldKeys\":\"FMATERIALID.FName,FMATERIALID.FNumber,FBASEQTY,FSTOCKID.FName\",\"FilterString\":\"\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";
            string[] results = Jsonhelper.JsonToString(content);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < results.Length; i++)
            {
                string txt = results[i].Replace("[", "");
                string[] array = txt.Split(',');
                string num = array[2].Split('.')[0];
                if (num != "0")
                {
                    listdata.Add(new InventoryData { FName = array[0], FNumber = array[1], FBaseQTY = num, Stock = array[3] });
                }
            }
            listview.ItemsSource = listdata;
            picker.SelectedIndexChanged += (sender, args) =>
            {
                var selecteditem = picker.SelectedItem;
                List<InventoryData> selectdata = listdata;
                var a = selectdata.Where(s => s.Stock == selecteditem.ToString());
                listview.ItemsSource = a;
            };
        }

       
    }
}