using AuvoMovies.Services.Interfaces;

namespace AuvoMovies.Services
{
    public class Settings : ISettings
    {
        public string UrlBaseTMDB { get => "https://api.themoviedb.org/3/"; }
        public string UrlApiTMDb { get => $"{UrlBaseTMDB}discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc"; }
        public string UrlApiAutenticacaoTMDb { get => $"{UrlBaseTMDB}authentication/token/new?api_key="; }
        public string UrlImagePathTMDB { get => "https://image.tmdb.org/t/p/w500"; }
        public string ApiKeyTMDB { get => "c46e5cc683764f4b99c03638e3ad0f09"; }
        public string Token { get; set; }
        public string DatabaseFilename { get => "AuvoMovies.db3"; }
        public SQLite.SQLiteOpenFlags Flags
        {
            get
            {
                return SQLite.SQLiteOpenFlags.ReadWrite |
                SQLite.SQLiteOpenFlags.Create |
                SQLite.SQLiteOpenFlags.SharedCache;
            }
        }
        public string DatabasePath { get => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename); }
    }
}
