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

namespace candaBarcode
{
	public partial class MainPage : ContentPage
	{

        private ListView listView;
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
            listView.ItemSelected +=async delegate 
            {
                Listdata selectdata= (Listdata)listView.SelectedItem;
                InvokeHelper.Login();
                string content = "{\"FormId\":\"ECC_PickDeliverySchedule\",\"FieldKeys\":\"FMaterialId.FName,FQty\",\"FilterString\":\"FLogisticNum='" + selectdata.Num + "'\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";
                string result= InvokeHelper.ExecuteBillQuery(content);
                await Navigation.PushAsync(new detailsPage(result));
            };
            buttonScanCustomPage.Clicked += async delegate
            {
                var customScanPage = new CustomScanPage();
                await Navigation.PushAsync(customScanPage);
            };
         
            var stack = new StackLayout();           
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
       
    }
}
