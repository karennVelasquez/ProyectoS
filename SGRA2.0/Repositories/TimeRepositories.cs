using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface ITimeRepositories
    {
        Task<List<Time>> GetAll();
        Task<Time> GetTime(int IdTime);
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
        public async Task<Time> CreateTime(int idWaste, int Processduration, int idProcessStage)
        {
            Waste? waste = _db.wastes.FirstOrDefault(ut =>ut.IdWaste == idWaste);
            ProcessStage? processStage = _db.processStages.FirstOrDefault(ut => ut.IdProcessStage == idProcessStage);
            Time newTime = new Time
            {
                IdWaste = idWaste,
                Processduration = Processduration,
                IdProcessStage = idProcessStage,
                IsDelete = false,   
                Date = null
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
            Time TimeUpdate = await _db.times.FindAsync(time.IdTime);
            if (TimeUpdate == null) 
            {
                TimeUpdate.IdWaste = time.IdWaste;
                TimeUpdate.Processduration = time.Processduration;
                TimeUpdate.IdProcessStage = time.IdProcessStage;

                await _db.SaveChangesAsync();
            }

            return TimeUpdate;
            /*
            _db.historicalCost.Attach(historicalCost); //Llamamos la actualizacion
            _db.Entry(historicalCost).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return historicalCost;
            */
        }
    }
}
