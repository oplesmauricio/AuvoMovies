using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using AuvoMovies.Infra.Interfaces;
using AuvoMovies.Models;
using AuvoMovies.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers.Commands;


namespace AuvoMovies.ViewModels
{
    [QueryProperty("Filme", "Filme")]
    public partial class FilmesDetailViewModel : ObservableObject
    {
        private IFilmeService _filmeService;
        private ISettings _settings;
        private IRepository _repository;

        [ObservableProperty]
        public Filme filme;

        [ObservableProperty]
        public bool isBusy;

        //public IRelayCommand FavoritarCommand { get; }

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
            //isRefreshing = false;
            if (result.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Adicionado", "Td certo!", "Ok");
                _repository.Delete(filme);
            }
        }
        //    async Task Favoritar()
        //    {
        //        IsBusy = true;

        //        IsBusy = false;
        //    }
    }
}
