using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            TableView tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection
                    {
                        new SwitchCell
                        {
                            Text = "选项:"
                        },
                         new SwitchCell
                        {
                            Text = "选项2:"
                        },
                          new SwitchCell
                        {
                            Text = "选项3:"
                        }
                    }
                }
            };
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