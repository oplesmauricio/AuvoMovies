using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuvoMovies.Services.Interfaces;

namespace AuvoMovies.Services
{
    public class Settings : ISettings
    {
        public string UrlBaseTMDB { get => "https://api.themoviedb.org/3/"; }
        public string UrlApiTMDb { get => $"{UrlBaseTMDB}discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc"; }
        public string UrlApiAutenticacaoTMDb { get => $"{UrlBaseTMDB}authentication/token/new?api_key="; }
        public string UrlImagePathTMDB { get => "https://image.tmdb.org/t/p/w500"; }

        //public string UrlApi { get => DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5001/" : "http://localhost:5001/"; }
        //public string UrlSocket { get => DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5002/" : "http://localhost:5002/"; }

        public string UrlApi { get => "http://mauriciocph-001-site1.gtempurl.com/"; }
        public string UrlSocket { get => "ws://mauricionetwork-001-site1.gtempurl.com"; }

        public string ApiKeyTMDB { get => "c46e5cc683764f4b99c03638e3ad0f09"; }
        public string Token { get => "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJjNDZlNWNjNjgzNzY0ZjRiOTljMDM2MzhlM2FkMGYwOSIsIm5iZiI6MTczOTkwNzg2Ni4yMTIsInN1YiI6IjY3YjRlMzFhYjY4N2U3MzhmMTU2NWNlZSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.nrkwK_r9tDfP2CTxmK_lvACg30LPkOgrlQvzngNIHUc"; }
        //public string Token { get; set; }
    }
}
