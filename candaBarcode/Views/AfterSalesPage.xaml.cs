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
	public partial class AfterSalesPage : ContentPage
	{
       
        public AfterSalesPage()
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
            RowDel.Clicked += RowDel_Clicked;
            RowAdd.Clicked += async delegate
            {
                var detailpage = new AfterSalesDetailsPage(-1);
                detailpage.Title = "收件明细";
                await Navigation.PushAsync(detailpage);
            };
            SaveBtn.Clicked += SaveBtn_Clicked;
         }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            if (App.aftersalesdata.Model.FEntityDetection.Count > 0 && App.aftersalesdata.Model.FID!=0)
            {
                string s = JsonConvert.SerializeObject(App.aftersalesdata);
                InvokeHelper.Save("XAY_ServiceApplication", s);
                await DisplayAlert("提示", "OKKKKKKK", "ok");
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
            FID.Text = App.aftersalesdata.Model.FID.ToString();
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