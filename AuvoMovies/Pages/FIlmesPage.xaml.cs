using AuvoMovies.Models;
using AuvoMovies.Pages.Base;
using AuvoMovies.Services.Interfaces;
using AuvoMovies.ViewModels;

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
            base.OnAppearing();

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
                await this.vm.GetFilmesAsync();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Tivemos um probleminha =D", "Nao estamos conseguindos acessar a lista de filmes", "Ok");
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