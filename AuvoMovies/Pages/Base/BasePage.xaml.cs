namespace AuvoMovies.Pages.Base;

public partial class BasePage : ContentPage
{
    public BasePage()
    {
        InitializeComponent();
    }

    public bool INternetConectada()
    {
        return Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}