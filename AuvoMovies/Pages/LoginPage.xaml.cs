using AuvoMovies.Services.Interfaces;
using AuvoMovies.ViewModels;

namespace AuvoMovies.Pages;

public partial class LoginPage : ContentPage
{
    private LoginViewModel vm;
    private IFilmeService _service;
    private ISettings _settings;
    public LoginPage(IFilmeService service, ISettings settings)
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        MediaPlayer.Stop();
        base.OnDisappearing();
    }

    private async void LoginButtonClicked(object sender, EventArgs e)
    {
        var resultToekn = await _service.PegarTokenASync();

        if (resultToekn.IsFailed)
            await Application.Current.MainPage.DisplayAlert("", resultToekn.Errors.FirstOrDefault().Message, "");

        var resultValidacao = await _service.ValidarToken(resultToekn.Value, this.Usuario.Text, this.Senha.Text);

        if (resultValidacao.IsFailed)
            await Application.Current.MainPage.DisplayAlert("", resultValidacao.Errors.FirstOrDefault().Message, "");

        var resultTokenSessao = await _service.CriarTokenSessaoAsync(resultToekn.Value);

        if (resultTokenSessao.IsFailed)
            await Application.Current.MainPage.DisplayAlert("", resultTokenSessao.Errors.FirstOrDefault().Message, "");

        //return resultTokenSessao;

        _settings.Token = $"Bearer {resultTokenSessao.Value}";

        Application.Current.MainPage = new AppShell();
    }
    private async void ForgotPassword_Tapped(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new ForgotPasswordPage());
    }

    private async void GoBack_Tapped(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void PageUnloaded(object sender, EventArgs e)
    {
        MediaPlayer.Handler?.DisconnectHandler();
    }
}