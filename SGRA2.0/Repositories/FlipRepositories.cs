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
        Task<Flip> CreateFlip(int IdResiduos, int FrecuenciaVolteo, string DescripcionUniformidad);
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
        public async Task<Flip> CreateFlip(int IdWaste, int Flipfrequency, string UniformedDescription)
        {
            //
            Flip newFlip = new Flip
            {
                IdWaste = IdWaste,
                Flipfrequency = Flipfrequency,
                UniformedDescription = UniformedDescription
                //
            };
            _db.flips.Add(newFlip);
            _db.SaveChanges();
            return newFlip;
        }
        public async Task<Flip> DeleteFlip(Flip volteo)
        {
            _db.flips.Attach(volteo); //Llamamos la actualizacion
            _db.Entry(volteo).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return volteo;  
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
            //
            _db.flips.Attach(flip); //Llamamos la actualizacion
            _db.Entry(flip).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return flip;
        }
    }
}
