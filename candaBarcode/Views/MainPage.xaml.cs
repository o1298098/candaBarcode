using candaBarcode.apiHelper;
using candaBarcode.Forms;
using candaBarcode.Model;
using candaBarcode.Views;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace candaBarcode.Views
{
	public partial class MainPage : ContentPage
	{

        private ListView listView;
        public MainPage()
		{
            InitializeComponent();            
            List<string> s = new List<string>();
            StackLayout relativeLayout = new StackLayout {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 70,
            };
            Button buttonScanCustomPage = new Button
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(0, 0, 10, 10),
                Text = "扫描",
                FontSize = 20,
                AutomationId = "scan",
                WidthRequest = 70,
                HeightRequest = 70,
                Style = new Style(typeof(Button))
                {
                    Setters = { new Setter { Property = Button.CornerRadiusProperty, Value = 35 } }
                }
            };        
            listView = new ListView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "list"
            };
            var customCell = new DataTemplate(typeof(CustomCell));
            customCell.SetBinding(CustomCell.IndexProperty, "Index");
            customCell.SetBinding(CustomCell.NumProperty, "Num");
            customCell.SetBinding(CustomCell.StateProperty, "State");
            listView.ItemTemplate = customCell;
            listView.ItemsSource = App.list;
            listView.ItemTapped +=async delegate 
            {
                ScanListdata selectdata= (ScanListdata)listView.SelectedItem;
                await Navigation.PushAsync(new detailsPage(selectdata.Num));
            };
            buttonScanCustomPage.Clicked += async delegate
            {
                var customScanPage = new CustomScanPage(0);
                await Navigation.PushAsync(customScanPage);
            };
         
            var stack = new StackLayout();           
            relativeLayout.Children.Add(buttonScanCustomPage);
            stack.Children.Add(listView);
            stack.Children.Add(relativeLayout);        
            Content = stack;
        }
       
    }
}
