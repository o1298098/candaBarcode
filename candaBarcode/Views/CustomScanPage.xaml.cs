using candaBarcode.Forms;
using candaBarcode.apiHelper;
using Plugin.Vibrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

//using BAH.BOS.WebAPI.Client;
using System.Globalization;
using Newtonsoft.Json.Linq;
using candaBarcode.Model;
using candaBarcode.action;

namespace candaBarcode
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomScanPage : ContentPage
	{
       private ZXingScannerView zxing;
       private ZXingOverlay overlay;
       private ListView listview;
       private Label label;
        public class Info
        {
            public string FLOGISTICNUM { get; set; }
           
        }

        public CustomScanPage(int Mode):base()
		{           
            listview = new ListView
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                AutomationId = "listview",                  
                
            };       
            label = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                AutomationId = "label",
                TextColor = Color.White,
                FontSize=30

            };
            ZXing.Mobile.MobileBarcodeScanningOptions scanningOptions = new ZXing.Mobile.MobileBarcodeScanningOptions { DelayBetweenContinuousScans=2000,PossibleFormats=new List<ZXing.BarcodeFormat> {ZXing.BarcodeFormat.CODE_128,ZXing.BarcodeFormat.CODE_39,ZXing.BarcodeFormat.CODE_93,ZXing.BarcodeFormat.EAN_13,ZXing.BarcodeFormat.EAN_8 } };
             zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Options= scanningOptions,
             };
            zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(async () => {

                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    //zxing.IsAnalyzing = false;
                    // Show an alert
                    //await DisplayAlert("扫描条码", result.Text, "OK");
                    if (Mode == 0) { HandleScanResult(result); }
                    else if (Mode == 1) {
                        zxing.IsAnalyzing = false;
                        string content = "{\"FormId\":\"XAY_ServiceApplication\",\"FieldKeys\":\"FBillNo,F_QiH_Contact,F_XAY_ExpNumback,FID\",\"FilterString\":\"F_XAY_ExpNumback='"+ result + "'and  FDocumentStatus='B'\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";
                        string[] results = Jsonhelper.JsonToString(content);
                        if (results == null)
                        {
                            await DisplayAlert("提示", "系统无此单号", "OK");
                        }
                        else
                        {
                            string txt = results[0].Replace("[", "");
                            string[] array = txt.Split(',');
                            App.aftersalesdata.Model.FBillNo = array[0];
                            App.aftersalesdata.Model.Contact = array[1];
                            App.aftersalesdata.Model.ExpNumback = array[2];
                            App.aftersalesdata.Model.FID = Convert.ToInt64(array[3].Replace("]", ""));
                            App.aftersalesdata.Model.FEntityDetection.Clear();
                        }
                        
                        await Navigation.PopAsync();
                    }
                    // Navigate away
                    //await Navigation.PopAsync();
                });
            
            overlay = new ZXingOverlay
            {
                ShowFlashButton = true,
            };
            overlay.FlashButtonClicked += (sender, e) =>
            {
                try
                {

                    if (!zxing.IsTorchOn)
                    {
                        sender.Image = "flashlighton.png";
                        //CrossLampState = true;
                        zxing.IsTorchOn = true;

                    }
                    else
                    {
                        sender.Image = "flashlightoff.png";
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
            grid.Children.Add(zxing);           
            overlay.Children.Add(label, 0, 0);
            grid.Children.Add(overlay);            
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
            var v = CrossVibrate.Current;
            v.Vibration(TimeSpan.FromSeconds(0.2));
            //var result2 = InvokeHelper.Login();
            //var iResult = JObject.Parse(result2)["LoginResultType"].Value<int>();
            //if (iResult == 1 || iResult == -5)
            //{
            List<object> Parameters = new List<object>();
            Parameters.Add(result.Text);
            try
            {
                string result2 = InvokeHelper.AbstractWebApiBusinessService("Kingdee.BOS.WebAPI.ServiceExtend.ServicesStub.CustomBusinessService.ExecuteService2", Parameters);
                if (result2 == "1")
                {
                    App.list.Add(new ScanListdata { Index = App.list.Count + 1, Num = result.Text, State = "已同步" });
                    label.Text = result.Text+"扫描成功";
                }
                else if (result2 == "2")
                { label.Text = result.Text + "重复扫描"; App.list.Add(new ScanListdata { Index = App.list.Count + 1, Num = result.Text, State = "重复" }); }
                else
                {
                    label.Text = "无此记录";
                }

            }
            catch (Exception ex)
            {
                label.Text = ex.Message;
            }

        }
    }
}
	