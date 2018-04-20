using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    [Table("InfoTable")]
    public class InfoTable : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string EmsNum { get; set; }
        public string state { get; set; }
        public string DateTime { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public static implicit operator List<object>(InfoTable v)
        {
            throw new NotImplementedException();
        }
    }
}