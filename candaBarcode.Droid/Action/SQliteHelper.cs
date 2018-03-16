using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using candaBarcode.Droid.model;
using SQLite;

namespace candaBarcode.Droid.Action
{
    public class SQliteHelper
    {
        private string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "InfoTable.db3");
        private SQLiteConnection sqliteConn;
        private const string TableName = "InfoTable";

        public void insertAsync(string num, string state)
        {
            if (!File.Exists(dbPath))
            {
                createDatabase(dbPath);
            }
            sqliteConn = new SQLiteConnection(dbPath);
            var Table = sqliteConn.GetTableInfo(TableName);
            if (Table.Count == 0)
            {
                sqliteConn.CreateTable<InfoTable>();                
            }
            InfoTable info = new InfoTable() { EmsNum = num, state = state, DateTime = System.DateTime.Now.ToShortDateString() };
            sqliteConn.Insert(info);
        }
        public List<InfoTable> selectAsync(string num,string date)
        {
            if (!File.Exists(dbPath))
            {
                createDatabase(dbPath);
            }
            sqliteConn = new SQLiteConnection(dbPath);
            var Table = sqliteConn.GetTableInfo(TableName);
            if (Table.Count == 0)
            {
                sqliteConn.CreateTable<InfoTable>();
            }
            var Infos = sqliteConn.Table<InfoTable>();
            var Info = Infos.Where(p => p.EmsNum.Contains(num)||p.DateTime==date);
            List<InfoTable> list = Info.ToList();
            return list;
        }
        public void updateAsync(string num)
        {
            if (!File.Exists(dbPath))
            {
                createDatabase(dbPath);
            }
            sqliteConn = new SQLiteConnection(dbPath);
            var Table = sqliteConn.GetTableInfo(TableName);
            if (Table.Count == 0)
            {
                sqliteConn.CreateTable<InfoTable>();
            }
            TableQuery<InfoTable> Infos = sqliteConn.Table<InfoTable>();
            foreach (var q in Infos)
            {
                if (q.EmsNum == num&&q.state=="未同步")
                {
                    q.state = "已同步";
                }
            }
            sqliteConn.Update(Infos);
        }
        private string createDatabase(string path)
        {
            try
            {
                var connection = new SQLiteAsyncConnection(path);
                {
                    connection.CreateTableAsync<InfoTable>();
                    return "Database created";
                }
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }
    }
}