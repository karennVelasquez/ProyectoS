using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface ISaleRepositories
    {
        Task<List<Sale>> GetAll();
        Task<Sale> GetSale(int id);
        Task<Sale> CreateSale(int IdCustomer, DateTime SaleDate);
        Task<Sale> UpdateSale(Sale sale);
        Task<Sale> DeleteSale(Sale sale);
    }
    public class SaleRepositories : ISaleRepositories
    {
        private readonly PersonDBContext _db;
        public SaleRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<Sale> CreateSale(int IdCustomer, DateTime SaleDate)
        {
            Sale newSale = new Sale
            {
                IdCustomer = IdCustomer,
                SaleDate = SaleDate
            };
            _db.sales.Add(newSale);
            _db.SaveChanges();
            return newSale;
        }
        public async Task<Sale> DeleteSale(Sale sale)
        {
            _db.sales.Attach(sale); //Llamamos la actualizacion
            _db.Entry(sale).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return sale;
        }
        public async Task<List<Sale>> GetAll()
        {
            return await _db.sales.ToListAsync();
        }
        public async Task<Sale> GetSale(int id)
        {
            return await _db.sales.FirstOrDefaultAsync(u => u.IdSale == id);
        }
        public async Task<Sale> UpdateSale(Sale sale)
        {
            _db.sales.Attach(sale); //Llamamos la actualizacion
            _db.Entry(sale).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return sale;
        }
    }
}
