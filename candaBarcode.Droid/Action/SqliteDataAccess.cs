﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Collections.ObjectModel;
using System.Linq;
using candaBarcode.Droid.model;
using System.IO;

namespace candaBarcode.Droid
{
    public class SqliteDataAccess
    {
        private SQLiteConnection DB;
        private static object collisionLock = new object();
        public ObservableCollection<EmsNum> collection { get; set; }
        public SqliteDataAccess()
        {
            var dbname = "MyAppDb.db3";
            var path = Path.Combine(System.Environment.
                GetFolderPath(System.Environment.
                SpecialFolder.Personal), dbname);
            DB = new SQLiteConnection(path);
            DB.CreateTable<EmsNum>();
            this.collection = new ObservableCollection<EmsNum>();
            //if (!DB.Table<InfoTable>().Any())
            //{
            //    AddNewOptionTable();
            //}
        }
        //public void AddNewOptionTable()
        //{
        //    this.collection.Add(new InfoTable {  });
        //    SaveAllOption();
        //}
        public List<EmsNum> Select(string num, string date)
        {
            // Use locks to avoid database collitions
            lock (collisionLock)
            {
                var table = DB.Table<EmsNum>();
                var list = table.Where(cust => cust.EMSNUM.Contains(num) || cust.datetime == date);
                return list.ToList();
            }
        }
        public ObservableCollection<EmsNum> SelectAll()
        {
            // Use locks to avoid database collitions
            lock (collisionLock)
            {
                var table = DB.Table<EmsNum>();
                return new ObservableCollection<EmsNum>(table.AsEnumerable());
            }
        }

        public int SaveOption(EmsNum Table)
        {
            lock (collisionLock)
            {
                if (Table.Id != 0)
                {
                    DB.Update(Table);
                    return Table.Id;
                }
                else
                {
                    DB.Insert(Table);
                    return Table.Id;
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