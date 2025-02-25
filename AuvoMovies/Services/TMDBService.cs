using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class TMDBService
{
    private const string ApiKey = "c46e5cc683764f4b99c03638e3ad0f09";
    private const string BaseUrl = "https://api.themoviedb.org/3";

    private readonly HttpClient _httpClient;

    public TMDBService()
    {
        _httpClient = new HttpClient();
    }

    // Solicitar um token de Request
    public async Task<string> GetRequestTokenAsync()
    {
        var url = $"{BaseUrl}/authentication/token/new?api_key={ApiKey}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var jsonDocument = JsonDocument.Parse(jsonResponse);
        var requestToken = jsonDocument.RootElement.GetProperty("request_token").GetString();

        return requestToken;
    }

    // Criar um token de Sessão com o token de Request
    public async Task<string> CreateSessionTokenAsync(string requestToken)
    {
        var url = $"{BaseUrl}/authentication/session/new?api_key={ApiKey}";
        var content = new StringContent($"{{\"request_token\": \"{requestToken}\"}}", System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var jsonDocument = JsonDocument.Parse(jsonResponse);
        var sessionToken = jsonDocument.RootElement.GetProperty("session_id").GetString();

        return sessionToken;
    }

    // Usar o token de Sessão para autenticar o usuário e realizar outras requisições
    public async Task<string> GetUserInfoAsync(string sessionToken)
    {
        var url = $"{BaseUrl}/account?api_key={ApiKey}&session_id={sessionToken}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return jsonResponse;
    }

    public async Task<string> ValidateWithLoginAsync(string requestToken, string username, string password)
    {
        var url = $"https://api.themoviedb.org/3/authentication/token/validate_with_login?api_key={ApiKey}";
        var content = new StringContent(
            $"{{\"username\": \"{username}\", \"password\": \"{password}\", \"request_token\": \"{requestToken}\"}}",
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var jsonDocument = JsonDocument.Parse(jsonResponse);

        var sessionToken = jsonDocument.RootElement.GetProperty("request_token").GetString();
        return sessionToken;
    }
}
