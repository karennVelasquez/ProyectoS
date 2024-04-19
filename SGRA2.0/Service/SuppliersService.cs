using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface ISuppliersService
    {
        Task<List<Suppliers>> GetAll();
        Task<Suppliers> GetSuppliers(int IdSuppliers);
        Task<Suppliers> CreateSuppliers(int IdPerson, int IdWasteType);
        Task<Suppliers> UpdateSuppliers(int IdSuppliers, int? IdPerson = null, int? IdWasteType = null);
        Task<Suppliers> DeleteSuppliers(int IdSuppliers);
    }
    public class SuppliersService : ISuppliersService
    {
        public readonly SuppliersRepositories _suppliersRepositories;
        public SuppliersService(SuppliersRepositories suppliersRepositories)
        {
            _suppliersRepositories = suppliersRepositories;
        }
        public async Task<Suppliers> CreateSuppliers(int IdPerson, int IdWasteType)
        {
            return await _suppliersRepositories.CreateSuppliers(IdPerson, IdWasteType);
            //throw new NotImplementedException();
        }

        public async Task<Suppliers> DeleteSuppliers(int IdSuppliers)
        {
            // comprobar si existe
            Suppliers suppliersToDelete = await _suppliersRepositories.GetSuppliers(IdSuppliers);
            if (suppliersToDelete == null)
            {
                throw new Exception($"El proveedor con el Id {IdSuppliers} no existe");
            }

            return await _suppliersRepositories.DeleteSuppliers(suppliersToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<Suppliers>> GetAll()
        {
            return await _suppliersRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Suppliers> GetSuppliers(int IdSuppliers)
        {
            return await _suppliersRepositories.GetSuppliers(IdSuppliers);
            //throw new NotImplementedException();
        }

        public async Task<Suppliers> UpdateSuppliers(int IdSuppliers, int? IdPerson = null, int? IdWasteType = null)
        {
            Suppliers newsuppliers = await _suppliersRepositories.GetSuppliers(IdSuppliers);
            if (newsuppliers != null)
            {
                if (IdPerson != null)
                {
                    newsuppliers.IdPerson = (int)IdPerson;
                }
                if (IdWasteType != null)
                {
                    newsuppliers.IdWasteType = (int)IdWasteType;
                }
                return await _suppliersRepositories.UpdateSuppliers(newsuppliers);
            }
            throw new NotImplementedException();
        }
    }
}
