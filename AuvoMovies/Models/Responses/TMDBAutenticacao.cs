namespace AuvoMovies.Models.Responses
{
    public class TMDBAutenticacao
    {
        public bool success { get; set; }
        public string expires_at { get; set; }
        public string request_token { get; set; }
    }

}
