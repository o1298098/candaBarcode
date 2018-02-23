using candaBarcode.apiHelper;
using candaBarcode.Forms;
using candaBarcode.Model;
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

namespace candaBarcode
{
	public partial class MainPage : ContentPage
	{
      
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
            Button buttonrefresh = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = "刷新",
                FontSize = 20,
                AutomationId = "refresh",
                WidthRequest = 150,
                HeightRequest = 60
            };
            ListView listView = new ListView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "list"
            };
            List<Listdata> list = new List<Listdata>();
            List<Listdata> list2 = new List<Listdata>();
            var customCell = new DataTemplate(typeof(CustomCell));
            customCell.SetBinding(CustomCell.IndexProperty, "Index");
            customCell.SetBinding(CustomCell.NumProperty, "Num");
            customCell.SetBinding(CustomCell.StateProperty, "State");
            listView.ItemTemplate = customCell;
            listView.ItemsSource = list;
            buttonScanCustomPage.Clicked += async delegate
            {
                var customScanPage = new CustomScanPage(list,out list2);
                await Navigation.PushAsync(customScanPage);
            };
            buttonrefresh.Clicked += delegate {list= listAdd(list,list2); listView.ItemsSource = list; };
            var stack = new StackLayout();
            stack.Children.Add(listView);
            stack.Children.Add(buttonScanCustomPage);
            stack.Children.Add(buttonrefresh);
            Content = stack;
        }
        public List<Listdata> listAdd(List<Listdata> list, List<Listdata> list2)
        {
            while (true)
            {
                List<Listdata> listdata = Clone<Listdata>(list2);

                if (list2.Count > 0)
                {
                    for (int i = listdata.Count; i <= 0; i--)
                    {
                        list.Add(listdata[i]);
                        list2.RemoveAt(i);
                    }
                }
            return listdata;
            }
        }

        public static List<T> Clone<T>(object List)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, List);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as List<T>;
            }
        }
    }
}
