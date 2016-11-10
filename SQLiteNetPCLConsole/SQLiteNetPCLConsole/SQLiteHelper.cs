using System;
using System.Collections.Generic;
using SQLite;

namespace SQLiteNetPCLConsole
{
    /// <summary>
    /// SQLite 輔助類別
    /// 參考 https://github.com/praeclarum/sqlite-net 範例。
    /// </summary>
    public static class SQLiteHelper
    {
        private const int success= 1;
        private const int zeroSucess = 0;

        /// <summary>
        /// 確認 SQLite 資料庫與資料表是否存在，不存在會自動建立型別 T 資料表。
        /// </summary>
        /// <param name="database">資料庫名稱</param>
        public static void CreateDatabase<T>(string database)
        {
            var db = new SQLiteConnection(database);
            var c = db.CreateTable<T>();
            if (c == zeroSucess)
            {
                Console.WriteLine($"建立 {database} - {typeof(T)} 資料表完成。");
            }
            else
            {
                Console.WriteLine($"建立 {database} - {typeof(T)} 資料表失敗。");
            }
        }

        /// <summary>
        /// 刪除型別 T 資料表
        /// </summary>
        /// <typeparam name="T">資料型別</typeparam>
        /// <param name="database">資料庫名稱</param>
        public static void DeleteTable<T>(string database)
        {
            var db = new SQLiteConnection(database);
            var dt = db.DropTable<T>();
            if (dt == zeroSucess)
            {
                Console.WriteLine($"刪除 {database} - {typeof(T)} 資料表完成。");
            }
            else
            {
                Console.WriteLine($"刪除 {database} - {typeof(T)} 資料表失敗。");
            }

        }

        /// <summary>
        /// 取得所有 T 型別資料
        /// </summary>
        /// <typeparam name="T">資料型別</typeparam>
        /// <param name="database">資料庫名稱</param>
        /// <returns>TableQuery&lt;T&gt;</returns>
        public static TableQuery<T> GetAll<T>(string database) where T : new()
        {
            var db = new SQLiteConnection(database);
            return db.Table<T>();
        }

        public static IEnumerable<T> Query<T>(string database, string sqlCommand) where T : new()
        {
            var db = new SQLiteConnection(database);
            return db.Query<T>(sqlCommand, "");
        }

        /// <summary>
        /// Add 資料物件。
        /// </summary>
        /// <typeparam name="T">資料型別</typeparam>
        /// <param name="database">資料庫名稱</param>
        /// <param name="data">資料物件</param>
        public static void Add<T>(string database, T data)
        {
            var db = new SQLiteConnection(database);
            var t = db.Insert(data);
            if (t == success)
            {
                Console.WriteLine($"新增 {data.GetType()} - {t} 筆成功。");
            }
            else
            {
                Console.WriteLine($"新增 {data.GetType()} 失敗。");
            }
        }

        public static void AddList<T>(string database, IEnumerable<T> dataList)
        {
            var db = new SQLiteConnection(database);
            var t = db.InsertAll(dataList);
            if (t > zeroSucess)
            {
                Console.WriteLine($"批次新增 {dataList.GetEnumeratedType()} - {t} 筆成功。");
            }
            else
            {
                Console.WriteLine($"批次新增 {dataList.GetEnumeratedType()} 失敗。");
            }
        }

        /// <summary>
        /// Update 資料物件，必須含主鍵。
        /// </summary>
        /// <typeparam name="T">資料型別</typeparam>
        /// <param name="database">資料庫名稱</param>
        /// <param name="data">資料物件</param>
        public static void Update<T>(string database, T data)
        {
            var db = new SQLiteConnection(database);
            var u = db.Update(data);
            if (u == success)
            {
                Console.WriteLine($"更新 {data.GetType()}  - {u} 筆成功。");
            }
            else
            {
                Console.WriteLine($"更新 {data.GetType()} 失敗。");
            }
        }

        public static void Delete<T>(string database, T data)
        {
            var db = new SQLiteConnection(database);
            var d = db.Delete(data);
            if (d == success)
            {
                Console.WriteLine($"刪除 {data.GetType()} - {d} 筆成功。");
            }
            else
            {
                Console.WriteLine($"刪除 {data.GetType()} 失敗。");
            }
        }

        #region Extension

        public static Type GetListType<T>(this List<T> _)
        {
            return typeof(T);
        }

        public static Type GetEnumeratedType<T>(this IEnumerable<T> _)
        {
            return typeof(T);
        }

        #endregion

    }
}
