using candaBarcode.Forms;
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
		public AfterSalesDetailsPage (int index)
		{
			InitializeComponent ();
            if (index != -1)
            {
                FBillNo.Text = App.aftersalesdata[index].FBillNo;
                Contact.Text = App.aftersalesdata[index].Contact;
                ExpNumback.Text= App.aftersalesdata[index].ExpNumback;
            }
            check.Checked = false;
            check.Text = "lalalal";
            insertbtn.Clicked += async delegate {
                if (index == -1)
                {
                    App.aftersalesdata.Add(new AfterSalesData { FBillNo = FBillNo.Text, Contact = Contact.Text, ExpNumback = ExpNumback.Text });
                }
                else
                {
                    App.aftersalesdata[index].FBillNo = FBillNo.Text;
                    App.aftersalesdata[index].Contact = Contact.Text;
                    App.aftersalesdata[index].ExpNumback = ExpNumback.Text;
                }
                await Navigation.PopAsync();
            };

        }

      
    }
}