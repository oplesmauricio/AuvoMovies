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
    public partial class FilmesViewModel : ObservableObject
    {
        private IFilmeService _filmeService;
        private ISettings _settings;

        [ObservableProperty]
        public ObservableCollection<Filme> filmes = new();

        [ObservableProperty]
        public bool isBusy;

        // Propriedade do comando
        //public IRelayCommand RefreshCommand { get; }
        public AsyncCommand RefreshCommand { get; }

        public FilmesViewModel(IFilmeService filmeService, ISettings settings)
        {
            _filmeService = filmeService;
            _settings = settings;
        }

        public async Task GetFilmesAsync()
        {
            var result = await _filmeService.GetFilmesAsync();

            if (result.IsFailed)
                await Application.Current.MainPage.DisplayAlert("", result.Errors.FirstOrDefault().Message, "");

            foreach (var filme in result.Value)
            {
                filme.PosterPath = _settings.UrlImagePathTMDB + filme.PosterPath;
                filmes.Add(filme);
            }
        }

        public async Task Autenticar()
        {
            var result = await _filmeService.AutenticarAsync();

            if (result.IsFailed)
                await Application.Current.MainPage.DisplayAlert("", result.Errors.FirstOrDefault().Message, "");
        }

        //[RelayCommand]
        //private void Refresh()
        //{
        //    //await GetFilmesAsync();
        //    isRefreshing = false;
        //    testes = "depois dp refresh";
        //}
        async Task Refresh()
        {
            IsBusy = true;
            GetFilmesAsync();

            IsBusy = false;
        }
    }
}
