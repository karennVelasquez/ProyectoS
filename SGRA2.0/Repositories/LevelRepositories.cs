using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface ILevelRepositories
    {
        Task<List<Level>> GetAll();
        Task<Level> GetLevel(int id);
        Task<Level> CreateLevel(int Numlevel);
        Task<Level> UpdateLevel(Level level);
        Task<Level> DeleteLevel(Level level);
    }
    public class LevelRepositories : ILevelRepositories
    {
        private readonly PersonDBContext _db;
        public LevelRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<Level> CreateLevel(int NumLevel)
        {
            Level newLevel = new Level
            { NumLevel = NumLevel };
            _db.levels.AddAsync(newLevel);
            _db.SaveChanges();
            return newLevel;
        }
        public async Task<Level> DeleteLevel(Level level)
        {
            _db.levels.Attach(level); //Llamamos la actualizacion
            _db.Entry(level).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return level;
        }
        public async Task<List<Level>> GetAll()
        {
            return await _db.levels.ToListAsync();
        }
        public async Task<Level> GetLevel(int id)
        {
            return await _db.levels.FirstOrDefaultAsync(u => u.IdLevel == id);
        }
        public async Task<Level> UpdateLevel(Level level)
        {
            _db.levels.Attach(level); //Llamamos la actualizacion
            _db.Entry(level).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return level;
        }
    }
}
