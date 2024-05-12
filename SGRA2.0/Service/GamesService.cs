using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface IGamesService
    {
        Task<List<Games>> GetAll();
        Task<Games> GetGames(int IdGames);
        Task<Games> CreateGames(int IdUser);
        Task<Games> UpdateGames(int IdGames, int? IdLevel = null);
        Task<Games> DeleteGames(int IdGames);
    }
    public class GamesService : IGamesService
    {
        public readonly IGamesRepositories _gamesRepositories;
        public GamesService(IGamesRepositories gamesRepositories)
        {
            _gamesRepositories = gamesRepositories;
        }
        public async Task<Games> CreateGames(int IdLevel)
        {
            return await _gamesRepositories.CreateGames( IdLevel);
            //throw new NotImplementedException();
        }

        public async Task<Games> DeleteGames(int IdGames)
        {
            // comprobar si existe
            Games gamesToDelete = await _gamesRepositories.GetGames(IdGames);
            if (gamesToDelete == null)
            {
                throw new Exception($"La partida con el Id {IdGames} no existe");
            }
            gamesToDelete.IsDelete = true;
            gamesToDelete.Date = DateTime.Now;

            return await _gamesRepositories.DeleteGames(gamesToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<Games>> GetAll()
        {
            return await _gamesRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Games> GetGames(int IdGames)
        {
            return await _gamesRepositories.GetGames(IdGames);
            //throw new NotImplementedException();
        }

        public async Task<Games> UpdateGames(int IdGames, int? IdLevel = null)
        {
            Games newgames = await _gamesRepositories.GetGames(IdGames);
            if (newgames != null)
            {
                
                if (IdLevel != null)
                {
                    newgames.IdLevel = (int)IdLevel;
                }
                return await _gamesRepositories.UpdateGames(newgames);
            }
            throw new NotImplementedException();
        }
    }
}