namespace AuvoMovies.Services.Interfaces
{
    public interface IFirebaseService
    {
        Task EnviarNotificacao(string titulo, string decricao);
    }
}
