using AuvoMovies.Models;
using AuvoMovies.Models.Responses;
using AuvoMovies.Services.Interfaces;
using FluentResults;

namespace AuvoMovies.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly IApiService _apiService;
        private readonly ISettings _settings;
        private List<KeyValuePair<string, string>> _headers;

        public FilmeService(IApiService apiService, ISettings settings)
        {
            _apiService = apiService;
            _settings = settings;
        }

        public async Task<Result<IEnumerable<Filme>>> GetFilmesAsync()
        {
            _headers = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>
                    (
                        "Authorization",
                        _settings.Token
                    )
                };
            var response = await _apiService.GetAsync<TMBDList>(_settings.UrlApiTMDb, _headers);

            if (response.Sucesso)
                return response.Resposta.results;

            return Result.Fail($"{response.HttpStatus} - {response.Mensagem}");
        }

        public async Task<Result> AutenticarAsync()
        {
            var response = await _apiService.GetAnonymousAsync<TMDBAutenticacao>(_settings.UrlApiAutenticacaoTMDb + _settings.ApiKeyTMDB);

            if(response.Sucesso)
            {
                //_settings.Token = "Bearer " +  response.Resposta.request_token;
                return Result.Ok();
            }

            return Result.Fail($"{response.HttpStatus} - {response.Mensagem}");
        }
    }
}
