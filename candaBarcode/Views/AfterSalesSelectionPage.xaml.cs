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
	public partial class AfterSalesSelectionPage : ContentPage
	{
        private int Mode;
        private  ObservableCollection<SeletionModel> listdata { get; set; }

        public AfterSalesSelectionPage(int mode)
		{
			InitializeComponent ();
            Mode = mode;
            listdata = new ObservableCollection<SeletionModel>();
            listview.ItemsSource = listdata;
            listview.ItemTapped += async delegate
            {
                SeletionModel data = listview.SelectedItem as SeletionModel;
                switch (mode) {
                    case 1:
                        App.AfterSalesDetailPagedata.product = data.FName;
                        App.detection.F_XAY_InstockMaterial = data.FID;
                        break;
                    case 2:
                        App.AfterSalesDetailPagedata.typet = data.FName;
                        App.detection.F_QiH_Faulttypes = data.FID;
                        break;
                    case 3:
                        App.AfterSalesDetailPagedata.typew = data.FName;
                        App.detection.F_XAY_WAY = data.FID;
                        break;
                    case 4:
                        App.AfterSalesDetailPagedata.instock = data.FName;
                        App.detection.F_XAY_inStock = data.FID;
                        App.detection.F_XAY_isOutStock = 1;
                        break;
                    case 5:
                        App.AfterSalesDetailPagedata.outstock = data.FName;
                        App.detection.F_XAY_OutStock = data.FID;
                        App.detection.F_XAY_isOutStock = 1;
                        break;
                    default:return;
                }
                await Navigation.PopAsync();
            };
            listview.Refreshing += Listview_Refreshing;  
            listview.IsPullToRefreshEnabled = true;
            searchbar.SearchButtonPressed += Searchbar_SearchButtonPressed;
            searchbar.TextChanged += Searchbar_TextChanged;
            listview.BeginRefresh();
        }

        private void Listview_Refreshing(object sender, EventArgs e)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += async delegate
            {
                string content;
                listdata.Clear();
                switch (Mode) {
                    case 1:
                        content = "{\"FormId\":\"3d2267aea48748a8af1350d27b5b5ebb\",\"FieldKeys\":\"F_XAY_Material,FName\",\"FilterString\":\"\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";//配件
                        break;
                    case 2:
                        content = "{\"FormId\":\"BOS_ASSISTANTDATA_DETAIL\",\"FieldKeys\":\"FENTRYID,FDATAVALUE\",\"FilterString\":\" FID='582ab4c60d889f'\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";//故障类型
                        break;
                    case 3:
                        content = "{\"FormId\":\"BOS_ASSISTANTDATA_DETAIL\",\"FieldKeys\":\"FENTRYID,FDATAVALUE\",\"FilterString\":\" FID='582ab49c0d888b'\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";//处理方式
                        break;
                    case 4: content = "{\"FormId\":\"BD_STOCK\",\"FieldKeys\":\"FSTOCKID,FName\",\"FilterString\":\"\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";//仓库
                        break;
                    case 5:
                        content = "{\"FormId\":\"BD_STOCK\",\"FieldKeys\":\"FSTOCKID,FName\",\"FilterString\":\"\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";//仓库
                        break;
                    default:
                        return;
                }
                string[] results = Jsonhelper.JsonToString(content);

                if (results == null)
                {
                    //await DisplayAlert("提示", "木有结果", "OK");
                    return;
                }
                await Task.Run(() =>
                {
                    for (int i = 0; i < results.Length; i++)
                    {
                        string txt = results[i].Replace("[", "");
                        string[] array = txt.Split(',');
                        listdata.Add(new SeletionModel { FID = array[0], FName = array[1] });
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
            var a = listdata.Where(s => s.FName.Contains(searchbar.Text));
            listview.ItemsSource = a;
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var a = listdata.Where(s => s.FName.Contains(searchbar.Text));
            listview.ItemsSource = a;
        }


    }
}