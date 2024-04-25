using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface IScoreService
    {
        Task<List<Score>> GetAll();
        Task<Score> GetScore(int IdScore);
        Task<Score> CreateScore(int IdUser, int IdGames, int NumScore);
        Task<Score> UpdateScore(int IdScore, int? IdUser = null, int? IdGames = null, int? NumScore = null);
        Task<Score> DeleteScore(int IdScore);
    }
    public class ScoreService : IScoreService
    {
        public readonly IScoreRepositories _scoreRepositories;
        public ScoreService(IScoreRepositories scoreRepositories)
        {
            _scoreRepositories = scoreRepositories;
        }
        public async Task<Score> CreateScore(int IdUser, int IdGames, int NumScore)
        {
            return await _scoreRepositories.CreateScore(IdUser, IdGames, NumScore);
            //throw new NotImplementedException();
        }

        public async Task<Score> DeleteScore(int IdScore)
        {
            // comprobar si existe
            Score scoreToDelete = await _scoreRepositories.GetScore(IdScore);
            if (scoreToDelete == null)
            {
                throw new Exception($"El puntaje con el Id {IdScore} no existe");
            }
            scoreToDelete.IsDelete = true;
            scoreToDelete.Date = DateTime.Now;

            return await _scoreRepositories.DeleteScore(scoreToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<Score>> GetAll()
        {
            return await _scoreRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Score> GetScore(int IdScore)
        {
            return await _scoreRepositories.GetScore(IdScore);
            //throw new NotImplementedException();
        }

        public async Task<Score> UpdateScore(int IdScore, int? IdUser = null, int? IdGames = null, int? NumScore = null)
        {
            Score newscore = await _scoreRepositories.GetScore(IdScore);
            if (newscore != null)
            {
                if (IdUser != null)
                {
                    newscore.IdUser = (int)IdUser;
                }
                if (IdGames != null)
                {
                    newscore.IdGames = (int)IdGames;
                }
                if (NumScore != null)
                {
                    newscore.NumScore = (int)NumScore;
                }
                return await _scoreRepositories.UpdateScore(newscore);
            }
            throw new NotImplementedException();
        }
    }
}
