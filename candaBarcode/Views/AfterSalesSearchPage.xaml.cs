using candaBarcode.action;
using candaBarcode.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace candaBarcode.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AfterSalesSearchPage : ContentPage
	{
		public AfterSalesSearchPage ()
		{
			InitializeComponent ();
		}

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            string content = "{\"FormId\":\"XAY_ServiceApplication\",\"FieldKeys\":\"FBillNo,F_QiH_Contact,F_XAY_ExpNumback,FID\",\"FilterString\":\"F_XAY_ExpNumback like '%"+searchbar.Text+"%'\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";
            string[] results = Jsonhelper.JsonToString(content);
            ObservableCollection<AfterSalesData> listdata = new ObservableCollection<AfterSalesData>();
            listview.ItemsSource = listdata;
            for (int i = 0; i < results.Length; i++)
            {
                string txt = results[0].Replace("[", "");
                string[] array = txt.Split(',');
                listdata.Add(new AfterSalesData {FBillNo=array[0],Contact=array[1],ExpNumback=array[2],FID=array[3] });             
            }
        }
    }
}