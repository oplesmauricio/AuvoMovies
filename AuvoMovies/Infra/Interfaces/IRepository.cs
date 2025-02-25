using AuvoMovies.Models;

namespace AuvoMovies.Infra.Interfaces
{
    public interface IRepository
    {
        void Inicializar();

        void Salvar(Filme user);

        List<Filme> BUscar();
        Filme BUscarPOrId(int id);
        void Delete(Filme filme);
    }
}
