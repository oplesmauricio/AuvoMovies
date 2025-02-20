using AuvoMovies.Models;
using AuvoMovies.Pages.Base;
using AuvoMovies.Services;
using AuvoMovies.Services.Interfaces;
using AuvoMovies.ViewModels;

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
            //await this.vm.Autenticar(); TMDB modificou sua forma de acessar com token de leitura fixo por usuario
            await this.vm.GetFilmesAsync();
        }
        catch (Exception)
        {
            await DisplayAlert("Tivemos um probleminha =D", "Nao estamos conseguindos acessar a lista de filmes, chama o Maicon (se eu passar pode me chamar tb =D)", "Ok");
        }
    }

    private async void OnMovieSelected(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            var filmeSelecionado = (Filme)e.CurrentSelection.FirstOrDefault();
            await Shell.Current.GoToAsync($"{nameof(FilmeDetailPage)}", true, new Dictionary<string, object>
            {
                {"Filme",  filmeSelecionado}
            });
        }
        catch (Exception)
        {
            await DisplayAlert("Tivemos um probleminha =D", "Nao estamos conseguindos acessar a lista de filmes, chama o Maicon (se eu passar pode me chamar tb =D)", "Ok");
        }
    }
}