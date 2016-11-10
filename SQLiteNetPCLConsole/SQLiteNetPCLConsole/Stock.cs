using SQLite;

namespace SQLiteNetPCLConsole
{
    /// <summary>
    /// Stock is Class
    /// </summary>
    public class Stock
    {
        // Attribute from SQLite
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(8)]
        public string Symbol { get; set; }
    }
}
