using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;

namespace SGRA2._0.Repositories
{
    public interface IAchievementsGamesRespositories
    {
        Task<List<AchievementsGames>> GetAll();
        Task<AchievementsGames> GetAchievementsGames(int IdAchievementsG);
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
            public async Task<AchievementsGames> CreateAchievementsGames(int idGames, int idAchievements)
            {
                Games? games = _db.games.FirstOrDefault(ut => ut.IdGames == idGames);
                Achievements? achievements = _db.achievements.FirstOrDefault(ut => ut.IdAchievements == idAchievements);
                AchievementsGames newAchievementsGames = new AchievementsGames
                {
                    IdGames = idGames,
                    IdAchievements = idAchievements,
                    IsDelete = false,
                    Date = null
                };
                _db.achievementsGames.AddAsync(newAchievementsGames);
                _db.SaveChanges();
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
                AchievementsGames AchievementsGamesUpdate = await _db.achievementsGames.FindAsync(achievementsGames.IdAchievementsG);
                if (AchievementsGamesUpdate != null)
                {
                    AchievementsGamesUpdate.IdGames = achievementsGames.IdGames;
                    AchievementsGamesUpdate.IdAchievements = achievementsGames.IdAchievements;

                    await _db.SaveChangesAsync();
                }
                return AchievementsGamesUpdate;
                //_db.achievementsGames.Attach(achievementsGames); //Llamamos la actualizacion
                //_db.Entry(achievementsGames).State = EntityState.Modified;
                //await _db.SaveChangesAsync();
                //return achievementsGames;
            }
        }
    }
}
