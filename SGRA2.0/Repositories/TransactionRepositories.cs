using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface ITransactionRepositories
    {
        Task<List<Transaction>> GetAll();
        Task<Transaction> GetTransaction(int id);
        Task<Transaction> CreateTransaction(int IdSuppliers, int DeliveredQuantity, DateTime DeliveredDate, string Price, string Quality);
        Task<Transaction> UpdateTransaction(Transaction transaction);
        Task<Transaction> DeleteTransaction(Transaction transaction);
    }
    public class TransactionRepositories : ITransactionRepositories
    {
        private readonly PersonDBContext _db;
        public TransactionRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<Transaction> CreateTransaction(int idSuppliers, int DeliveredQuantity, DateTime DeliveredDate, string Price, string Quality)
        {
            Suppliers? suppliers = _db.suppliers.FirstOrDefault(ut => ut.IdSuppliers == idSuppliers);
            Transaction newTransaction = new Transaction
            {
                IdSuppliers = idSuppliers,
                DeliveredQuantity = DeliveredQuantity,
                DeliveredDate = DeliveredDate,
                Price = Price,
                Quality = Quality,
                IsDelete = false,
                Date = null
            };
            _db.transactions.AddAsync(newTransaction);
            _db.SaveChanges();
            return newTransaction;
        }
        public async Task<Transaction> DeleteTransaction(Transaction transaction)
        {
            _db.transactions.Attach(transaction); //Llamamos la actualizacion
            _db.Entry(transaction).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return transaction;
        }
        public async Task<List<Transaction>> GetAll()
        {
            return await _db.transactions.ToListAsync();

        }
        public async Task<Transaction> GetTransaction(int id)
        {
            return await _db.transactions.FirstOrDefaultAsync(u => u.IdTransaction == id);
        }
        public async Task<Transaction> UpdateTransaction(Transaction transaction)
        {
            Transaction TransactionUpdate = await _db.transactions.FindAsync(transaction.IdTransaction);
            if (TransactionUpdate != null) 
            {
                TransactionUpdate.IdSuppliers = transaction.IdSuppliers;
                TransactionUpdate.DeliveredQuantity = transaction.DeliveredQuantity;    
                TransactionUpdate.DeliveredDate = transaction.DeliveredDate;
                TransactionUpdate.Price = transaction.Price;    
                TransactionUpdate.Quality = transaction.Quality;  

                await _db.SaveChangesAsync();
            }
            return TransactionUpdate;
            /*
            _db.historicalCost.Attach(historicalCost); //Llamamos la actualizacion
            _db.Entry(historicalCost).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return historicalCost;
            */
        }
    }
}
