using SGRA2._0.Model;
using SGRA2._0.Repositories;
using System.Threading;

namespace SGRA2._0.Service
{
    public interface ITimeService
    {
        Task<List<Time>> GetAll();
        Task<Time> GetTime(int IdTime);
        Task<Time> CreateTime(int IdWaste, int Processduration, int IdProcessStage);
        Task<Time> UpdateTime(int IdTime, int? IdWaste = null, int? Processduration = null, int? IdProcessStage = null);
        Task<Time> DeleteTime(int IdTime);
    }
    public class TimeService : ITimeService
    {
        public readonly ITimeRepositories _timeRepositories;
        public TimeService(ITimeRepositories timeRepositories)
        {
            _timeRepositories = timeRepositories;
        }
        public async Task<Time> CreateTime(int IdWaste, int Processduration, int IdProcessStage)
        {
            return await _timeRepositories.CreateTime(IdWaste, Processduration, IdProcessStage);
            //throw new NotImplementedException();
        }

        public async Task<Time> DeleteTime(int IdTime)
        {
            // comprobar si existe
            Time timeToDelete = await _timeRepositories.GetTime(IdTime);
            if (timeToDelete == null)
            {
                throw new Exception($"El tiempo con el Id {IdTime} no existe");
            }
            timeToDelete.IsDelete = true;
            timeToDelete.Date = DateTime.Now;

            return await _timeRepositories.DeleteTime(timeToDelete);
            //throw new NotImplementedException();
        }
        public async Task<List<Time>> GetAll()
        {
            return await _timeRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Time> GetTime(int IdTime)
        {
            return await _timeRepositories.GetTime(IdTime);
            //throw new NotImplementedException();
        }

        public async Task<Time> UpdateTime(int IdTime, int? IdWaste = null, int? Processduration = null, int? IdProcessStage = null)
        {
            Time newtime = await _timeRepositories.GetTime(IdTime);
            if (newtime != null)
            {
                if (IdWaste != null)
                {
                    newtime.IdWaste = (int)IdWaste;
                }
                if (Processduration != null)
                {
                    newtime.Processduration = (int)Processduration;
                }
                if (IdProcessStage != null)
                {
                    newtime.IdProcessStage = (int)IdProcessStage;
                }
                return await _timeRepositories.UpdateTime(newtime);
            }
            throw new NotImplementedException();
        }
    }
}
