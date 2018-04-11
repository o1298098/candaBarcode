using candaBarcode.Forms;
using candaBarcode.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static candaBarcode.Model.AfterSalesData;

namespace candaBarcode.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AfterSalesDetailsPage :TabbedPage
	{
        
        public AfterSalesDetailsPage (int index)
		{
			InitializeComponent ();
            App.detection = new AfterSalesDetectionModel();
            if (index != -1)
            {
                App.detection = App.aftersalesdata.Model.FEntityDetection[index];
            }
            insertbtn.Clicked += async delegate {               
                if (index == -1)
                {
                    if (App.detection.F_XAY_InstockMaterial.FMaterialID != "0")
                    { App.aftersalesdata.Model.FEntityDetection.Add(App.detection); }
                    else
                    {
                        await DisplayAlert("提示","请输入库产品","OK");
                        return;
                    }
                   
                }
                else
                {
                    App.aftersalesdata.Model.FEntityDetection[index] = App.detection;
                }
                await Navigation.PopAsync();
            };
            selectproduct.Clicked += async delegate { await GoToSelect(1); };
            PiCi.TextChanged += delegate { App.detection.F_XAY_Flot = PiCi.Text; };
            InStockNumber.TextChanged += delegate { App.detection.F_XAY_DetQty =InStockNumber.Text;App.detection.F_XAY_Qty = InStockNumber.Text; };
            typetbtn.Clicked += async delegate { await GoToSelect(2); };
            Reasont.TextChanged += delegate { App.detection.F_QiH_FalutReason = Reasont.Text; };
            typewbtn.Clicked += async delegate { await GoToSelect(3); };
            Infow.TextChanged += delegate { App.detection.F_XAY_ServiceInf = Infow.Text;};
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
            selectproduct.Text = App.detection.F_XAY_Product;
            PiCi.Text = App.detection.F_XAY_Flot;
            InStockNumber.Text = App.detection.F_XAY_DetQty;
            typetbtn.Text = App.detection.typet;
            Reasont.Text = App.detection.F_QiH_FalutReason;
            typewbtn.Text = App.detection.typew;
            Infow.Text = App.detection.F_XAY_ServiceInf;
            InStockbtn.Text = App.detection.instock;
            OutStockbtn.Text = App.detection.outstock;
        }
    }
}