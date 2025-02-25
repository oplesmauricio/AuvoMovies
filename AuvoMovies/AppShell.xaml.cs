using AuvoMovies.Pages;

namespace AuvoMovies
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(FilmesPage), typeof(FilmesPage));
            Routing.RegisterRoute(nameof(FilmeDetailPage), typeof(FilmeDetailPage));
            Routing.RegisterRoute(nameof(FavoritosPage), typeof(FavoritosPage));
            Routing.RegisterRoute(nameof(EnviarNotificacaoPage), typeof(EnviarNotificacaoPage));
        }
    }
}
