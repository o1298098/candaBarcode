using candaBarcode.apiHelper;
using Newtonsoft.Json.Linq;
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
            buttonScanCustomPage.Clicked += async delegate
            {
                var customScanPage = new CustomScanPage();
                await Navigation.PushAsync(customScanPage);
            };         

            var stack = new StackLayout();
            stack.Children.Add(buttonScanCustomPage);
            Content = stack;
        }


        
       
    }
}
