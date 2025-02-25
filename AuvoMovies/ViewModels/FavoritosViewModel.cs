using System.Collections.ObjectModel;
using AuvoMovies.Infra.Interfaces;
using AuvoMovies.Models;
using AuvoMovies.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace AuvoMovies.ViewModels
{
    public partial class FavoritosViewModel : ObservableObject
    {
        private readonly IFilmeService _filmeService;
        private readonly ISettings _settings;
        private readonly IRepository _repository;

        [ObservableProperty]
        public ObservableCollection<Filme> filmes = new();

        [ObservableProperty]
        public bool isRefreshing;

        public FavoritosViewModel(IFilmeService filmeService, ISettings settings, IRepository repository)
        {
            _filmeService = filmeService;
            _settings = settings;
            _repository = repository;
        }

        public async Task BuscarFavoritosAsync()
        {
            filmes.Clear();

            var filmesLOcais = _repository.BUscar();
            AtribuirAListaDaTela(filmesLOcais);

            var result = await _filmeService.BuscarFavoritosAsync();

            if (result.IsFailed)
                await Application.Current.MainPage.DisplayAlert("", result.Errors.FirstOrDefault().Message, "Ok");

            AtribuirAListaDaTela(result.Value);
        }

        private void AtribuirAListaDaTela(IEnumerable<Filme> favoritos)
        {
            foreach (var filmeFavorito in favoritos)
            {
                if (!filmes.Any(m => m.Id == filmeFavorito.Id))
                {
                    filmeFavorito.PosterPath = _settings.UrlImagePathTMDB + filmeFavorito.PosterPath;
                    filmes.Add(filmeFavorito);
                }
            }
        }

        [RelayCommand]
        private async void Refresh()
        {
            await BuscarFavoritosAsync();
            isRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));
        }
    }
}
