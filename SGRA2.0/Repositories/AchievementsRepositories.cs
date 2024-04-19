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
        Task<Achievements> CreateAchievements(string Achievement);
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
        public async Task<Achievements> CreateAchievements(string Achievement)
        {
            Achievements newAchievements = new Achievements
            { Achievement = Achievement };
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
            _db.achievements.Attach(achievements); //Llamamos la actualizacion
            _db.Entry(achievements).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return achievements;
        }
    }
}
