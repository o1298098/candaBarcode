using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using candaBarcode.action;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace candaBarcode.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OptionPage : ContentPage
	{
		public OptionPage ()
		{
			InitializeComponent ();
            SqliteDataAccess access = new SqliteDataAccess();
            var optiondatas=access.SelectAll();
            
            TableView tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot()
            };
            TableSection section  = new TableSection();
            foreach (var data  in optiondatas)
            {
                section.Add(new SwitchCell { Text = data.Key, On =data.Value=="0"?false:true});
            }
            tableView.Root.Add(section);
            switch (Device.RuntimePlatform)
            {
                case "iOS":
                    this.Padding = new Thickness(10,20,10, 5);
                    break;
                case "Android":
                    this.Padding = new Thickness(10, 10, 10, 5);
                    break;
                case "UWP":
                    this.Padding = new Thickness(10, 0, 10, 5);
                    break;
                default:
                    this.Padding = new Thickness(10, 0, 10, 5);
                    break;
            }
            
            this.Content = new StackLayout
            {
                Children =
                {
                    tableView
                }
            };
        }
	}
}