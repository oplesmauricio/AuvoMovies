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

        private const string LISTAR = "discover/movie";
        private const string FAVORITAR = "account/21827232/favorite";

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
            var response = await _apiService.GetAsync<TMBDList>(_settings.UrlBaseTMDB + LISTAR, _headers);

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

        public async Task<Result> Favoritar(int idFilme)
        {
            _headers = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>
                    (
                        "Authorization",
                        _settings.Token
                    )
                };

            var body = new Dictionary<string, string>
            {
                { "media_type", "movie" },
                { "media_id", idFilme.ToString() },
                { "favorite", "true" }
            };

            var response = await _apiService.PostAsync<TMBDList>(body, _settings.UrlBaseTMDB + FAVORITAR + "?api_key=" + _settings.ApiKeyTMDB, _headers);

            if (response.Sucesso)
                return Result.Ok();

            return Result.Fail($"{response.HttpStatus} - {response.Mensagem}");
        }

        public async Task<Result<IEnumerable<Filme>>> BuscarFavoritosAsync()
        {
            _headers = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>
                    (
                        "Authorization",
                        _settings.Token
                    )
                };

            var response = await _apiService.GetAsync<TMBDList>(_settings.UrlBaseTMDB + FAVORITAR + "/movies", _headers);

            if (response.Sucesso)
                return response.Resposta.results;

            return Result.Fail($"{response.HttpStatus} - {response.Mensagem}");
        }
    }
}
