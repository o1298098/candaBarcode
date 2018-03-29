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
            FBillNo.Text = App.teststring[0];
            Contact.Text = App.teststring[1];
            ExpNumback.Text = App.teststring[2];

        }
	}
}