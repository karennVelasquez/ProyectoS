using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface IComposterService
    {
        Task<List<Composter>> GetAll();
        Task<Composter> GetComposter(int IdComposter);
        Task<Composter> CreateComposter(string Size, string Material, string DrainageSystem);
        Task<Composter> UpdateComposter(int IdComposter, string? Size = null, string? Material = null, string? DrainageSystem = null);
        Task<Composter> DeleteComposter(int IdComposter);
    }
    public class ComposterService : IComposterService
    {
        public readonly ComposterRepositories _composterRepositories;
        public ComposterService(ComposterRepositories composterRepositories)
        {
            _composterRepositories = composterRepositories;
        }

        public async Task<Composter> CreateComposter(string Size, string Material, string DrainageSystem)
        {
            return await _composterRepositories.CreateComposter(Size, Material, DrainageSystem);
            //throw new NotImplementedException();
        }

        public async Task<Composter> DeleteComposter(int IdComposter)
        {
            // comprobar si existe
            Composter composterToDelete = await _composterRepositories.GetComposter(IdComposter);
            if (composterToDelete == null)
            {
                throw new Exception($"El compostador con el Id {IdComposter} no existe");
            }

            composterToDelete.IsDeleted = true;
            composterToDelete.Date = DateTime.Now;

            return await _composterRepositories.DeleteComposter(composterToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<Composter>> GetAll()
        {
            return await _composterRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Composter> GetComposter(int IdComposter)
        {
            return await _composterRepositories.GetComposter(IdComposter);
            //throw new NotImplementedException();
        }

        public async Task<Composter> UpdateComposter(int IdComposter, string? Size = null, string? Material = null, string? DrainageSystem = null)
        {
            Composter newcomposter = await _composterRepositories.GetComposter(IdComposter);
            if (newcomposter != null)
            {
                if (Size != null)
                {
                    newcomposter.Size = Size;
                }
                if (Material != null)
                {
                    newcomposter.Material = Material;
                }
                if (DrainageSystem != null)
                {
                    newcomposter.DrainageSystem = DrainageSystem;
                }
                return await _composterRepositories.UpdateComposter(newcomposter);
            }
            throw new NotImplementedException();
        }
    }
}
