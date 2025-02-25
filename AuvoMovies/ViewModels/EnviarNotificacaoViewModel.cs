using AuvoMovies.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AuvoMovies.ViewModels
{
    public partial class EnviarNotificacaoViewModel : ObservableObject
    {
        private IFirebaseService _firebaseService;


        [ObservableProperty]
        public string tituloNotificacao;

        [ObservableProperty]
        public string descricaoNotificacao;

        public EnviarNotificacaoViewModel(IFirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        [RelayCommand]
        private async void EnviarNotificacao()
        {
            if (string.IsNullOrEmpty(tituloNotificacao))
            {
                await Application.Current.MainPage.DisplayAlert("Carissimo", "Escreva um titulo pra notificacao", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(descricaoNotificacao))
            {
                await Application.Current.MainPage.DisplayAlert("Carissimo", "Escreva uma descricao pra notificacao", "Ok");
                return;
            }

            await _firebaseService.EnviarNotificacao(tituloNotificacao, descricaoNotificacao);
        }

    }
}
