using AuvoMovies.Infra.Interfaces;
using AuvoMovies.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace AuvoMovies.Infra
{
    public class Repository : IRepository
    {
        private readonly string dbPath;
        private SQLiteAsyncConnection _dbConnection;

        public Repository()
        {
            dbPath = Path.Combine(FileSystem.AppDataDirectory, "app_database.db3");
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
                db.Insert(user);
            }
        }

        public List<Filme> BUscar()
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                return db.Table<Filme>().ToList();
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
