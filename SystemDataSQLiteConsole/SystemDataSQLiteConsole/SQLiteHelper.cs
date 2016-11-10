using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SystemDataSQLiteConsole
{
    /// <summary>
    /// SQLite 輔助類別
    /// </summary>
    public static class SQLiteHelper
    {
        /// <summary>
        /// 確認 SQLite 資料庫是否存在
        /// </summary>
        /// <param name="database">資料庫名稱</param>
        public static void CreateDatabase(string database)
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + database))
            {
                SQLiteConnection.CreateFile(database);
                Console.WriteLine($"建立新 {database} 完成。");
            }
        }

        /// <summary>
        /// 開啟資料庫連線
        /// </summary>
        /// <param name="database">資料庫名稱</param>
        /// <returns>SQLiteConnection</returns>
        private static SQLiteConnection OpenConnection(string database)
        {
            string connectionString = $"Data Source={database}";
            SQLiteConnection sqlite = new SQLiteConnection
            {
                ConnectionString = connectionString
            };
            if (sqlite.State == ConnectionState.Open) sqlite.Close();
            sqlite.Open();
            return sqlite;
        }

        /// <summary>
        /// 新增資料表
        /// </summary>
        /// <param name="database">資料庫名稱</param>
        /// <param name="commandString">建立 Table 的 SQL 字串</param>
        public static void CreateTable(string database, string commandString)
        {
            SQLiteConnection sqlite = OpenConnection(database);
            SQLiteCommand cmd = new SQLiteCommand(commandString, sqlite);
            SQLiteTransaction transaction = sqlite.BeginTransaction();

            try
            {
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.Message);
            }

            if (sqlite.State == ConnectionState.Open) sqlite.Close();
        }

        /// <summary>
        /// 執行 Insert, Update, Delete 作業
        /// </summary>
        /// <param name="database">資料庫名稱</param>
        /// <param name="commandString">執行異動的 SQL 字串</param>
        public static void CUDOperator(string database, string commandString)
        {
            SQLiteConnection sqlite = OpenConnection(database);
            SQLiteCommand cmd = new SQLiteCommand(commandString, sqlite);
            SQLiteTransaction transaction = sqlite.BeginTransaction();

            try
            {
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.Message);
            }

            if (sqlite.State == ConnectionState.Open) sqlite.Close();
        }

        /// <summary>
        /// 取得 DataReader 型別資料，返回時不能關閉連線。
        /// </summary>
        /// <param name="database">資料庫名稱</param>
        /// <param name="commandString">取得資料的 Select 的 SQL 字串</param>
        /// <returns>SQLiteDataReader</returns>
        public static SQLiteDataReader GetDataReader(string database, string commandString)
        {
            SQLiteConnection sqlite = OpenConnection(database);
            SQLiteCommand cmd = new SQLiteCommand(commandString, sqlite);
            SQLiteDataReader reader = cmd.ExecuteReader();

            return reader;
        }

        /// <summary>
        /// 取得 DataTable 型別資料
        /// </summary>
        /// <param name="database">資料庫名稱</param>
        /// <param name="commandString">取得資料的 Select 的 SQL 字串</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataTable(string database, string commandString)
        {
            SQLiteConnection sqlite = OpenConnection(database);
            SQLiteDataAdapter da = new SQLiteDataAdapter(commandString, sqlite);
            DataSet ds = new DataSet();
            ds.Clear();
            da.Fill(ds);
            var dataTable = ds.Tables[0];

            if (sqlite.State == ConnectionState.Open) sqlite.Close();

            return dataTable;
        }
    }
}
