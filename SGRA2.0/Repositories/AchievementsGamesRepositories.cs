using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IAchievementsGamesRespositories
    {
        Task<List<AchievementsGames>> GetAll();
        Task<AchievementsGames> GetAchievementsGames(int id);
        Task<AchievementsGames> CreateAchievementsGames(int IdGames, int IdAchievements);
        Task<AchievementsGames> UpdateAchievementsGames(AchievementsGames achievementsGames);
        Task<AchievementsGames> DeleteAchievementsGames(AchievementsGames achievementsGames);
        public class AchievementsGamesRespositories : IAchievementsGamesRespositories
        {
            private readonly PersonDBContext _db;
            public AchievementsGamesRespositories(PersonDBContext db)
            {
                _db = db;
            }
            public async Task<AchievementsGames> CreateAchievementsGames(int IdGames, int IdAchievements)
            {
                AchievementsGames newAchievementsGames = new AchievementsGames
                {
                    IdGames = IdGames,
                    IdAchievements = IdAchievements
                };
                _db.achievementsGames.AddAsync(newAchievementsGames);
                return newAchievementsGames;
            }
            public async Task<AchievementsGames> DeleteAchievementsGames(AchievementsGames achievementsGames)
            {
                _db.achievementsGames.Attach(achievementsGames); //Llamamos la actualizacion
                _db.Entry(achievementsGames).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return achievementsGames;
            }
            public async Task<List<AchievementsGames>> GetAll()
            {
                return await _db.achievementsGames.ToListAsync();
            }
            public async Task<AchievementsGames> GetAchievementsGames(int id)
            {
                return await _db.achievementsGames.FirstOrDefaultAsync(u => u.IdAchievementsG == id);

            }
            public async Task<AchievementsGames> UpdateAchievementsGames(AchievementsGames achievementsGames)
            {
                _db.achievementsGames.Attach(achievementsGames); //Llamamos la actualizacion
                _db.Entry(achievementsGames).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return achievementsGames;
            }
        }
    }
}
