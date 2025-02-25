using AuvoMovies.ViewModels;

namespace AuvoMovies.Pages;

public partial class EnviarNotificacaoPage : ContentPage
{
    public EnviarNotificacaoPage(EnviarNotificacaoViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}