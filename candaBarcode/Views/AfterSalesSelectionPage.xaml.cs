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
                        App.detection.F_XAY_Product= data.FName;
                        App.detection.F_XAY_REPRODUCT.FNumber =data.FNumber;
                        App.detection.F_XAY_MaterialName.FNumber = data.FNumber;
                        App.detection.F_XAY_InstockMaterial.FMaterialID= data.FID;
                        App.detection.F_XAY_OutMaterial.FMaterialID= data.FID;
                        break;
                    case 2:
                        App.detection.typet = data.FName;
                        App.detection.F_QiH_Faulttypes.FNumber = data.FNumber;
                        break;
                    case 3:
                        App.detection.typew = data.FName;
                        App.detection.F_XAY_WAY.FNumber = data.FNumber;
                        break;
                    case 4:
                        App.detection.instock = data.FName;
                        App.detection.F_XAY_inStock.FNumber =data.FNumber;
                        App.detection.F_XAY_isInStock = 1;
                        break;
                    case 5:
                        App.detection.outstock = data.FName;
                        App.detection.F_XAY_OutStock.FNumber = data.FNumber;
                        App.detection.F_XAY_isOutStock = 1;
                        break;
                    case 6:
                        App.aftersalesdata.Model.FEntityDetection.Clear();
                       string content = "{\"FormId\":\"3d2267aea48748a8af1350d27b5b5ebb\",\"FieldKeys\":\"FNumber,FName\",\"FilterString\":\"F_XAY_Material="+ data.FID + "\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";//配件
                       string[] results = Jsonhelper.JsonToString(content);
                        if (results == null)
                        {
                            return;
                        }     
                        
                        for (int i = 0; i < results.Count(); i++)
                        {
                            string txt = results[i].Replace("[", "");
                            string[] array = txt.Split(',');
                            string FNumber = array[0];
                            string FName = array[1].Replace("]", "");
                            App.aftersalesdata.Model.FEntityDetection.Add(
                                new AfterSalesDetectionModel
                                {
                                    F_XAY_Product = FName,
                                    F_XAY_REPRODUCT = new AfterSalesDetectionModel.basenum() { FNumber = FNumber },
                                    F_XAY_MaterialName = new AfterSalesDetectionModel.basenum() { FNumber = FNumber },
                                    F_XAY_InstockMaterial = new AfterSalesDetectionModel.baseid() { FMaterialID = data.FID },
                                    F_XAY_OutMaterial = new AfterSalesDetectionModel.baseid() { FMaterialID = data.FID },
                                    F_XAY_DetQty="1"
                                }
                                    );
                        }
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
                        content = "{\"FormId\":\"3d2267aea48748a8af1350d27b5b5ebb\",\"FieldKeys\":\"FNumber,FName,F_XAY_Material\",\"FilterString\":\"\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";//配件
                        break;
                    case 2:
                        content = "{\"FormId\":\"BOS_ASSISTANTDATA_DETAIL\",\"FieldKeys\":\"FNumber,FDATAVALUE,FEntryID\",\"FilterString\":\" FID='582ab4c60d889f'\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";//故障类型
                        break;
                    case 3:
                        content = "{\"FormId\":\"BOS_ASSISTANTDATA_DETAIL\",\"FieldKeys\":\"FNumber,FDATAVALUE,FEntryID\",\"FilterString\":\" FID='582ab49c0d888b'\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";//处理方式
                        break;
                    case 4: content = "{\"FormId\":\"BD_STOCK\",\"FieldKeys\":\"FNumber,FName,FSTOCKID\",\"FilterString\":\"\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";//仓库
                        break;
                    case 5:
                        content = "{\"FormId\":\"BD_STOCK\",\"FieldKeys\":\"FNumber,FName,FSTOCKID\",\"FilterString\":\"\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";//仓库
                        break;
                    case 6:
                        content = "{\"FormId\":\"BD_MATERIAL\",\"FieldKeys\":\"FNumber,FName,FMaterialID\",\"FilterString\":\"\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";//物料
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
                        listdata.Add(new SeletionModel { FNumber = array[0], FName = array[1],FID= array[2].Replace("]","") });
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