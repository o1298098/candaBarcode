using candaBarcode.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace candaBarcode
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomScanPage : ContentPage
	{
        ZXingScannerView zxing;
        ZXingOverlay overlay;
        List<string> s = new List<string>();
        ListView listview;
        public CustomScanPage ():base()
		{
            listview = new ListView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "listview",  
                
            };
            ZXing.Mobile.MobileBarcodeScanningOptions scanningOptions = new ZXing.Mobile.MobileBarcodeScanningOptions { DelayBetweenContinuousScans=1000 };
             zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Options= scanningOptions
             };
            zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(() => 

                // Stop analysis until we navigate away so we don't keep reading barcodes
                //zxing.IsAnalyzing = false;
                // Show an alert
                //await DisplayAlert("扫描条码", result.Text, "OK");
                 HandleScanResult(result)
                    // Navigate away
                    //await Navigation.PopAsync();
                );
            
            overlay = new ZXingOverlay
            {
                //TopText = "请对准二维码",
                BottomText = "阴暗天气建议打开闪光灯",
                ShowFlashButton = true,
                ButtonText = "开灯"
            };
            overlay.FlashButtonClicked += (sender, e) =>
            {
                try
                {

                    if (!zxing.IsTorchOn)
                    {
                        sender.Text = "关灯";
                        //CrossLampState = true;
                        zxing.IsTorchOn = true;

                    }
                    else
                    {
                        sender.Text = "开灯";
                        zxing.IsTorchOn = false;
                    }
                }
                catch (Exception)
                {


                }
            };
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing,0,0);
            //overlay.Children.Add(listview, 0, 2);
            grid.Children.Add(overlay);
            grid.Children.Add(listview);


            // The root page of your application
            Content = grid;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }
       void HandleScanResult(ZXing.Result result)
        {
           
            if (result != null && !string.IsNullOrEmpty(result.Text))
                s.Add(result.Text);
            listview.ItemsSource = s;
        }


    }
}
	