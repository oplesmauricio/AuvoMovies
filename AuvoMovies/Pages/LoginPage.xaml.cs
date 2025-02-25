using AuvoMovies.Pages.Base;
using AuvoMovies.Services.Interfaces;

namespace AuvoMovies.Pages;

public partial class LoginPage : BasePage
{
    private readonly IFilmeService _service;
    private readonly ISettings _settings;
    public LoginPage(IFilmeService service, ISettings settings)
    {
        InitializeComponent();
        _service = service;
        _settings = settings;
    }

    protected override void OnDisappearing()
    {
        MediaPlayer.Stop();
        base.OnDisappearing();
    }

    private async void LoginButtonClicked(object sender, EventArgs e)
    {
        try
        {
            actIndicator.IsRunning = true;
            actIndicator.IsVisible = true;

            if (!INternetConectada())
            {
                await DisplayAlert("Voce esta sem internet", "Reestabelexa a conexao e tente novamente", "Ok");
            }

#if DEBUG
            this.Usuario.Text = "mauriciodevelopermaui";
            this.Senha.Text = "1234";
#endif

            if (String.IsNullOrEmpty(this.Usuario.Text))
            {
                await DisplayAlert("Carissimo", "Vc precisa digitar um usuario meu jovem", "Ok");
                return;
            }

            if (String.IsNullOrEmpty(this.Senha.Text))
            {
                await DisplayAlert("Carissimo", "Vc precisa digitar uma senha meu jovem", "Ok");
                return;
            }

            var resultToekn = await _service.PegarTokenASync();
            if (resultToekn.IsFailed)
            {
                await DisplayAlert("Ops", resultToekn.Errors.FirstOrDefault().Message, "Ok");
                return;
            }

            var resultValidacao = await _service.ValidarToken(resultToekn.Value, this.Usuario.Text, this.Senha.Text);
            if (resultValidacao.IsFailed)
            {
                await DisplayAlert("Ops", resultValidacao.Errors.FirstOrDefault().Message, "Ok");
                return;
            }

            var resultTokenSessao = await _service.CriarTokenSessaoAsync(resultToekn.Value);
            if (resultTokenSessao.IsFailed)
            {
                await DisplayAlert("Ops", resultTokenSessao.Errors.FirstOrDefault().Message, "Ok");
                return;
            }

            _settings.Token = resultTokenSessao.Value;

            App.Current.MainPage = new AppShell();
        }
        catch (Exception ex)
        {
            //implementar aplication insignths azure
            await DisplayAlert("Ops", "Nao conseguimso logar", "Ok");
        }
        finally
        {
            actIndicator.IsRunning = false;
            actIndicator.IsVisible = false;
        }
    }

    private void PageUnloaded(object sender, EventArgs e)
    {
        MediaPlayer.Handler?.DisconnectHandler();
    }

    private async void ForgotPassword_Tapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Essa aqui entra com debito tecnico", "Esta no proximo card do backlog", "Ok");
    }
}