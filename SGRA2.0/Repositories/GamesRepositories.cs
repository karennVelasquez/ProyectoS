using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IGamesRepositories
    {
        Task<List<Games>> GetAll();
        Task<Games> GetGames(int id);
        Task<Games> CreateGames( int IdLevel);
        Task<Games> UpdateGames(Games games);
        Task<Games> DeleteGames(Games games);
    }
    public class GamesRepositories : IGamesRepositories
    {
        private readonly PersonDBContext _db;
        public GamesRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<Games> CreateGames( int idLevel)
        {
            Level? level = _db.levels.FirstOrDefault(ut => ut.IdLevel == idLevel);
            Games newPartidas = new Games
            {
                
                IdLevel = idLevel,
                IsDelete = false,
                Date = null
            };
            _db.games.AddAsync(newPartidas);
            _db.SaveChanges();
            return newPartidas;
        }
        public async Task<Games> DeleteGames(Games games)
        {
            _db.games.Attach(games); //Llamamos la actualizacion
            _db.Entry(games).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return games;
        }
        public async Task<List<Games>> GetAll()
        {
            return await _db.games.ToListAsync();
        }
        public async Task<Games> GetGames(int id)
        {
            return await _db.games.FirstOrDefaultAsync(u => u.IdGames == id);
        }
        public async Task<Games> UpdateGames(Games games)
        {
            Games GamesUpdate = await _db.games.FindAsync(games.IdGames);
            if (GamesUpdate != null)
            {
                
                GamesUpdate.IdLevel = games.IdLevel;
                await _db.SaveChangesAsync();
            }

            return GamesUpdate;
            /*
            _db.games.Attach(games); //Llamamos la actualizacion
            _db.Entry(games).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return games;*/
        }
    }
}
