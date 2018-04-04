using candaBarcode.Model;
using candaBarcode.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace candaBarcode
{
	public partial class App : Application
	{
        public static ObservableCollection<ScanListdata> list { get; set; }
        public static AfterSalesData teststring { get; set; }
        public static ObservableCollection<AfterSalesData> aftersalesdata { get; set; }
        public App ()
		{
			InitializeComponent();
            list = new ObservableCollection<ScanListdata>();
            teststring = new AfterSalesData {FBillNo="babababa",Contact="bababba",ExpNumback="bababbab",FID="sdfsdfsdf" };
            aftersalesdata = new ObservableCollection<AfterSalesData>();
            var pins = new List<MapLocationModel>() { };
            pins.Add(new MapLocationModel() { Name = "上海市浦东新区陆家嘴街道乳山二村", Longitude = 31.2317331909, Latitude = 121.5170146362 });
            MainPage = new MasterDetailPage1(); 
		}

		protected override void OnStart ()
		{
            // Handle when your app starts
           
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
