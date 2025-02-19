using AuvoMovies.Models;

namespace AuvoMovies.Pages;

public partial class FilmeDetailPage : ContentPage
{
    public FilmeDetailPage(dynamic movie)
    {
        InitializeComponent();

        PosterImage.Source = movie.PosterPath;
        TitleLabel.Text = movie.Title;
        OverviewLabel.Text = movie.Overview;
    }
}