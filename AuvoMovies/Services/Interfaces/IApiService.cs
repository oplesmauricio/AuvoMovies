namespace AuvoMovies.Services.Interfaces
{
    public interface IApiService
    {
        Task<RespostaServico<T>> PostAsync<T>(object item, string url, List<KeyValuePair<string, string>> headers);

        Task<RespostaServico<T>> GetAsync<T>(string url, List<KeyValuePair<string, string>> headers);

        Task<RespostaServico<T>> PutAsync<T>(object item, string url, List<KeyValuePair<string, string>> headers);

        Task<RespostaServico<T>> DeleteAsync<T>(string url, List<KeyValuePair<string, string>> headers);
        Task<RespostaServico<T>> GetAnonymousAsync<T>(string url);

        Task<RespostaServico<T>> PostAnonymousAsync<T>(object item, string url);
    }
}
