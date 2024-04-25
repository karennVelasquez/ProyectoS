using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface ISaleService
    {
        Task<List<Sale>> GetAll();
        Task<Sale> GetSale(int IdSale);
        Task<Sale> CreateSale(int IdCustomer, DateTime SaleDate);
        Task<Sale> UpdateSale(int IdSale, int? IdCustomer = null, DateTime? SaleDate = null);
        Task<Sale> DeleteSale(int IdSale);
    }
    public class SaleService : ISaleService
    {
        public readonly SaleRepositories _saleRepositories;
        public SaleService(SaleRepositories saleRepositories)
        {
            _saleRepositories = saleRepositories;
        }
        public async Task<Sale> CreateSale(int IdCustomer, DateTime SaleDate)
        {
            return await _saleRepositories.CreateSale(IdCustomer, SaleDate);
            //throw new NotImplementedException();
        }

        public async Task<Sale> DeleteSale(int IdSale)
        {
            // comprobar si existe
            Sale saleToDelete = await _saleRepositories.GetSale(IdSale);
            if (saleToDelete == null)
            {
                throw new Exception($"La venta con el Id {IdSale} no existe");
            }
            saleToDelete.IsDelete = true;
            saleToDelete.Date = DateTime.Now;

            return await _saleRepositories.DeleteSale(saleToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<Sale>> GetAll()
        {
            return await _saleRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Sale> GetSale(int IdSale)
        {
            return await _saleRepositories.GetSale(IdSale);
            //throw new NotImplementedException();
        }
        public async Task<Sale> UpdateSale(int IdSale, int? IdCustomer = null, DateTime? SaleDate = null)
        {
            Sale newsale = await _saleRepositories.GetSale(IdSale);
            if (newsale != null)
            {
                if (IdCustomer != null)
                {
                    newsale.IdCustomer = (int)IdCustomer;
                }
                if (SaleDate != null)
                {
                    newsale.SaleDate = (DateTime)SaleDate;
                }
                return await _saleRepositories.UpdateSale(newsale);
            }
            throw new NotImplementedException();
        }
    }
}
