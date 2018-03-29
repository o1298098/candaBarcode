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
            str = str.Substring(0,str.Length-1);
            str = str.Substring(1, str.Length - 1);
            string[] result = str.Split(new string[]{"],"},StringSplitOptions.None);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                string txt= result[i].Replace("[", "");
                string[] array = txt.Split(',');
                stringBuilder.Append(array[0] + "   ");
                stringBuilder.Append(array[1].Split('.')[0]+"\r\n");
                
            }
            label.Text = stringBuilder.ToString();
		}
	}
}