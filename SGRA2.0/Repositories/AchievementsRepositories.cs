using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IAchievementsRepositories
    {
        Task<List<Achievements>> GetAll();
        Task<Achievements> GetAchievements(int id);
        Task<Achievements> CreateAchievements(int IdUser, int IdGames);
        Task<Achievements> UpdateAchievements(Achievements achievements);
        Task<Achievements> DeleteAchievements(Achievements achievements);
    }

    public class AchievementsRepositories : IAchievementsRepositories
    {
        private readonly PersonDBContext _db;
        public AchievementsRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<Achievements> CreateAchievements(int idUser, int idGames)
        {
            User? user = _db.users.FirstOrDefault(ut => ut.IdUser == idUser);
            Games? games = _db.games.FirstOrDefault(ut => ut.IdGames == idGames);
            Achievements newAchievements = new Achievements
            { 
                IdUser = idUser,
                IdGames = idGames,
                IsDelete = false,
                Date = null
                //
            };
            _db.achievements.AddAsync(newAchievements);
            _db.SaveChanges();
            return newAchievements;
        }
        public async Task<Achievements> DeleteAchievements(Achievements achievements)
        {
            _db.achievements.Attach(achievements); //Llamamos la actualizacion
            _db.Entry(achievements).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return achievements;
        }
        public async Task<List<Achievements>> GetAll()
        {
            return await _db.achievements.ToListAsync();
        }
        public async Task<Achievements> GetAchievements(int id)
        {
            return await _db.achievements.FirstOrDefaultAsync(u => u.IdAchievements == id);
        }
        public async Task<Achievements> UpdateAchievements(Achievements achievements)
        {
            Achievements AchievementsUpdate = await _db.achievements.FindAsync(achievements.IdAchievements);
            if (AchievementsUpdate != null)
            {
                AchievementsUpdate.IdUser = achievements.IdUser;
                AchievementsUpdate.IdGames = achievements.IdGames;

                await _db.SaveChangesAsync();
            }
            //_db.achievements.Attach(achievements); //Llamamos la actualizacion
            //_db.Entry(achievements).State = EntityState.Modified;
            //await _db.SaveChangesAsync();
            return achievements;
        }
    }
}
