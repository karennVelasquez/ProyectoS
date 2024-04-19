using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface ICollectWasteRepositories
    {
        Task<List<CollectWaste>> GetAll();
        Task<CollectWaste> GetCollectWaste(int id);
        Task<CollectWaste> CreateCollectWaste(int IdSuppliers, int IdComposter, DateTime CollectionDate, int Amount);
        Task<CollectWaste> UpdateCollectWaste(CollectWaste collectWaste);
        Task<CollectWaste> DeleteCollectWaste(CollectWaste collectWaste);
    }
    public class CollectWasteRepositories : ICollectWasteRepositories
    {
        private readonly PersonDBContext _db;
        public CollectWasteRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<CollectWaste> CreateCollectWaste(int IdSuppliers, int IdComposter, DateTime CollectionDate, int Amount)
        {
            CollectWaste newCollectWaste = new CollectWaste
            {
                IdSuppliers = IdSuppliers,
                IdComposter = IdComposter,
                CollectionDate = CollectionDate,
                Amount = Amount
            };
            _db.collectWastes.AddAsync(newCollectWaste);
            _db.SaveChanges();
            return newCollectWaste;
        }

        public async Task<CollectWaste> DeleteCollectWaste(CollectWaste collectWaste)
        {
            _db.collectWastes.Attach(collectWaste); //Llamamos la actualizacion
            _db.Entry(collectWaste).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return collectWaste;
        }
        public async Task<List<CollectWaste>> GetAll()
        {
            return await _db.collectWastes.ToListAsync();
        }
        public async Task<CollectWaste> GetCollectWaste(int id)
        {
            return await _db.collectWastes.FirstOrDefaultAsync(u => u.IdCollectWaste == id);
        }
        public async Task<CollectWaste> UpdateCollectWaste(CollectWaste collectWaste)
        {
            _db.collectWastes.Attach(collectWaste); //Llamamos la actualizacion
            _db.Entry(collectWaste).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return collectWaste;
        }
    }
}
