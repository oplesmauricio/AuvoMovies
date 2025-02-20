using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using AuvoMovies.Models;
using AuvoMovies.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers.Commands;
using FluentResults;
using AuvoMovies.Infra.Interfaces;


namespace AuvoMovies.ViewModels
{
    public partial class FilmesViewModel : ObservableObject
    {
        private IFilmeService _filmeService;
        private ISettings _settings;
        private IRepository _repository;

        [ObservableProperty]
        public ObservableCollection<Filme> filmes = new();

        [ObservableProperty]
        public bool isBusy;

        // Propriedade do comando
        //public IRelayCommand RefreshCommand { get; }
        public AsyncCommand RefreshCommand { get; }

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
                await Application.Current.MainPage.DisplayAlert("", result.Errors.FirstOrDefault().Message, "");

            filmes.Clear();

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
            foreach ( var filme in filmesLocais)
                if((await _filmeService.Favoritar(filme.Id)).IsSuccess)
                    _repository.Delete(filme);

            return Result.Ok();
        }
    }
}
