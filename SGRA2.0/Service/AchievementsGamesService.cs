using SGRA2._0.Model;
using SGRA2._0.Repositories;
using static SGRA2._0.Repositories.IAchievementsGamesRespositories;

namespace SGRA2._0.Service
{
    public interface IAchievementsGamesService
    {
        Task<List<AchievementsGames>> GetAll();
        Task<AchievementsGames> GetAchievementsGames(int IdAchievementsG);
        Task<AchievementsGames> CreateAchievementsGames(int IdGames, int IdAchievements);
        Task<AchievementsGames> UpdateAchievementsGames(int IdAchievementsG, int? IdGames = null, int? IdAchievements = null);
        Task<AchievementsGames> DeleteAchievementsGames(int IdAchievementsG);
    }
    public class AchievementsGamesService : IAchievementsGamesService
    {
        public readonly AchievementsGamesRespositories _achievementsGamesRespositories;
        public AchievementsGamesService(AchievementsGamesRespositories achievementsGamesRespositories)
        {
            _achievementsGamesRespositories = achievementsGamesRespositories;
        }
        public async Task<AchievementsGames> CreateAchievementsGames(int IdGames, int IdAchievements)
        {
            return await _achievementsGamesRespositories.CreateAchievementsGames(IdGames, IdAchievements);
            //throw new NotImplementedException();
        }

        public async Task<AchievementsGames> DeleteAchievementsGames(int IdAchievementsG)
        {
            // comprobar si existe
            AchievementsGames achievementsGamesToDelete = await _achievementsGamesRespositories.GetAchievementsGames(IdAchievementsG);
            if (achievementsGamesToDelete == null)
            {
                throw new Exception($"El logro de la partida con el Id {IdAchievementsG} no existe");
            }

            return await _achievementsGamesRespositories.DeleteAchievementsGames(achievementsGamesToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<AchievementsGames>> GetAll()
        {
            return await _achievementsGamesRespositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<AchievementsGames> GetAchievementsGames(int IdAchievementsG)
        {
            return await _achievementsGamesRespositories.GetAchievementsGames(IdAchievementsG);
            //throw new NotImplementedException();
        }

        public async Task<AchievementsGames> UpdateAchievementsGames(int IdAchievementsG, int? IdGames = null, int? IdAchievements = null)
        {
            AchievementsGames newachievementsGames = await _achievementsGamesRespositories.GetAchievementsGames(IdAchievementsG);
            if (newachievementsGames != null)
            {
                if (IdGames != null)
                {
                    newachievementsGames.IdGames = (int)IdGames;
                }
                if (IdAchievements != null)
                {
                    newachievementsGames.IdAchievements = (int)IdAchievements;
                }
                return _achievementsGamesRespositories.UpdateAchievementsGames(newachievementsGames);
            }
            throw new NotImplementedException();
        }
    }
}
