using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface IAchievementsService
    {
        Task<List<Achievements>> GetAll();
        Task<Achievements> GetAchievements(int IdAchievements);
        Task<Achievements> CreateAchievements(string Achievement);
        Task<Achievements> UpdateAchievements(int IdAchievements, string? Achievement = null);
        Task<Achievements> DeleteAchievements(int IdAchievements);
    }
    public class AchievementsService : IAchievementsService
    {
        public readonly AchievementsRepositories _achievementsRepositories;
        public AchievementsService(AchievementsRepositories achievementsRepositories)
        {
            _achievementsRepositories = achievementsRepositories;
        }
        public async Task<Achievements> CreateAchievements(string Achievement)
        {
            return await _achievementsRepositories.CreateAchievements(Achievement);
            //throw new NotImplementedException();
        }

        public async Task<Achievements> DeleteAchievements(int IdAchievements)
        {
            // comprobar si existe
            Achievements achievementsToDelete = await _achievementsRepositories.GetAchievements(IdAchievements);
            if (achievementsToDelete == null)
            {
                throw new Exception($"El volteo con el Id {IdAchievements} no existe");
            }

            achievementsToDelete.IsDeleted = true;
            achievementsToDelete.Date = DateTime.Now;

            return await _achievementsRepositories.DeleteAchievements(achievementsToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<Achievements>> GetAll()
        {
            return await _achievementsRepositories.GetAll();
            //throw new NotImplementedException(); 
        }

        public async Task<Achievements> GetAchievements(int IdAchievements)
        {
            return await _achievementsRepositories.GetAchievements(IdAchievements);
            //throw new NotImplementedException();
        }

        public async Task<Achievements> UpdateAchievements(int IdAchievements, string? Achievement = null)
        {
            Achievements newachievements = await _achievementsRepositories.GetAchievements(IdAchievements);
            if (newachievements != null)
            {
                if (Achievement != null)
                {
                    newachievements.Achievement = Achievement;
                }
                return await _achievementsRepositories.UpdateAchievements(newachievements);
            }
            throw new NotImplementedException();
        }
    }
}
