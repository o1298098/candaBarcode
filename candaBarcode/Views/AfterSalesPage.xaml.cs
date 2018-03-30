using candaBarcode.action;
using candaBarcode.apiHelper;
using System;
using System.Collections.Generic;
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
            scanbtn.Clicked+= async delegate {
                var ScanPage = new CustomScanPage(1);
                await Navigation.PushAsync(ScanPage);
            };  
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            FBillNo.Text = App.teststring.FBillNo;
            Contact.Text = App.teststring.Contact;
            ExpNumback.Text = App.teststring.ExpNumback;
            FID.Text = App.teststring.FID;
        }
    }
}