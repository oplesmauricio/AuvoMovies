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
    public partial class FavoritosViewModel : ObservableObject
    {
        private IFilmeService _filmeService;
        private ISettings _settings;

        [ObservableProperty]
        public ObservableCollection<Filme> filmes = new();

        [ObservableProperty]
        public bool isBusy;

        public FavoritosViewModel(IFilmeService filmeService, ISettings settings)
        {
            _filmeService = filmeService;
            _settings = settings;
        }

        public async Task BuscarFavoritosAsync()
        {
            var result = await _filmeService.BuscarFavoritosAsync();

            if (result.IsFailed)
                await Application.Current.MainPage.DisplayAlert("", result.Errors.FirstOrDefault().Message, "");

            filmes.Clear();

            foreach (var filme in result.Value)
            {
                filme.PosterPath = _settings.UrlImagePathTMDB + filme.PosterPath;
                filmes.Add(filme);
            }
        }
    }
}
