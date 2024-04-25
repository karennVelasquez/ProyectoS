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
        Task<CollectWaste> GetCollectWaste(int IdCollectWaste);
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
        public async Task<CollectWaste> CreateCollectWaste(int idSuppliers, int idComposter, DateTime CollectionDate, int Amount)
        {
            Suppliers? suppliers = _db.suppliers.FirstOrDefault(ut => ut.IdSuppliers == idSuppliers);
            Composter? composter = _db.composters.FirstOrDefault(ut => ut.IdComposter == idComposter);
            CollectWaste newCollectWaste = new CollectWaste
            {
                IdSuppliers = idSuppliers,
                IdComposter = idComposter,
                CollectionDate = CollectionDate,
                Amount = Amount,
                IsDelete = false,
                Date = null
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
            CollectWaste CollectWasteUpdate = await _db.collectWastes.FindAsync(collectWaste.IdCollectWaste);
            if (CollectWasteUpdate != null)
            {
                CollectWasteUpdate.IdSuppliers = collectWaste.IdSuppliers;
                CollectWasteUpdate.IdComposter = collectWaste.IdComposter;
                CollectWasteUpdate.CollectionDate = collectWaste.CollectionDate;
                CollectWasteUpdate.Amount = collectWaste.Amount;

                await _db.SaveChangesAsync();
            }
            //_db.collectWastes.Attach(collectWaste); //Llamamos la actualizacion
            //_db.Entry(collectWaste).State = EntityState.Modified;
            //await _db.SaveChangesAsync();
            return collectWaste;
        }
    }
}
