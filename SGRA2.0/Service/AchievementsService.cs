using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface IAchievementsService
    {
        Task<List<Achievements>> GetAll();
        Task<Achievements> GetAchievements(int IdAchievements);
        Task<Achievements> CreateAchievements(int IdUser, int IdGames);
        Task<Achievements> UpdateAchievements(int IdAchievements, int? IdUser = null, int? IdGames = null) ;
        Task<Achievements> DeleteAchievements(int IdAchievements);
    }
    public class AchievementsService : IAchievementsService
    {
        public readonly IAchievementsRepositories _achievementsRepositories;
        public AchievementsService(IAchievementsRepositories achievementsRepositories)
        {
            _achievementsRepositories = achievementsRepositories;
        }
        public async Task<Achievements> CreateAchievements(int IdUser, int IdGames)
        {
            return await _achievementsRepositories.CreateAchievements(IdUser, IdGames);
            //throw new NotImplementedException();
        }

        public async Task<Achievements> DeleteAchievements(int IdAchievements)
        {
            // comprobar si existe
            Achievements achievementsToDelete = await _achievementsRepositories.GetAchievements(IdAchievements);
            if (achievementsToDelete == null)
            {
                throw new Exception($"Los logros con el Id {IdAchievements} no existe");
            }

            achievementsToDelete.IsDelete = true;
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

        public async Task<Achievements> UpdateAchievements(int IdAchievements, int? IdUser = null, int? IdGames = null)
        {
            Achievements newachievements = await _achievementsRepositories.GetAchievements(IdAchievements);
            if (newachievements != null)
            {
                if (IdUser != null)
                {
                    newachievements.IdUser = (int) IdUser;
                }
                if (IdGames != null) 
                {
                    newachievements.IdGames = (int)IdGames;
                }
                return await _achievementsRepositories.UpdateAchievements(newachievements);
            }
            throw new NotImplementedException();
        }
    }
}
