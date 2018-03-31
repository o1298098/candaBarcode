using candaBarcode.Model;
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
	public partial class AfterSalesDetailsPage : ContentPage
	{
		public AfterSalesDetailsPage ()
		{
			InitializeComponent ();
            insertbtn.Clicked += async delegate {
                App.aftersalesdata.Add(new AfterSalesData { FBillNo = "mam", Contact = "asd", ExpNumback = "asd" });
                await Navigation.PopAsync();
            };

        }
    }
}