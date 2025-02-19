using AuvoMovies.Models;
using AuvoMovies.ViewModels;

namespace AuvoMovies.Pages;

public partial class FilmeDetailPage : ContentPage
{
    public FilmeDetailPage(FilmesDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}