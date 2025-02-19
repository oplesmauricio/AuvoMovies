using AuvoMovies.Models;
using AuvoMovies.Services;
using AuvoMovies.Services.Interfaces;
using AuvoMovies.ViewModels;

namespace AuvoMovies.Pages;

public partial class FilmesPage : ContentPage
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
        base.OnAppearing();
        //await this.vm.Autenticar(); TMDB modificou sua forma de acessar com token de leitura fixo por usuario
        await this.vm.GetFilmesAsync();
    }

    private async void OnMovieSelected(object sender, SelectionChangedEventArgs e)
    {
        var filmeSelecionado = (Filme)e.CurrentSelection.FirstOrDefault();
        await Shell.Current.GoToAsync($"{nameof(FilmeDetailPage)}", true, new Dictionary<string, object>
        {
            {"Filme",  filmeSelecionado}
        });
    }
}