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
    public FilmesPage(FilmesViewModel vm)
	{
		InitializeComponent();
        BindingContext = this.vm = vm;
	}

    protected override async void OnAppearing()
    {
        try
        {
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