using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace candaBarcode.Droid.model
{
    [Table("UserInfo")]
    public class InfoTable
    {
        [PrimaryKey, AutoIncrement, Collation("Id")]
        public int Id { get; set; }
        public string EmsNum { get; set; }
        public string state { get; set; }
    }
}