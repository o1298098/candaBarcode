using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Collections.ObjectModel;
using candaBarcode.Model;
using Xamarin.Forms;
using candaBarcode.Interface;
using System.Linq;

namespace candaBarcode.action
{
    public class SqliteDataAccess
    {
        private SQLiteConnection DB;
        private static object collisionLock = new object();
        public ObservableCollection<OptionTableModel> collection { get; set; }
        public SqliteDataAccess()
        {
            DB = DependencyService.Get<IDatabaseConnection>().SQLiteConnection();
            DB.CreateTable<OptionTableModel>();
            this.collection = new ObservableCollection<OptionTableModel>();
            if (!DB.Table<OptionTableModel>().Any())
            {
                AddNewOptionTable();
            }
        }
        public void AddNewOptionTable()
        {
            this.collection.Add(new OptionTableModel {Type="StartUp", Titel = "默认打开界面", Key="售后工单" ,Value = "AfterSalesPage2" });
            this.collection.Add(new OptionTableModel { Type="DataSource" ,Titel="数据账套选择",Key="测试账套",Value= "5972f88ff9373a" });
            SaveAllOption();
        }
        public IEnumerable<OptionTableModel> Select(string key)
        {
            // Use locks to avoid database collitions
            lock (collisionLock)
            {
                var query = from cust in DB.Table<OptionTableModel>()
                            where cust.Type == key
                            select cust;
                return query.AsEnumerable();
            }
        }
        public IEnumerable<OptionTableModel> SelectAll()
        {
            // Use locks to avoid database collitions
            lock (collisionLock)
            {
                var query = from cust in DB.Table<OptionTableModel>()
                            select cust;
                return query.AsEnumerable();
            }
        }

        public int SaveOption(OptionTableModel optionTable)
        {
            lock (collisionLock)
            {
                if (optionTable.Id != 0)
                {
                    DB.Update(optionTable);
                    return optionTable.Id;
                }
                else
                {
                    DB.Insert(optionTable);
                    return optionTable.Id;
                }
            }
        }
        public void SaveAllOption()
        {
            lock (collisionLock)
            {
                foreach (var s in this.collection)
                {
                    if (s.Id != 0)
                    {
                        DB.Update(s);
                    }
                    else
                    {
                        DB.Insert(s);
                    }
                }

            }
        }
    }
}
