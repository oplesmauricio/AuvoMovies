using AuvoMovies.ViewModels;

namespace AuvoMovies.Pages;

public partial class FilmeDetailPage : ContentPage
{
    private readonly FilmesDetailViewModel vm;

    public FilmeDetailPage(FilmesDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = this.vm = vm;
    }

    protected async override void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            await this.vm.VerificarFavorito();
        }
        catch (Exception ex)
        {
            //TODO implementar telemetry azure aplciation insights
            await DisplayAlert("Tivemos um probleminha =D", "Nao estamos conseguindos acessar a lista de filmes", "Ok");
        }
    }
}