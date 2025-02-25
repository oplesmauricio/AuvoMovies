using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuvoMovies.Models.Responses
{
    public class PushFirebaseResponse : ValueChangedMessage<string>
    {
        public PushFirebaseResponse(string message) : base(message) { }
    }
}
