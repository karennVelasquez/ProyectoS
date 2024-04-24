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
        Task<RecordTime> CreateRecordTime(int IdLevel, int IdWaste, DateTime Collecttime, int AmountCollected);
        Task<RecordTime> UpdateRecordTime(RecordTime recordTime);
        Task<RecordTime> DeleteRecordTime(RecordTime recordTime);
        public class RecordTimeRepositories
        {
            private readonly PersonDBContext _db;
            public RecordTimeRepositories(PersonDBContext db)
            {
                _db = db;

            }
            public async Task<RecordTime> CreateRecordTime(int IdLevel, int IdWaste, DateTime Collecttime, int AmountCollected)
            {
                //
                RecordTime newTiempoRecord = new RecordTime
                {
                    IdLevel = IdLevel,
                    IdWaste = IdWaste,
                    Collecttime = Collecttime,
                    AmountCollected = AmountCollected,
                    //
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
                //
                _db.recordTimes.Attach(recordTime); //Llamamos la actualizacion
                _db.Entry(recordTime).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return recordTime;
            }
        }
    }
}
