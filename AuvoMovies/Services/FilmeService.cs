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
        private const string NOVO_TOKEN = "authentication/token/new";
        private const string VALIDAR_TOKEN = "authentication/token/validate_with_login";
        private const string SESSAO_TOKEN = "authentication/session/new";

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
            var response = await _apiService.GetAsync<TMBDList>(_settings.UrlBaseTMDB + LISTAR + "?session_id=" + _settings.Token + "&api_key=" + _settings.ApiKeyTMDB, _headers);

            if (response.Sucesso)
                return response.Resposta.results;

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

            var response = await _apiService.PostAsync<TMBDList>(body, _settings.UrlBaseTMDB + FAVORITAR + "?session_id=" + _settings.Token + "&api_key=" + _settings.ApiKeyTMDB, _headers);

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

            var response = await _apiService.GetAnonymousAsync<TMBDList>(_settings.UrlBaseTMDB + FAVORITAR + "/movies?session_id=" + _settings.Token + "&api_key=" + _settings.ApiKeyTMDB);

            if (response.Sucesso)
                return response.Resposta.results;

            return Result.Fail($"{response.HttpStatus} - {response.Mensagem}");
        }

        public async Task<Result<string>> PegarTokenASync()
        {
            var response = await _apiService.GetAnonymousAsync<TMDBAutenticacao>(_settings.UrlBaseTMDB + NOVO_TOKEN + "?api_key=" + _settings.ApiKeyTMDB);

            if (response.Sucesso)
                return response.Resposta.request_token;

            return Result.Fail($"{response.HttpStatus} - {response.Mensagem}");
        }


        public async Task<Result<string>> CriarTokenSessaoAsync(string requestToken)
        {
            var body = new Dictionary<string, string>
            {
                { "request_token", requestToken }
            };

            var response = await _apiService.PostAnonymousAsync<TMDBSessao>(body, _settings.UrlBaseTMDB + SESSAO_TOKEN + "?api_key=" + _settings.ApiKeyTMDB);

            if (response.Sucesso)
                return response.Resposta.session_id;

            return Result.Fail($"{response.HttpStatus} - {response.Mensagem}");
        }

        public async Task<Result<string>> ValidarToken(string requestToken, string username, string password)
        {
            var body = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password },
                { "request_token", requestToken }
            };

            var response = await _apiService.PostAnonymousAsync<TMDBAutenticacao>(body, _settings.UrlBaseTMDB + VALIDAR_TOKEN + "?api_key=" + _settings.ApiKeyTMDB);

            if (response.Sucesso)
                return response.Resposta.request_token;

            return Result.Fail($"{response.HttpStatus} - {response.Mensagem}");
        }

        public async Task<Result<string>> Logar(string username, string password)
        {
            var resultToekn = await PegarTokenASync();

            if (resultToekn.IsFailed)
                return resultToekn.ToResult();

            var resultValidacao = await ValidarToken(resultToekn.Value, username, password);

            if (resultValidacao.IsFailed)
                return resultValidacao.ToResult();

            var resultTokenSessao = await CriarTokenSessaoAsync(resultToekn.Value);

            if (resultTokenSessao.IsFailed)
                return resultValidacao.ToResult();

            return resultTokenSessao;

        }
    }
}
