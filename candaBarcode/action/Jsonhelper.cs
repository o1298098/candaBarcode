using candaBarcode.apiHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace candaBarcode.action
{
   public class Jsonhelper
    {
        public static string[] JsonToString(string content)
        {
            
            InvokeHelper.Login();
            string result = InvokeHelper.ExecuteBillQuery(content);
            result = result.Substring(0, result.Length - 1);
            result = result.Substring(1, result.Length - 1);
            result = result.Replace("\"", "");
            string[] results = result.Split(new string[] { "]," }, StringSplitOptions.None);
            return results;
        }
    }
}
