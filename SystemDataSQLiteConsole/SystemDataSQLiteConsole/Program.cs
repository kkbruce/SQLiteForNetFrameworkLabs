using System;
using System.Data;

namespace SystemDataSQLiteConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string dbname = "my.sqlite";

            SQLiteHelper.CreateDatabase(dbname);

            string tableString =
                "CREATE TABLE IF NOT EXISTS Personal (Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT);";
            SQLiteHelper.CreateTable(dbname, tableString);

            string insertString =
                @"INSERT INTO Personal (Name) VALUES ('Bruce');INSERT INTO Personal (Name) VALUES ('中文呢？');";
            SQLiteHelper.CUDOperator(dbname, insertString);

            string readString = "SELECT * FROM Personal";
            var reader = SQLiteHelper.GetDataReader(dbname, readString);

            Console.WriteLine("DataReader");

            while (reader.Read())
            {
                Console.WriteLine($"Id: {reader.GetInt32(0)}, Name: {reader.GetString(1)}");
            }
            // DataReader 使用完畢在關閉。
            //if (sqlite.State == ConnectionState.Open) sqlite.Close();
            Console.WriteLine();
            Console.WriteLine("DataTable");

            var table = SQLiteHelper.GetDataTable(dbname, readString);
            // colum info
            foreach (DataColumn column in table.Columns)
            {
                Console.Write($"{column.ColumnName}  ");
            }
            Console.WriteLine();

            // row info
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    Console.Write($"{row[column]}  ");
                }
                Console.WriteLine();
            }

            string deleteString =
                @"DELETE FROM Personal;";
            SQLiteHelper.CUDOperator(dbname, deleteString);

            string dropTable =
                @"DROP TABLE Personal;";
            SQLiteHelper.CUDOperator(dbname, dropTable);

            Console.Read();
        }
    }
}
