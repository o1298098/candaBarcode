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
	public partial class AfterSalesSearchPage : ContentPage
	{
      private  ObservableCollection<AfterSalesData> listdata { get; set; }

        public AfterSalesSearchPage ()
		{
			InitializeComponent ();
            listdata = new ObservableCollection<AfterSalesData>();
            listview.ItemsSource = listdata;
            listview.ItemTapped += async delegate
            {
                AfterSalesData data = listview.SelectedItem as AfterSalesData;
                App.teststring.Model.FBillNo = data.FBillNo;
                App.teststring.Model.Contact = data.Contact;
                App.teststring.Model.ExpNumback = data.ExpNumback;
                App.teststring.Model.FID = data.FID;
                await Navigation.PopAsync();
            };
            listview.Refreshing += Listview_Refreshing;  
            listview.IsPullToRefreshEnabled = true;
            searchbar.SearchButtonPressed += Searchbar_SearchButtonPressed;
		}

        private void Listview_Refreshing(object sender, EventArgs e)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork +=async delegate {
                Thread.Sleep(1000);
                listdata.Clear();
                string content = "{\"FormId\":\"XAY_ServiceApplication\",\"FieldKeys\":\"FBillNo,F_QiH_Contact,F_XAY_ExpNumback,FID\",\"FilterString\":\"F_XAY_ExpNumback like '%" + searchbar.Text + "%'\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";
                string[] results = Jsonhelper.JsonToString(content);

                if (results == null)
                {
                   await DisplayAlert("提示", "木有结果", "OK");
                    return;
                }
                await Task.Run(() => {
                    for (int i = 0; i < results.Length; i++)
                    {
                        string txt = results[i].Replace("[", "");
                        string[] array = txt.Split(',');
                        listdata.Add(new AfterSalesData { FBillNo = array[0], Contact = array[1], ExpNumback = array[2], FID = array[3] });
                    }
                });
            };
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync();
        }
       
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listview.IsRefreshing = false;
        }

        private void Searchbar_SearchButtonPressed(object sender, EventArgs e)
        {
            listview.BeginRefresh();
        }

       
    }
}