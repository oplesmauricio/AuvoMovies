using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuvoMovies.Models;
using FluentResults;

namespace AuvoMovies.Services.Interfaces
{
    public interface IFilmeService
    {
        Task<Result<IEnumerable<Filme>>> GetFilmesAsync();
        Task<Result> Favoritar(int idFilme);
        Task<Result<IEnumerable<Filme>>> BuscarFavoritosAsync();

        Task<Result<string>> PegarTokenASync();
        Task<Result<string>> CriarTokenSessaoAsync(string requestToken);

        Task<Result<string>> ValidarToken(string requestToken, string username, string password);

        //Task<Result> Logar(string username, string password);
    }
}
