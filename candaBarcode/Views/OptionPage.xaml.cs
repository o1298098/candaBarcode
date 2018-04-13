using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using candaBarcode.action;
using candaBarcode.Model;
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
                Intent = TableIntent.Settings,
                Root = new TableRoot()
            };
            TableSection section  = new TableSection();
            foreach (var data  in optiondatas)
            {
                
                ViewCell cell = new ViewCell();
                StackLayout sl = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                            new Label{Text=data.Titel,WidthRequest=100,VerticalOptions=LayoutOptions.Center},                           
                        }
                };
                Picker picker = new Picker
                {
                    Title = data.Titel,
                    HorizontalOptions=LayoutOptions.FillAndExpand,
                };
                List<OptionTableModel> items=new List<OptionTableModel>();
                int index = 0;
                if (data.Type == "StartUp")
                {
                    items= new List<OptionTableModel>
                    {
                        new  OptionTableModel {Id=data.Id,Type=data.Type,Titel=data.Titel,Key="出库扫描",Value="MainPage" },
                        new  OptionTableModel {Id=data.Id,Type=data.Type,Titel=data.Titel,Key="库存查询",Value="InventoryPage" },
                        new OptionTableModel {Id=data.Id,Type=data.Type,Titel=data.Titel,Key="售后工单",Value="AfterSalesPage2" },
                        new  OptionTableModel {Id=data.Id,Type=data.Type,Titel=data.Titel,Key="地图",Value="MapPage" },
                    };
                    picker.ItemsSource = items;
                }
                else if (data.Type == "DataSource")
                {
                    items =new List<OptionTableModel>
                    {
                        new  OptionTableModel {Id=data.Id,Type=data.Type,Titel=data.Titel,Key="测试账套",Value="5972f88ff9373a" },
                        new OptionTableModel {Id=data.Id,Type=data.Type,Titel=data.Titel,Key="正式账套",Value="59a12c8ba824d2" },
                    };
                    picker.ItemsSource = items;
                }
                for (int i = 0; i < items.Count; i++)
                {
                    if (data.Key == items[i].Key)
                    {
                        index = i;
                        break;
                    }
                       
                }
                picker.ItemDisplayBinding = new Binding("Key");
                picker.SelectedIndex=index;
                picker.SelectedIndexChanged +=delegate 
                {
                    OptionTableModel saveModel = (OptionTableModel)picker.SelectedItem;
                    access.SaveOption(saveModel);
                };
                sl.Children.Add(picker);
                cell.View = sl;
                section.Add(cell);
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
                    tableView,
                }
            };
        }

       
    }
}