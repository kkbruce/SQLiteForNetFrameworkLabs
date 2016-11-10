using System;
using System.Collections.Generic;

namespace SQLiteNetPCLConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string database = "my.sqlite";
            SQLiteHelper.CreateDatabase<Stock>(database);
            SQLiteHelper.CreateDatabase<Valuation>(database);
            Console.WriteLine();

            var s1 = new Stock()
            {
                Symbol = "中文測試"
            };
            var s2 = new Stock()
            {
                Symbol = "Hi SQLite!"
            };
            SQLiteHelper.Add(database, s1);
            SQLiteHelper.Add(database, s2);
            Console.WriteLine();

            // 批次新增
            IEnumerable<Stock> stocks = new List<Stock>()
            {
                new Stock()
                {
                    Symbol = "List中文"
                },
                new Stock()
                {
                    Symbol = "中文List"
                }
            };
            SQLiteHelper.AddList(database, stocks);
            ShowAllStock(database);
            Console.WriteLine();

            var u = new Stock()
            {
                Id = 1,
                Symbol = "English"
            };
            SQLiteHelper.Update(database, u);
            ShowAllStockUseQuery(database);
            Console.WriteLine();

            var d = new Stock()
            {
                Id = 1
            };
            SQLiteHelper.Delete(database, d);
            Console.WriteLine();

            SQLiteHelper.DeleteTable<Stock>(database);
            SQLiteHelper.DeleteTable<Valuation>(database);

            Console.Read();
        }

        private static void ShowAllStock(string database)
        {
            var stockData = SQLiteHelper.GetAll<Stock>(database);
            foreach (var stock in stockData)
            {
                Console.WriteLine($"Stock Id: {stock.Id}, Symbol: {stock.Symbol}");
            }
        }

        private static void ShowAllStockUseQuery(string database)
        {
            var stockData = SQLiteHelper.Query<Stock>(database, "SELECT * FROM Stock");
            foreach (var stock in stockData)
            {
                Console.WriteLine($"Stock Id: {stock.Id}, Symbol: {stock.Symbol}");
            }
        }
    }
}
