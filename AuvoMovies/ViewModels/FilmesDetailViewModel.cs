using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
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

        [ObservableProperty]
        public Filme filme;

        [ObservableProperty]
        public bool isBusy;

        //public IRelayCommand FavoritarCommand { get; }

        public FilmesDetailViewModel(IFilmeService filmeService, ISettings settings)
        {
            _filmeService = filmeService;
            _settings = settings;
        }

        [RelayCommand]
        private void Favoritar()
        {
            //await GetFilmesAsync();
            //isRefreshing = false;
            //testes = "depois dp refresh";
        }
        //    async Task Favoritar()
        //    {
        //        IsBusy = true;

        //        IsBusy = false;
        //    }
    }
}
