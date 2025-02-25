using AuvoMovies.Infra.Interfaces;
using AuvoMovies.Models;
using AuvoMovies.Services.Interfaces;
using SQLite;

namespace AuvoMovies.Infra
{
    public class Repository : IRepository
    {
        private readonly string dbPath;
        private readonly ISettings _settings;

        public Repository(ISettings settings)
        {
            _settings = settings;
            dbPath = Path.Combine(FileSystem.AppDataDirectory, _settings.DatabaseFilename);
            Inicializar();
        }

        public void Inicializar()
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.CreateTable<Filme>();
            }
        }

        public void Salvar(Filme user)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.InsertOrReplace(user);
            }
        }

        public List<Filme> BUscar()
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                return db.Table<Filme>().ToList();
            }
        }

        public Filme BUscarPOrId(int id)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                return db.Table<Filme>().Where(m => m.Id == id).FirstOrDefault();
            }
        }

        public void Delete(Filme filme)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.Delete(filme);
            }
        }
    }

}
