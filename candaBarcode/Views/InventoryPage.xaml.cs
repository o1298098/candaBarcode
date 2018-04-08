using candaBarcode.action;
using candaBarcode.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            ObservableCollection<InventoryData> listdata = new ObservableCollection<InventoryData>();           
            listview.ItemsSource = listdata;
            listview.IsPullToRefreshEnabled = true;
            listview.Refreshing += delegate {
                listview.ItemsSource = listdata;
                listview.IsRefreshing = false;
            };
            listview.IsRefreshing = true;
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += async delegate {
                Thread.Sleep(1000);
                await Task.Run(() => {
                   string content = "{\"FormId\":\"STK_Inventory\",\"FieldKeys\":\"FMATERIALID.FName,FMATERIALID.FNumber,FBASEQTY,FSTOCKID.FName\",\"FilterString\":\"\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";
                   string[] results = Jsonhelper.JsonToString(content);
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
                });
            };
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
            picker.SelectedIndexChanged += (sender, args) =>
            {
                var selecteditem = picker.SelectedItem;
                ObservableCollection<InventoryData> selectdata = listdata;
                var a = selectdata.Where(s => s.Stock == selecteditem.ToString());
                listview.ItemsSource = a;
            };
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listview.IsRefreshing = false;
        }
    }
}