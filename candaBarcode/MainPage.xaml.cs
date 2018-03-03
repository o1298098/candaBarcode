using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace candaBarcode
{
	public partial class MainPage : ContentPage
	{      
        Button buttonScanCustomPage;
        Button buttonScanContinuously;
        public MainPage()
		{
            //InitializeComponent();
          List<string> s = new List<string>();

            buttonScanCustomPage = new Button
            {
                Text = "Scan",
                AutomationId = "scan",
            };
            buttonScanCustomPage.Clicked += async delegate {
                var customScanPage = new CustomScanPage();
                await Navigation.PushAsync(customScanPage);
            };
            buttonScanContinuously = new Button
            {
                Text = "Scan Continuously",
                AutomationId = "scanContinuously",
            };
            buttonScanContinuously.Clicked += async delegate {
                var scanPage = new ZXingScannerPage(new ZXing.Mobile.MobileBarcodeScanningOptions { DelayBetweenContinuousScans = 3000 });
                scanPage.OnScanResult += (result) =>
                    Device.BeginInvokeOnMainThread(() =>
                       DisplayAlert("Scanned Barcode", result.Text, "OK"));

                await Navigation.PushAsync(scanPage);
            };

            var stack = new StackLayout();
            stack.Children.Add(buttonScanCustomPage);
            stack.Children.Add(buttonScanContinuously);

            Content = stack;
        }


        async void scanButon_Clicked(object sender, EventArgs args)
        {
            var scanPage = new ZXingScannerPage();

            scanPage.OnScanResult += (result) => {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopAsync();
                    DisplayAlert("Scanned Barcode", result.Text, "OK");
                });
            };

            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);

        }
       
    }
}
