using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace SGRA2._0.Repositories
{
    public interface IFinalCompostRepositories
    {
        Task<List<FinalCompost>> GetAll();
        Task<FinalCompost> GetFinalCompost(int id);
        Task<FinalCompost> CreateFinalCompost(int IdWaste, string HumidityLevel, string FinalPh, string Nutrients);
        Task<FinalCompost> UpdateFinalCompost(FinalCompost finalCompost);
        Task<FinalCompost> DeleteFinalCompost(FinalCompost finalCompost);
    }
    public class FinalCompostRepositories : IFinalCompostRepositories
    {
        private readonly PersonDBContext _db;
        public FinalCompostRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<FinalCompost> CreateFinalCompost(int idWaste, string HumidityLevel, string FinalPh, string Nutrients)
        {
            Waste? waste = _db.wastes.FirstOrDefault(ut => ut.IdWaste == idWaste);
            FinalCompost newFinalCompost = new FinalCompost
            {
                IdWaste = idWaste,
                HumidityLevel = HumidityLevel,
                FinalPh = FinalPh,
                Nutrients = Nutrients,
                IsDelete = false,
                Date = null
            };
            _db.finalComposts.AddAsync(newFinalCompost);
            _db.SaveChanges();
            return newFinalCompost;
        }
        public async Task<FinalCompost> DeleteFinalCompost(FinalCompost finalCompost)
        {
            _db.finalComposts.Attach(finalCompost); //Llamamos la actualizacion
            _db.Entry(finalCompost).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return finalCompost;
        }
        public async Task<List<FinalCompost>> GetAll()
        {
            return await _db.finalComposts.ToListAsync();
        }
        public async Task<FinalCompost> GetFinalCompost(int id)
        {
            return await _db.finalComposts.FirstOrDefaultAsync(u => u.IdFinalCompost == id);
        }
        public async Task<FinalCompost> UpdateFinalCompost(FinalCompost finalCompost)
        {
            FinalCompost FinalCompostUpdate = await _db.finalComposts.FindAsync(finalCompost.IdFinalCompost);
            if (FinalCompostUpdate != null)
            {
                FinalCompostUpdate.IdWaste = finalCompost.IdWaste;
                FinalCompostUpdate.HumidityLevel = finalCompost.HumidityLevel;
                FinalCompostUpdate.FinalPh = finalCompost.FinalPh;
                FinalCompostUpdate.Nutrients = finalCompost.Nutrients;

                await _db.SaveChangesAsync();
            }

            return FinalCompostUpdate;
            /*
            _db.finalComposts.Attach(finalCompost); //Llamamos la actualizacion
            _db.Entry(finalCompost).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return finalCompost;
            */
        }
    }
}
