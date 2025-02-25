using AuvoMovies.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AuvoMovies.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private IFilmeService _service;
        private ISettings _settings;

        public LoginViewModel(IFilmeService service, ISettings settings)
        {
            _service = service;
            _settings = settings;
        }

        //[RelayCommand]
        //private async void Logar(string userName, string password)
        //{
        //    var resultToekn = await _service.PegarTokenASync();

        //    if (resultToekn.IsFailed)
        //        await Application.Current.MainPage.DisplayAlert("", resultToekn.Errors.FirstOrDefault().Message, "");

        //    var resultValidacao = await _service.ValidarToken(resultToekn.Value, userName, password);

        //    if (resultValidacao.IsFailed)
        //        await Application.Current.MainPage.DisplayAlert("", resultValidacao.Errors.FirstOrDefault().Message, "");

        //    var resultTokenSessao = await _service.CriarTokenSessaoAsync(resultToekn.Value);

        //    if (resultTokenSessao.IsFailed)
        //        await Application.Current.MainPage.DisplayAlert("", resultTokenSessao.Errors.FirstOrDefault().Message, "");

        //    //return resultTokenSessao;

        //    _settings.Token = $"Bearer {resultTokenSessao.Value}";

        //    Application.Current.MainPage = new AppShell();
        //}
    }
}
