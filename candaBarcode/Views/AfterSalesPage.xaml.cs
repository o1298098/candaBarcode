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
	public partial class AfterSalesPage : ContentPage
	{
		public AfterSalesPage()
		{
			InitializeComponent ();
            string str = "{\"FormId\":\"XAY_ServiceApplication\",\"FieldKeys\":\"FBillNo,F_QiH_Contact,F_XAY_ExpNumback\",\"FilterString\":\"F_XAY_ExpNumback='700401598134'\",\"OrderString\":\"\",\"TopRowCount\":\"0\",\"StartRow\":\"0\",\"Limit\":\"0\"}";

        }
	}
}