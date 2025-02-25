using AuvoMovies.Models;
using AuvoMovies.Pages.Base;
using AuvoMovies.Services;
using AuvoMovies.Services.Interfaces;
using AuvoMovies.ViewModels;
using FluentResults;

//#if ANDROID
//using Plugin.Firebase.CloudMessaging;
//#endif

namespace AuvoMovies.Pages;
    
public partial class FilmesPage : BasePage
{
    private readonly IFilmeService _filmeService;
    private readonly FilmesViewModel vm;
    private readonly ISettings _settings;
    public FilmesPage(FilmesViewModel vm, IFilmeService service, ISettings _settings)
	{
		InitializeComponent();
        BindingContext = this.vm = vm;
        this._filmeService = service;
        this._settings = _settings;
	}

    protected override async void OnAppearing()
    {
        try
        {
            #region
            string requestToken = await new TMDBService().GetRequestTokenAsync();
            string jsonResponse = await new TMDBService().ValidateWithLoginAsync(requestToken, "mauriciodevelopermaui", "1234");

            await Launcher.OpenAsync($"https://www.themoviedb.org/authenticate/{requestToken}");


            // Aqui você teria uma tela de login que permite ao usuário inserir suas credenciais diretamente
            // ou realizar a autenticação de outra forma. Para simplicidade, estamos ignorando o fluxo de login.

            // Se o login for bem-sucedido, cria o token de sessão
            string sessionToken = await new TMDBService().CreateSessionTokenAsync(requestToken);



            // Agora, você pode usar o sessionToken para fazer chamadas API autenticadas
            string userInfo = await new TMDBService().GetUserInfoAsync(sessionToken);
            _settings.Token = sessionToken;
            var favoritos = await _filmeService.BuscarFavoritosAsync();
            #endregion

            base.OnAppearing();
            await NewMethod();

            if (!INternetConectada())
            {
                if (vm.Filmes.Any())
                    await DisplayAlert("Voce esta sem internet", "No entanto, fique tranquilo, pode navegar normalmente pela lista que vc ja estava olhando e continuar favoritando, logo que a coenxao for reestabelecida, sincronizamos tudo ;)", "Ok");
                else
                    await DisplayAlert("Voce esta sem internet", "Reestabelexa a conexao e tente novamente", "Ok");
            }
            else
            {
                await this.vm.SincronizarSQLiteApi();
                //await this.vm.Autenticar(); TMDB modificou sua forma de acessar com token de leitura fixo por usuario
                await this.vm.GetFilmesAsync();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Tivemos um probleminha =D", "Nao estamos conseguindos acessar a lista de filmes", "Ok");
        }
    }

    private static async Task NewMethod()
    {
        if (Preferences.ContainsKey("DeviceToken"))
        {
            var deviceToken = Preferences.Get("DeviceToken", "");


        }
    }

    private async void OnMovieSelected(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            var filmeSelecionado = (Filme)e.CurrentSelection.FirstOrDefault();
            await Shell.Current.GoToAsync($"{nameof(FilmeDetailPage)}", true, new Dictionary<string, object>
            {
                {"Filme",  filmeSelecionado},
                {"JaEstaFavoritado", false }
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Tivemos um probleminha =D", "Nao estamos conseguindos acessar a lista de filmes", "Ok");
        }
    }
}