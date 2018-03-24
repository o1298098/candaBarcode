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

        private ListView listView;
        private List<Listdata> list, list2;
        public MainPage()
		{
            InitializeComponent();
            List<string> s = new List<string>();
            RelativeLayout relativeLayout = new RelativeLayout {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 70,
            };
            Button buttonScanCustomPage =new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions=LayoutOptions.CenterAndExpand,
                Text = "扫描",
                FontSize=20,
                AutomationId = "scan",
                WidthRequest=100,
                HeightRequest=60
            };
            Button buttonrefresh = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = "刷新",
                FontSize = 20,
                AutomationId = "refresh",
                WidthRequest = 100,
                HeightRequest = 60
            };
            listView = new ListView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "list"
            };
            list = new List<Listdata>();
            list2 = new List<Listdata>();
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
            relativeLayout.Children.Add(buttonrefresh, Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => {
                    return parent.X+30;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Y-230;
                }));
            relativeLayout.Children.Add(buttonScanCustomPage, Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => {
                    return parent.X+30;
                }),
                Constraint.RelativeToParent((parent) => {
                    return parent.Y+120;
                })
               );
            stack.Children.Add(listView);
            stack.Children.Add(relativeLayout);        
            Content = stack;
        }
        public List<Listdata> listAdd(List<Listdata> list, List<Listdata> list2)
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
        protected override bool OnBackButtonPressed()
        {
            list = listAdd(list, list2);
            listView.ItemsSource = list;
            return base.OnBackButtonPressed();
        }
    }
}
