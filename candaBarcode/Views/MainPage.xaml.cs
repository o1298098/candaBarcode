using candaBarcode.apiHelper;
using Java.IO;
using Newtonsoft.Json.Linq;
using SerialPort;
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
      
        public MainPage()
		{
            InitializeComponent();
            List<string> s = new List<string>();
            Button buttonScanCustomPage = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions=LayoutOptions.CenterAndExpand,
                Text = "扫描",
                FontSize=20,
                AutomationId = "scan",
                WidthRequest=150,
                HeightRequest=60
            };
            Button test = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = "测试",
                FontSize = 20,
                AutomationId = "test",
                WidthRequest = 150,
                HeightRequest = 60
            };
            Label la2 = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 25
            };
            test.Clicked += delegate
            {
                try
                {
                    SerialPortFinder serialPortFinder = new SerialPortFinder();
                    var entryValues = serialPortFinder.getAllDevicesPath();
                    String[] entries = serialPortFinder.getAllDevices();
                    SerialPort.SerialPort serialPort = new SerialPort.SerialPort(new File(entryValues[7]), 115200, 0);
                    //foreach (var a in entryValues)
                    //{
                    //    la2.Text = la2.Text+"  "+ a.ToString();
                    //}
                    la2.Text = "lalallal";


                }
                catch (Exception ex)
                {
                    la2.Text = ex.Message;
                }
            };           
            buttonScanCustomPage.Clicked += async delegate
            {
                var customScanPage = new CustomScanPage();
                await Navigation.PushAsync(customScanPage);
            };         

            var stack = new StackLayout();
            stack.Children.Add(buttonScanCustomPage);
            stack.Children.Add(test);
            stack.Children.Add(la2);
            Content = stack;
        }

      
    }
}
