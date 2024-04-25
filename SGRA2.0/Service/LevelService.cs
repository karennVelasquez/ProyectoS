using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface ILevelService
    {
        Task<List<Level>> GetAll();
        Task<Level> GetLevel(int IdLevel);
        Task<Level> CreateLevel(int NumLevel);
        Task<Level> UpdateLevel(int IdLevel, int? NumLevel = null);
        Task<Level> DeleteLevel(int IdLevel);
    }
    public class LevelService : ILevelService
    {
        public readonly LevelRepositories _levelRepositories;
        public LevelService(LevelRepositories levelRepositories)
        {
            _levelRepositories = levelRepositories;
        }
        public async Task<Level> CreateLevel(int NumLevel)
        {
            return await _levelRepositories.CreateLevel(NumLevel);
            //throw new NotImplementedException();
        }

        public async Task<Level> DeleteLevel(int IdLevel)
        {
            // comprobar si existe
            Level levelToDelete = await _levelRepositories.GetLevel(IdLevel);
            if (levelToDelete == null)
            {
                throw new Exception($"El nivel con el Id {IdLevel} no existe");
            }
            levelToDelete.IsDelete = true;
            levelToDelete.Date = DateTime.Now;

            return await _levelRepositories.DeleteLevel(levelToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<Level>> GetAll()
        {
            return await _levelRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Level> GetLevel(int IdLevel)
        {
            return await _levelRepositories.GetLevel(IdLevel);
            //throw new NotImplementedException();
        }

        public async Task<Level> UpdateLevel(int IdLevel, int? NumLevel = null)
        {
            Level newlevel = await _levelRepositories.GetLevel(IdLevel);
            if (newlevel != null)
            {
                if (NumLevel != null)
                {
                    newlevel.NumLevel = (int)NumLevel;
                }
                return await _levelRepositories.UpdateLevel(newlevel);
            }
            throw new NotImplementedException();
        }
    }
}
