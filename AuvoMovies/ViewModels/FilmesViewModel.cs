﻿using System.Collections.ObjectModel;
using AuvoMovies.Infra.Interfaces;
using AuvoMovies.Models;
using AuvoMovies.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentResults;


namespace AuvoMovies.ViewModels
{
    public partial class FilmesViewModel : ObservableObject
    {
        private readonly IFilmeService _filmeService;
        private readonly ISettings _settings;
        private readonly IRepository _repository;

        [ObservableProperty]
        public ObservableCollection<Filme> filmes = new();

        [ObservableProperty]
        public bool isRefreshing;

        public FilmesViewModel(IFilmeService filmeService, ISettings settings, IRepository repository)
        {
            _filmeService = filmeService;
            _settings = settings;
            _repository = repository;
        }

        public async Task GetFilmesAsync()
        {
            var result = await _filmeService.GetFilmesAsync();

            if (result.IsFailed)
                await Application.Current.MainPage.DisplayAlert("", result.Errors.FirstOrDefault().Message, "Ok");

            filmes.Clear();

            foreach (var filme in result.Value)
            {
                filme.PosterPath = _settings.UrlImagePathTMDB + filme.PosterPath;
                filmes.Add(filme);
            }
        }

        [RelayCommand]
        private async void Refresh()
        {
            await GetFilmesAsync();
            isRefreshing = false;
            OnPropertyChanged(nameof(IsRefreshing));
        }

        public async Task SincronizarSQLiteApi()
        {
            var filmesLocais = _repository.BUscar();

            if (filmesLocais.Count == 0)
                return;

            var result = await Task.Run(() => Sincronizar(filmesLocais));

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (result.IsSuccess)
                    Application.Current.MainPage.DisplayAlert("Filmes sincronizados", "Os filmes que vc favoritou offline agora estao sincronizados", "Ok");
            });
        }

        private async Task<Result> Sincronizar(List<Filme> filmesLocais)
        {
            foreach (var filme in filmesLocais)
                if ((await _filmeService.Favoritar(filme.Id)).IsSuccess)
                    _repository.Delete(filme);

            return Result.Ok();
        }
    }
}
