using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SQLiteNetPCLConsole
{
    public static class ValuationHelper
    {
        public static IEnumerable<Valuation> QueryValuations(string database, Stock stock)
        {
            var db = new SQLiteConnection(database);
            return db.Query<Valuation>("Select * from Valuation where StockId = ?", stock.Id);
        }
    }
}
