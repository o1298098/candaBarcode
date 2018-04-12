using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using candaBarcode.Interface;
using candaBarcode.iOS;
using Foundation;
using SQLite;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_iOS))]

namespace candaBarcode.iOS
{
    public class DatabaseConnection_iOS : IDatabaseConnection
    {
        public SQLiteConnection SQLiteConnection()
        {
            var dbName = "MyAppDb.db3";
            string personalFolder =System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder =Path.Combine(personalFolder, "..", "Library");
            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteConnection(path);
        }
    }
}