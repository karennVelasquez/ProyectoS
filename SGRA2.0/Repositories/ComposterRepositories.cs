using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IComposterRepositories
    {
        Task<List<Composter>> GetAll();
        Task<Composter> GetComposter(int id);
        Task<Composter> CreateComposter(string Material, string DrainageSystem);
        Task<Composter> UpdateComposter(Composter composter);
        Task<Composter> DeleteComposter(Composter composter);
    }
    public class ComposterRepositories : IComposterRepositories
    {
        private readonly PersonDBContext _db;
        public ComposterRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<Composter> CreateComposter( string Material, string DrainageSystem)
        {
            Composter newCompostador = new Composter
            {
                
                Material = Material,
                DrainageSystem = DrainageSystem,
                IsDelete = false,
                Date = null
            };
            _db.composters.AddAsync(newCompostador);
            _db.SaveChanges();
            return newCompostador;
        }
        public async Task<Composter> DeleteComposter(Composter composter)
        {
            _db.composters.Attach(composter); //Llamamos la actualizacion
            _db.Entry(composter).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return composter;
        }
        public async Task<List<Composter>> GetAll()
        {
            return await _db.composters.ToListAsync();
        }
        public async Task<Composter> GetComposter(int id)
        {
            return await _db.composters.FirstOrDefaultAsync(u => u.IdComposter == id);
        }
        public async Task<Composter> UpdateComposter(Composter composter)
        {
            Composter ComposterUpdate = await _db.composters.FindAsync(composter.IdComposter);
            if (ComposterUpdate != null)
            {
                
                
                ComposterUpdate.Material = composter.Material;
                ComposterUpdate.DrainageSystem = composter.DrainageSystem;

                await _db.SaveChangesAsync();
            }
            //_db.composters.Attach(composter); //Llamamos la actualizacion
            //_db.Entry(composter).State = EntityState.Modified;
            //await _db.SaveChangesAsync();
            return composter; 
        }
    }
}
