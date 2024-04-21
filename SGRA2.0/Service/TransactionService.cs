using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAll();
        Task<Transaction> GetTransaction(int IdTransaction);
        Task<Transaction> CreateTransaction(int IdSuppliers, int DeliveredQuantity, DateTime DeliveredDate, string Price, string Quality);
        Task<Transaction> UpdateTransaction(int IdTransaction, int? IdSuppliers = null, int? DeliveredQuantity = null, DateTime? DeliveredDate = null, string? Precio = null, string? Quality = null);
        Task<Transaction> DeleteTransaction(int IdTransaction);
    }
    public class TransactionService : ITransactionService
    {
        public readonly TransactionRepositories _transactionRepositories;
        public TransactionService(TransactionRepositories transactionRepositories)
        {
            _transactionRepositories = transactionRepositories;
        }
        public async Task<Transaction> CreateTransaction(int IdSuppliers, int DeliveredQuantity, DateTime DeliveredDate, string Price, string Quality)
        {
            return await _transactionRepositories.CreateTransaction(IdSuppliers, DeliveredQuantity, DeliveredDate, Price, Quality);
            //throw new NotImplementedException();
        }

        public async Task<Transaction> DeleteTransaction(int IdTransaction)
        {
            // comprobar si existe
            Transaction transactionToDelete = await _transactionRepositories.GetTransaction(IdTransaction);
            if (transactionToDelete == null)
            {
                throw new Exception($"La transaccion con el Id {IdTransaction} no existe");
            }
            transactionToDelete.IsDelete = true;
            transactionToDelete.Date = DateTime.Now;
            return await _transactionRepositories.DeleteTransaction(transactionToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<Transaction>> GetAll()
        {
            return await _transactionRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Transaction> GetTransaction(int IdTransaction)
        {
            return await _transactionRepositories.GetTransaction(IdTransaction);
            //throw new NotImplementedException();
        }

        public async Task<Transaction> UpdateTransaction(int IdTransaction, int? IdSuppliers = null, int? DeliveredQuantity = null, DateTime? DeliveredDate = null, string? Price = null, string? Quality = null)
        {
            Transaction newtransaccion = await _transactionRepositories.GetTransaction(IdTransaction);
            if (newtransaccion != null)
            {
                if (IdSuppliers != null)
                {
                    newtransaccion.IdSuppliers = (int)IdSuppliers;
                }
                if (DeliveredQuantity != null)
                {
                    newtransaccion.DeliveredQuantity = (int)DeliveredQuantity;
                }
                if (DeliveredDate != null)
                {
                    newtransaccion.DeliveredDate = (DateTime)DeliveredDate;
                }
                if (Price != null)
                {
                    newtransaccion.Price = Price;
                }
                if (Quality != null)
                {
                    newtransaccion.Quality = Quality;
                }
                return await _transactionRepositories.UpdateTransaction(newtransaccion);
            }
            throw new NotImplementedException();
        }
    }
}
