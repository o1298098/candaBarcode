using candaBarcode.Forms;
using candaBarcode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static candaBarcode.Model.AfterSalesData2;

namespace candaBarcode.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AfterSalesDetailsPage :TabbedPage
	{
        
        public AfterSalesDetailsPage (int index)
		{
			InitializeComponent ();
            App.detection = new Detection();
            App.AfterSalesDetailPagedata = new AfterSalesDetailModel();
            if (index != -1)
            {
               
            }
            insertbtn.Clicked += async delegate {               
                if (index == -1)
                {
                }
                else
                {
                }
                await Navigation.PopAsync();
            };
            selectproduct.Clicked += async delegate { await GoToSelect(1); };
            PiCi.TextChanged += delegate { App.AfterSalesDetailPagedata.batch = PiCi.Text;App.detection.F_XAY_Flot = PiCi.Text; };
            InStockNumber.TextChanged += delegate { App.AfterSalesDetailPagedata.number=PiCi.Text;App.detection.F_XAY_DetQty = Convert.ToInt32(PiCi.Text); };
            typetbtn.Clicked += async delegate { await GoToSelect(2); };
            Reasont.TextChanged += delegate { App.AfterSalesDetailPagedata.reasont=Reasont.Text;App.detection.F_QiH_FalutReason = Reasont.Text; };
            typewbtn.Clicked += async delegate { await GoToSelect(3); };
            Infow.
            InStockbtn.Clicked += async delegate { await GoToSelect(4); };
            OutStockbtn.Clicked += async delegate { await GoToSelect(5); };


        }

      
        private async Task GoToSelect(int mode)
        {
            AfterSalesSelectionPage selectionPage = new AfterSalesSelectionPage(mode);
            await Navigation.PushAsync(selectionPage);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            selectproduct.Text = App.AfterSalesDetailPagedata.product;
            PiCi.Text = App.AfterSalesDetailPagedata.batch;
            InStockNumber.Text = App.AfterSalesDetailPagedata.number;
            typetbtn.Text = App.AfterSalesDetailPagedata.typet;
            Reasont.Text = App.AfterSalesDetailPagedata.reasont;
            typewbtn.Text = App.AfterSalesDetailPagedata.typew;
            Infow.Text = App.AfterSalesDetailPagedata.infow;
            InStockbtn.Text = App.AfterSalesDetailPagedata.instock;
            OutStockbtn.Text = App.AfterSalesDetailPagedata.outstock;
        }
    }
}