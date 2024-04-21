using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IScoreRepositories
    {
        Task<List<Score>> GetAll();
        Task<Score> GetScore(int IdScore);
        Task<Score> CreateScore(int IdUser, int IdGames, int NumScore);
        Task<Score> UpdateScore(Score score);
        Task<Score> DeleteScore(Score score);

    }
    public class ScoreRepositories : IScoreRepositories
    {
        private readonly PersonDBContext _db;
        public ScoreRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<Score> CreateScore(int IdUser, int IdGames, int NumScore)
        {    
            Score newScore = new Score
            {
                IdUser = IdUser,
                IdGames = IdGames,
                NumScore = NumScore
            };
            _db.scores.AddAsync(newScore);
            _db.SaveChanges();
            return newScore;
        }
        public async Task<Score> DeleteScore(Score score)
        {
            _db.scores.Attach(score); //Llamamos la actualizacion
            _db.Entry(score).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return score;
        }
        public async Task<List<Score>> GetAll()
        {
            return await _db.scores.ToListAsync();
        }
        public async Task<Score> GetScore(int id)
        {
            return await _db.scores.FirstOrDefaultAsync(u => u.IdScore == id);
        }
        public async Task<Score> UpdateScore(Score score)
        {
            Score ScoreUpdate = await _db.scores.FindAsync(score.IdScore);
            if (ScoreUpdate != null) 
            {
                ScoreUpdate.IdUser =score.IdUser;
                ScoreUpdate.IdGames =score.IdGames; 
                ScoreUpdate.NumScore = score.NumScore;

                await _db.SaveChangesAsync();
            }

            return ScoreUpdate;
            /*
           _db.historicalCost.Attach(historicalCost); //Llamamos la actualizacion
           _db.Entry(historicalCost).State = EntityState.Modified;
           await _db.SaveChangesAsync();
           return historicalCost;
           */
        }
    }
}
