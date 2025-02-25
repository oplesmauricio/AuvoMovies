﻿using AuvoMovies.Infra.Interfaces;
using AuvoMovies.Models;
using AuvoMovies.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace AuvoMovies.ViewModels
{
    [QueryProperty("Filme", "Filme")]
    [QueryProperty("JaEstaFavoritado", "JaEstaFavoritado")]
    public partial class FilmesDetailViewModel : ObservableObject
    {
        private readonly IFilmeService _filmeService;
        private readonly ISettings _settings;
        private readonly IRepository _repository;

        [ObservableProperty]
        public Filme filme;

        [ObservableProperty]
        public bool jaEstaFavoritado;

        public FilmesDetailViewModel(IFilmeService filmeService, ISettings settings, IRepository repository)
        {
            _filmeService = filmeService;
            _settings = settings;
            _repository = repository;
        }

        [RelayCommand]
        private async void Favoritar()
        {
            _repository.Salvar(filme);

            var result = await _filmeService.Favoritar(filme.Id);
            if (result.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Adicionado", "Td certo!", "Ok");
                _repository.Delete(filme);
                await Shell.Current.Navigation.PopAsync();
            }
            else
                await Application.Current.MainPage.DisplayAlert("Ops", "Tente novamente daqui a pouco", "Ok");
        }

        [RelayCommand]
        private async void Compartilhar()
        {
            await Launcher.Default.OpenAsync($"whatsapp://send?phone=+{556294057385}&text={"Esse filme parece ser bom: " + filme.Title + "\n" + filme.Overview}");
        }

        public async Task VerificarFavorito()
        {
            //acredito que aqui seria melhor uma api apenas para verificar se ele eh ou nao favorito, mas esse endpoint nao existe no TMDB
            var filmes = await _filmeService.BuscarFavoritosAsync();

            if (filmes.IsSuccess)
                if (filmes.Value.Any(m => m.Id == Filme.Id))
                    this.jaEstaFavoritado = true;

            OnPropertyChanged(nameof(JaEstaFavoritado));
        }
    }
}
