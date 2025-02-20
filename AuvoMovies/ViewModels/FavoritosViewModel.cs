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
    public partial class FavoritosViewModel : ObservableObject
    {
        private IFilmeService _filmeService;
        private ISettings _settings;
        private IRepository _repository;

        [ObservableProperty]
        public ObservableCollection<Filme> filmes = new();

        [ObservableProperty]
        public bool isBusy;

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
                await Application.Current.MainPage.DisplayAlert("", result.Errors.FirstOrDefault().Message, "");

            AtribuirAListaDaTela(result.Value);
        }

        private void AtribuirAListaDaTela(IEnumerable<Filme> favoritos)
        {
            foreach (var filmeFavorito in favoritos)
            {
                if(!filmes.Any(m => m.Id == filmeFavorito.Id))
                {
                    filmeFavorito.PosterPath = _settings.UrlImagePathTMDB + filmeFavorito.PosterPath;
                    filmes.Add(filmeFavorito);
                }
            }
        }
    }
}
