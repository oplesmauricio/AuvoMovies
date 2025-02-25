using SQLite;

namespace AuvoMovies.Models
{
    public class BaseSQLite
    {
        [PrimaryKey, AutoIncrement]
        public int IdSQLite { get; set; }
    }
}
