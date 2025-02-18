using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuvoMovies.Services.Interfaces
{
    public interface IApiService
    {
        Task<RespostaServico<T>> PostAsync<T>(object item, string url, List<KeyValuePair<string, string>> headers);

        Task<RespostaServico<T>> GetAsync<T>(string url, List<KeyValuePair<string, string>> headers);

        Task<RespostaServico<T>> PutAsync<T>(object item, string url, List<KeyValuePair<string, string>> headers);

        Task<RespostaServico<T>> DeleteAsync<T>(string url, List<KeyValuePair<string, string>> headers);

        Task<RespostaServico<T>> PostAnonymousAsync<T>(object item, string url);
    }
}
