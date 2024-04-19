using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface ITimeRepositories
    {
        Task<List<Time>> GetAll();
        Task<Time> GetTime(int id);
        Task<Time> CreateTime(int IdWaste, int Processduration, int IdProcessStage);
        Task<Time> UpdateTime(Time time);
        Task<Time> DeleteTime(Time time);
    }
    public class TimeRepositories : ITimeRepositories
    {
        private readonly PersonDBContext _db;
        public TimeRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<Time> CreateTime(int IdWaste, int Processduration, int IdProcessStage)
        {
            Time newTime = new Time
            {
                IdWaste = IdWaste,
                Processduration = Processduration,
                IdProcessStage = IdProcessStage
            };
            _db.times.AddAsync(newTime);
            _db.SaveChanges();
            return newTime;
        }
        public async Task<Time> DeleteTime(Time time)
        {
            _db.times.Attach(time); //Llamamos la actualizacion
            _db.Entry(time).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return time;
        }
        public async Task<List<Time>> GetAll()
        {
            return await _db.times.ToListAsync();
        }
        public async Task<Time> GetTime(int id)
        {
            return await _db.times.FirstOrDefaultAsync(u => u.IdTime == id);

        }
        public async Task<Time> UpdateTime(Time time)
        {
            _db.times.Attach(time); //Llamamos la actualizacion
            _db.Entry(time).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return time;
        }
    }
}
