using candaBarcode.action;
using candaBarcode.apiHelper;
using candaBarcode.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace candaBarcode.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AfterSalesPage2 : ContentPage
	{
       
        public AfterSalesPage2()
		{
			InitializeComponent ();
            listview.ItemsSource = App.aftersalesdata.Model.FEntityDetection;           
            scanbtn.Clicked+= async delegate {
                var ScanPage = new CustomScanPage(1);
                await Navigation.PushAsync(ScanPage);
            };
            searchbtn.Clicked += async delegate {
                var SearchPage = new AfterSalesSearchPage();
                SearchPage.Title = "搜索";
                await Navigation.PushAsync(SearchPage);
            };
            //RowDel.Clicked += RowDel_Clicked;
            RowAdd.Clicked += async delegate
            {
                var detailpage = new AfterSalesDetailsPage(-1);
                detailpage.Title = "收件明细";
                await Navigation.PushAsync(detailpage);
            };
            RowLoad.Clicked +=async delegate{
                AfterSalesSelectionPage selectionPage = new AfterSalesSelectionPage(6);
                await Navigation.PushAsync(selectionPage);
            };
            listview.ItemTapped += async delegate
             {
                 var selectdata = (AfterSalesDetectionModel)listview.SelectedItem;
                 int index = App.aftersalesdata.Model.FEntityDetection.IndexOf(selectdata);
                 var detailpage = new AfterSalesDetailsPage(index);
                 detailpage.Title = "收件明细";
                 await Navigation.PushAsync(detailpage);
             };
            SaveBtn.Clicked += SaveBtn_Clicked;
         }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            if (App.aftersalesdata.Model.FEntityDetection.Count > 0 && App.aftersalesdata.Model.FID!=0)
            {
                foreach (var a in App.aftersalesdata.Model.FEntityDetection)
                {
                    if (a.F_XAY_OutStock.FNumber == "s")
                        a.F_XAY_OutStock.FNumber = "CK002";
                    if (a.F_XAY_inStock.FNumber == "s")
                        a.F_XAY_inStock.FNumber = "CK004";
                }
                string s = JsonConvert.SerializeObject(App.aftersalesdata);
                string result= InvokeHelper.Save("XAY_ServiceApplication", s);
                KingdeeJsonResultModel kingdeeJsonResult = JsonConvert.DeserializeObject<KingdeeJsonResultModel>(result);
                if (kingdeeJsonResult.Result.ResponseStatus.IsSuccess == "true")
                {
                   
                    App.aftersalesdata = new AfterSalesData();
                    FBillNo.Text = App.aftersalesdata.Model.FBillNo;
                    Contact.Text = App.aftersalesdata.Model.Contact;
                    ExpNumback.Text = App.aftersalesdata.Model.ExpNumback;
                    //FID.Text = App.aftersalesdata.Model.FID.ToString();
                    listview.ItemsSource = App.aftersalesdata.Model.FEntityDetection;
                    await DisplayAlert("提示", "保存成功", "ok");
                }
                else
                {
                    await DisplayAlert("提示", "保存失败", "ok");
                }
            }
            else
            {
              await  DisplayAlert("提示","请选择单据并输入明细","ok");
            }
        }

        private void RowDel_Clicked(object sender, EventArgs e)
        {
            AfterSalesDetectionModel select = (AfterSalesDetectionModel)listview.SelectedItem;
            App.aftersalesdata.Model.FEntityDetection.Remove(select);
        }

        protected override void OnAppearing() 
        {
            base.OnAppearing();
            FBillNo.Text = App.aftersalesdata.Model.FBillNo;
            Contact.Text = App.aftersalesdata.Model.Contact;
            ExpNumback.Text = App.aftersalesdata.Model.ExpNumback;
            //FID.Text = App.aftersalesdata.Model.FID.ToString();
        }

        private async void MenuItem_ClickedAsync(object sender, EventArgs e)
        {
           
            var mi = ((MenuItem)sender);
            var selectdata= (AfterSalesDetectionModel)mi.CommandParameter;
            int index= App.aftersalesdata.Model.FEntityDetection.IndexOf(selectdata);
            var detailpage = new AfterSalesDetailsPage(index);
            detailpage.Title = "收件明细";
            await Navigation.PushAsync(detailpage);
        }
    }
}