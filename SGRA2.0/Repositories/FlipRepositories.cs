using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IFlipRepositories
    {
        Task<List<Flip>> GetAll();
        Task<Flip> GetFlip(int id);
        Task<Flip> CreateFlip(int IdWaste, int Flipfrequency, string UniformedDescription);
        Task<Flip> UpdateFlip(Flip flip);
        Task<Flip> DeleteFlip(Flip flip);
    }
    public class FlipRepositories : IFlipRepositories
    {
        private readonly PersonDBContext _db;
        public FlipRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<Flip> CreateFlip(int idWaste, int Flipfrequency, string UniformedDescription)
        {
            Waste? waste = _db.wastes.FirstOrDefault(ut => ut.IdWaste == idWaste);
            Flip newFlip = new Flip
            {
                IdWaste = idWaste,
                Flipfrequency = Flipfrequency,
                UniformedDescription = UniformedDescription,
                IsDelete = false,
                Date = null
            };
            _db.flips.Add(newFlip);
            _db.SaveChanges();
            return newFlip;
        }
        public async Task<Flip> DeleteFlip(Flip flip)
        {
            _db.flips.Attach(flip); //Llamamos la actualizacion
            _db.Entry(flip).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return flip;  
        }
        public async Task<List<Flip>> GetAll()
        {
            return await _db.flips.ToListAsync();
        }
        public async Task<Flip> GetFlip(int id)
        {
            return await _db.flips.FirstOrDefaultAsync(u => u.IdFlip == id);
        }
        public async Task<Flip> UpdateFlip(Flip flip)
        {
            Flip FlipUpdate = await _db.flips.FindAsync(flip.IdFlip);
            if (FlipUpdate != null)
            {
                FlipUpdate.IdWaste = flip.IdWaste;
                FlipUpdate.Flipfrequency = flip.Flipfrequency;
                FlipUpdate.UniformedDescription = flip.UniformedDescription;

                await _db.SaveChangesAsync();
            }

            return FlipUpdate;
            /*
            _db.flips.Attach(flip); //Llamamos la actualizacion
            _db.Entry(flip).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return flip;*/
        }
    }
}
