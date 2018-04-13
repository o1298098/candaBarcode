using candaBarcode.action;
using candaBarcode.Model;
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
    public partial class MasterDetailPage1 : MasterDetailPage
    {
        public MasterDetailPage1()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            MasterPage.SettingBtn.Clicked += SettingBtn_Clicked;
            SqliteDataAccess access = new SqliteDataAccess();
            IEnumerable<OptionTableModel> link = access.Select("StartUp");
            string StartUp = string.Empty;
            string title = string.Empty;
            foreach (var s in link) { StartUp = s.Value;title = s.Key; }
            Type pagetype = typeof(MainPage); 
            switch (StartUp)
            {
                case "MainPage":
                    pagetype = typeof(MainPage);
                    break;
                case "InventoryPage":
                    pagetype = typeof(InventoryPage);
                    break;
                case "AfterSalesPage2":
                    pagetype = typeof(AfterSalesPage2);
                    break;
                case "MapPage":
                    pagetype = typeof(MapPage);
                    break;
            }
            var page = (Page)Activator.CreateInstance(pagetype);
            page.Title = title;
            Detail = new NavigationPage(page);
        }

        private void SettingBtn_Clicked(object sender, EventArgs e)
        {
            var page = (Page)Activator.CreateInstance(typeof(OptionPage));
            page.Title = "选项";
            Detail = new NavigationPage(page);
            IsPresented = false;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailPage1MenuItem;
            if (item == null)
                return;
            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;
            Detail = new NavigationPage(page);
            IsPresented = false;
            MasterPage.ListView.SelectedItem = null;
        }
    }
}