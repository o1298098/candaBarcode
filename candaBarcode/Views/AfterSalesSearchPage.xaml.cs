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
      private  ObservableCollection<AfterSalesBillModel> listdata { get; set; }

        public AfterSalesSearchPage ()
		{
			InitializeComponent ();
            listdata = new ObservableCollection<AfterSalesBillModel>();
            listview.ItemsSource = listdata;
            listview.ItemTapped += async delegate
            {
                AfterSalesBillModel data = listview.SelectedItem as AfterSalesBillModel;
                App.aftersalesdata.Model.FBillNo = data.FBillNo;
                App.aftersalesdata.Model.Contact = data.Contact;
                App.aftersalesdata.Model.ExpNumback = data.ExpNumback;
                App.aftersalesdata.Model.FID = data.FID;
                App.aftersalesdata.Model.FEntityDetection.Clear();
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
                        string FBillNo = array[0];
                        string Contact = array[1];
                        string ExpNumback = array[2];
                        listdata.Add(new AfterSalesBillModel { FBillNo = FBillNo, Contact = Contact, ExpNumback = ExpNumback, FID = Convert.ToInt64(array[3].Replace("]","")) });
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