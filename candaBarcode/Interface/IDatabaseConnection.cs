using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace candaBarcode.Interface
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection SQLiteConnection();
    }
}
