using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Collections.ObjectModel;
using System.Linq;
using candaBarcode.Droid.model;

namespace candaBarcode.action
{
    public class SqliteDataAccess
    {
        private SQLiteConnection DB;
        private static object collisionLock = new object();
        public ObservableCollection<InfoTable> collection { get; set; }
        public SqliteDataAccess()
        {
            DB.CreateTable<InfoTable>();
            this.collection = new ObservableCollection<InfoTable>();
            if (!DB.Table<InfoTable>().Any())
            {
                AddNewOptionTable();
            }
        }
        public void AddNewOptionTable()
        {
            this.collection.Add(new InfoTable {  });
            SaveAllOption();
        }
        public IEnumerable<InfoTable> Select(string key)
        {
            // Use locks to avoid database collitions
            lock (collisionLock)
            {
                var query = from cust in DB.Table<InfoTable>()
                            where cust.EmsNum == key
                            select cust;
                return query.AsEnumerable();
            }
        }
        public IEnumerable<InfoTable> SelectAll()
        {
            // Use locks to avoid database collitions
            lock (collisionLock)
            {
                var query = from cust in DB.Table<InfoTable>()
                            select cust;
                return query.AsEnumerable();
            }
        }

        public int SaveOption(InfoTable optionTable)
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
