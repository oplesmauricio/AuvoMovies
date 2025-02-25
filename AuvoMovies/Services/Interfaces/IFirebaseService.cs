using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuvoMovies.Services.Interfaces
{
    public interface IFirebaseService
    {
        Task EnviarNotificacao(string titulo, string decricao);
    }
}
