using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IRecordTimeRepositories
    {
        Task<List<RecordTime>> GetAll();
        Task<RecordTime> GetRecordTime(int id);
        Task<RecordTime> CreateRecordTime(int IdLevel, DateTime Collecttime);
        Task<RecordTime> UpdateRecordTime(RecordTime recordTime);
        Task<RecordTime> DeleteRecordTime(RecordTime recordTime);
        public class RecordTimeRepositories : IRecordTimeRepositories   
        {
            private readonly PersonDBContext _db;
            public RecordTimeRepositories(PersonDBContext db)
            {
                _db = db;

            }
            public async Task<RecordTime> CreateRecordTime(int idLevel, DateTime Collecttime)
            {
                
                Level? level = _db.levels.FirstOrDefault(ut => ut.IdLevel == idLevel);
                RecordTime newTiempoRecord = new RecordTime
                {
                    IdLevel = idLevel,
                    Collecttime = Collecttime,
                    IsDelete = false,
                    Date = null
                };
                _db.recordTimes.AddAsync(newTiempoRecord);
                _db.SaveChanges();
                return newTiempoRecord;
            }
            public async Task<RecordTime> DeleteRecordTime(RecordTime recordTime)
            {
                _db.recordTimes.Attach(recordTime); //Llamamos la actualizacion
                _db.Entry(recordTime).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return recordTime;
            }
            public async Task<List<RecordTime>> GetAll()
            {
                return await _db.recordTimes.ToListAsync();
            }
            public async Task<RecordTime> GetRecordTime(int id)
            {
                return await _db.recordTimes.FirstOrDefaultAsync(u => u.IdRecordTime == id);

            }
            public async Task<RecordTime> UpdateRecordTime(RecordTime recordTime)
            {
                RecordTime RecordTimeUpdate = await _db.recordTimes.FindAsync(recordTime.IdRecordTime);
                if (RecordTimeUpdate != null)
                {
                    RecordTimeUpdate.IdLevel = recordTime.IdLevel;
                    RecordTimeUpdate.Collecttime = recordTime.Collecttime;
                    await _db.SaveChangesAsync();
                }

                return RecordTimeUpdate;
                /*_db.recordTimes.Attach(recordTime); //Llamamos la actualizacion
                _db.Entry(recordTime).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return recordTime;*/
            }
        }
    }
}
