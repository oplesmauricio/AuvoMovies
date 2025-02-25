using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuvoMovies.Models.Responses
{
    public class TMDBSessao
    {
        public bool success { get; set; }
        public bool failure { get; set; }
        public int status_code { get; set; }
        public string status_message { get; set; }
        public string session_id { get; set; }
    }
}
