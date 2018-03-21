using candaBarcode.apiHelper;
using candaBarcode.Forms;
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
        public class lala {
            public string Name { get; set; }
            public string Age { get; set; }
            public string Location { get; set; }
            
        }
        public MainPage()
		{
            InitializeComponent();
            List<string> s = new List<string>();
            Button buttonScanCustomPage =new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions=LayoutOptions.CenterAndExpand,
                Text = "扫描",
                FontSize=20,
                AutomationId = "scan",
                WidthRequest=150,
                HeightRequest=60
            };
            ListView listView = new ListView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "list"
            };
            List<lala> zhu = new List<lala> {
                new lala{Name="lalaallala",Age="1wKg",Location="LLLL" },
                new lala{Name="lalaallala",Age="1wKg",Location="LLLL" },
                new lala{Name="lalaallala",Age="1wKg",Location="LLLL" },
                new lala{Name="lalaallala",Age="1wKg",Location="LLLL" },
                new lala{Name="lalaallala",Age="1wKg",Location="LLLL" },
            };
            buttonScanCustomPage.Clicked += async delegate
            {
                var customScanPage = new CustomScanPage();
                await Navigation.PushAsync(customScanPage);
            };
            var datalist = new List<IDictionary<string, object>>();
            var item = new Dictionary<string, object>();
            item.Add("Name", "bababa");
            item.Add("Age", "bababa");
            item.Add("Location", "bababa");
            datalist.Add(item);
            var customCell = new DataTemplate(typeof(CustomCell));
            customCell.SetBinding(CustomCell.NameProperty, "Name");
            customCell.SetBinding(CustomCell.AgeProperty, "Age");
            customCell.SetBinding(CustomCell.LocationProperty, "Location");
            listView.ItemTemplate = customCell;
            listView.ItemsSource = zhu;
            var stack = new StackLayout();
            stack.Children.Add(listView);
            stack.Children.Add(buttonScanCustomPage);
            Content = stack;
        }
       

    }
}
