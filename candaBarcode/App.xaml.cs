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
        public static ObservableCollection<Listdata> list { get; set; }
        public App ()
		{
			InitializeComponent();
            list = new ObservableCollection<Listdata>();
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
