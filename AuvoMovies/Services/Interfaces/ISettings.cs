using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuvoMovies.Services.Interfaces
{
    public interface ISettings
    {
        string UrlBaseTMDB { get; }
        string UrlApiTMDb { get; }
        string UrlApiAutenticacaoTMDb { get; }
        string UrlImagePathTMDB { get; }
        string ApiKeyTMDB { get; }
        string Token { get; set;  }
        string DatabaseFilename { get; }
        SQLite.SQLiteOpenFlags Flags { get; }
        string DatabasePath { get; }
    }
}
