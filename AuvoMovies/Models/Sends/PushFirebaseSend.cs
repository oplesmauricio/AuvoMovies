using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuvoMovies.Models.Sends
{
    public class PushFirebaseSend
    {
        public List<string> registration_ids { get; set; } = new List<string>();
        public NOtification notification { get; set; }
        public object data { get; set; }
    }

    public class NOtification
    {
        public string title { get; set; }
        public string body { get; set; }
    }
}
