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

            string[] results = null ;
            InvokeHelper.Login();
            string result = InvokeHelper.ExecuteBillQuery(content);
            if (result == "[]"|| result == "err") return results;
            result = result.Substring(0, result.Length - 1);
            result = result.Substring(1, result.Length - 1);
            result = result.Replace("\"", "");
            results = result.Split(new string[] { "]," }, StringSplitOptions.None);
            return results;
        }
    }
}
