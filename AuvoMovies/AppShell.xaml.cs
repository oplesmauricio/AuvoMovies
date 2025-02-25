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

        protected override void OnAppearing()
        {
            //Nao consegui instalar nenhuma dll atraves do nuget que pudesse me ajudar com a notificacao do firebase para ios, implementei para android e vo ucontinuar pesquisando pra descobrir
            //estava vendo q mais algumas pessoas estao com esta dificuldade https://stackoverflow.com/questions/79413766/cannot-install-plugin-firebase-cloudmessaging-in-net-maui-project-path-too-lon
            //um dos erros eh o caminho dos arquivos https://developercommunity.visualstudio.com/t/Allow-building-running-and-debugging-a/351628
            base.OnAppearing();
#if IOS
            var x = this.menu.Items.Remove(this.EnviarNotificacao);
#endif
        }
    }
}
