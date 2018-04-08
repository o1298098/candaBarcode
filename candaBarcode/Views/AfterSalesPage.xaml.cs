using candaBarcode.action;
using candaBarcode.apiHelper;
using candaBarcode.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            listview.ItemsSource = App.aftersalesdata;           
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
         }
       
        private void RowDel_Clicked(object sender, EventArgs e)
        {
            AfterSalesData select = (AfterSalesData)listview.SelectedItem;
            App.aftersalesdata.Remove(select);
        }

        protected override void OnAppearing() 
        {
            base.OnAppearing();
            FBillNo.Text = App.teststring.Model.FBillNo;
            Contact.Text = App.teststring.Model.Contact;
            ExpNumback.Text = App.teststring.Model.ExpNumback;
            FID.Text = App.teststring.Model.FID;
        }

        private async void MenuItem_ClickedAsync(object sender, EventArgs e)
        {
           
            var mi = ((MenuItem)sender);
            var selectdata= (AfterSalesData)mi.CommandParameter;
            int index= App.aftersalesdata.IndexOf(selectdata);
            var detailpage = new AfterSalesDetailsPage(index);
            detailpage.Title = "收件明细";
            await Navigation.PushAsync(detailpage);
        }
    }
}