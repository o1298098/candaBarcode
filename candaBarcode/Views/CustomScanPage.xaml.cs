﻿using candaBarcode.Forms;
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

namespace candaBarcode
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomScanPage : ContentPage
	{
       private ZXingScannerView zxing;
       private ZXingOverlay overlay;
       private List<Listdata> list = new List<Listdata>();
       private ListView listview;
       private Label label;
        public class Info
        {
            public string FLOGISTICNUM { get; set; }
           
        }

        public CustomScanPage(List<Listdata> listdata,out List<Listdata> listdata2):base()
		{
            this.list = listdata;
            listdata2 =list;
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
                FontSize=40

            };
            ZXing.Mobile.MobileBarcodeScanningOptions scanningOptions = new ZXing.Mobile.MobileBarcodeScanningOptions { DelayBetweenContinuousScans=1000 };
             zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Options= scanningOptions
             };
            zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(async () => {

                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    //zxing.IsAnalyzing = false;
                    // Show an alert
                    //await DisplayAlert("扫描条码", result.Text, "OK");
                    HandleScanResult(result);
                    // Navigate away
                    //await Navigation.PopAsync();
                });
            
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
            overlay.Children.Add(label, 0, 2);
            grid.Children.Add(overlay);


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
                    list.Add(new Listdata { Index = list.Count + 1, Num = result.Text, State = "已同步" });
                    label.Text = result.Text+"扫描成功";
                }
                else if (result2 == "2")
                { label.Text = result.Text + "重复扫描"; list.Add(new Listdata { Index = list.Count + 1, Num = result.Text, State = "重复" }); }
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
	