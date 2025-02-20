using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuvoMovies.Models;
using SQLite;

namespace AuvoMovies.Infra.Interfaces
{
    public interface IRepository
    {
        void Inicializar();

        void Salvar(Filme user);

        List<Filme> BUscar();
        void Delete(Filme filme);
    }
}
