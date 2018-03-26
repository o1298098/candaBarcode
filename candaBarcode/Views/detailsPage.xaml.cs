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
	public partial class detailsPage : ContentPage
	{
		public detailsPage (string str)
		{
			InitializeComponent ();
            str=str.Replace("]", "");
            str= str.Replace("[", "");
            string[] er= str.Split(',');
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < er.Length; i++)
            {
                if (i % 2 == 0) { stringBuilder.Append(er[i] + "   "); }
                else { stringBuilder.Append(er[i]+"\r\n"); }
                
            }
            label.Text = stringBuilder.ToString();
		}
	}
}