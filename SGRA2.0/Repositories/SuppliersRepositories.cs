using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface ISuppliersRepositories
    {
        Task<List<Suppliers>> GetAll();
        Task<Suppliers> GetSuppliers(int id);
        Task<Suppliers> CreateSuppliers(int IdPerson, int IdWasteType);
        Task<Suppliers> UpdateSuppliers(Suppliers suppliers);
        Task<Suppliers> DeleteSuppliers(Suppliers suppliers);
    }
    public class SuppliersRepositories : ISuppliersRepositories
    {
        private readonly PersonDBContext _db;
        public SuppliersRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<Suppliers> CreateSuppliers(int IdPerson, int IdWasteType)
        {
            Suppliers newSuppliers = new Suppliers
            {
                IdPerson = IdPerson,
                IdWasteType = IdWasteType
            };
            _db.suppliers.AddAsync(newSuppliers);
            _db.SaveChanges();
            return newSuppliers;
        }
        public async Task<Suppliers> DeleteSuppliers(Suppliers suppliers)
        {
            _db.suppliers.Attach(suppliers); //Llamamos la actualizacion
            _db.Entry(suppliers).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return suppliers;
        }
        public async Task<List<Suppliers>> GetAll()
        {
            return await _db.suppliers.ToListAsync();
        }
        public async Task<Suppliers> GetSuppliers(int id)
        {
            return await _db.suppliers.FirstOrDefaultAsync(u => u.IdSuppliers == id);
        }
        public async Task<Suppliers> UpdateSuppliers(Suppliers suppliers)
        {
            _db.suppliers.Attach(suppliers); //Llamamos la actualizacion
            _db.Entry(suppliers).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return suppliers;  
        }
    }
}
