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
        Task<Result> AutenticarAsync();
    }
}
