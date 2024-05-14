using SGRA2._0.Model;
using SGRA2._0.Repositories;
using static SGRA2._0.Repositories.IRecordTimeRepositories;

namespace SGRA2._0.Service
{
    public interface IRecordTimeService
    {
        Task<List<RecordTime>> GetAll();
        Task<RecordTime> GetRecordTime(int IdRecordTime);
        Task<RecordTime> CreateRecordTime(int IdLevel, DateTime Collecttime);
        Task<RecordTime> UpdateRecordTime(int IdRecordTime, int? IdLevel = null,  DateTime? Collecttime = null);
        Task<RecordTime> DeleteRecordTime(int IdRecordTime);
    }
    public class RecordTimeService : IRecordTimeService
    {
        public readonly IRecordTimeRepositories _recordTimeRepositories;
        public RecordTimeService(IRecordTimeRepositories recordTimeRepositories)
        {
            _recordTimeRepositories = recordTimeRepositories;
        }

        public async Task<RecordTime> CreateRecordTime(int IdLevel,  DateTime Collecttime)
        {
            return await _recordTimeRepositories.CreateRecordTime(IdLevel, Collecttime);
            //throw new NotImplementedException();
        }

        public async Task<RecordTime> DeleteRecordTime(int IdRecordTime)
        {
            // comprobar si existe
            RecordTime recordTimeToDelete = await _recordTimeRepositories.GetRecordTime(IdRecordTime);
            if (recordTimeToDelete == null)
            {
                throw new Exception($"El tiempo record con el Id {IdRecordTime} no existe");
            }
            recordTimeToDelete.IsDelete = true;
            recordTimeToDelete.Date = DateTime.Now;

            return await _recordTimeRepositories.DeleteRecordTime(recordTimeToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<RecordTime>> GetAll()
        {
            return await _recordTimeRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<RecordTime> GetRecordTime(int IdRecordTime)
        {
            return await _recordTimeRepositories.GetRecordTime(IdRecordTime);
            //throw new NotImplementedException();
        }

        public async Task<RecordTime> UpdateRecordTime(int IdRecordTime, int? IdLevel = null, DateTime? Collecttime = null)
        {
            RecordTime newrecordTime = await _recordTimeRepositories.GetRecordTime(IdRecordTime);
            if (newrecordTime != null)
            {
                if (IdLevel != null)
                {
                    newrecordTime.IdLevel = (int)IdLevel;
                }
                if (Collecttime != null)
                {
                    newrecordTime.Collecttime = (DateTime)Collecttime;
                }
                return await _recordTimeRepositories.UpdateRecordTime(newrecordTime);
            }
            throw new NotImplementedException();
        }
    }
}
