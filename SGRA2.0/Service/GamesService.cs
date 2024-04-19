using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface IGamesService
    {
        Task<List<Games>> GetAll();
        Task<Games> GetGames(int IdGames);
        Task<Games> CreateGames(int IdUser, int IdLevel, DateTime StartDate, DateTime FinalDate);
        Task<Games> UpdateGames(int IdGames, int? IdUser = null, int? IdLevel = null, DateTime? StartDate = null, DateTime? FinalDate = null);
        Task<Games> DeleteGames(int IdGames);
    }
    public class GamesService : IGamesService
    {
        public readonly GamesRepositories _gamesRepositories;
        public GamesService(GamesRepositories gamesRepositories)
        {
            _gamesRepositories = gamesRepositories;
        }
        public async Task<Games> CreateGames(int IdUser, int IdLevel, DateTime StartDate, DateTime FinalDate)
        {
            return await _gamesRepositories.CreateGames(IdUser, IdLevel, StartDate, FinalDate);
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

        public async Task<Games> UpdateGames(int IdPersona, int? IdUser = null, int? IdLevel = null, DateTime? StartDate = null, DateTime? FinalDate = null)
        {
            Games newgames = await _gamesRepositories.GetGames(IdPersona);
            if (newgames != null)
            {
                if (IdUser != null)
                {
                    newgames.IdUser = (int)IdUser;
                }
                if (IdLevel != null)
                {
                    newgames.IdLevel = (int)IdLevel;
                }
                if (StartDate != null)
                {
                    newgames.StartDate = (DateTime)StartDate;
                }
                if (FinalDate != null)
                {
                    newgames.FinalDate = (DateTime)FinalDate;
                }
                return await _gamesRepositories.UpdateGames(newgames);
            }
            throw new NotImplementedException();
        }
    }
}
