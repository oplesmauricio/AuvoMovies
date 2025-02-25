using AuvoMovies.Models;
using AuvoMovies.ViewModels;

namespace AuvoMovies.Pages;

public partial class FavoritosPage : ContentPage
{
    private readonly FavoritosViewModel vm;
    public FavoritosPage(FavoritosViewModel vm)
    {
        InitializeComponent();
        BindingContext = this.vm = vm;
    }

    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await this.vm.BuscarFavoritosAsync();
        }
        catch (Exception)
        {
            //TODO implementar telemetry azure aplciation insights
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
                {"JaEstaFavoritado", true }
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Tivemos um probleminha =D", "Nao estamos conseguindos acessar a lista de filmes", "Ok");
        }
    }
}