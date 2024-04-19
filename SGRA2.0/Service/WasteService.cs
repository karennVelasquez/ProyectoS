using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface IWasteService
    {
        Task<List<Waste>> GetAll();
        Task<Waste> GetWaste(int IdWaste);
        Task<Waste> CreateWaste(int IdWasteType, string Humidity);
        Task<Waste> UpdateWaste(int IdWaste, int? IdWasteType = null, string? Humidity = null);
        Task<Waste> DeleteWaste(int IdWaste);
    }
    public class WasteService : IWasteService
    {
        public readonly WasteRepositories _wasteRepositories;
        public WasteService(WasteRepositories wasteRepositories)
        {
            _wasteRepositories = wasteRepositories;
        }
        public async Task<Waste> CreateWaste(int IdWasteType, string Humidity)
        {
            return await _wasteRepositories.CreateWaste(IdWasteType, Humidity);
            //throw new NotImplementedException();
        }

        public async Task<Waste> DeleteWaste(int IdWaste)
        {
            // comprobar si existe
            Waste wasteToDelete = await _wasteRepositories.GetWaste(IdWaste);
            if (wasteToDelete == null)
            {
                throw new Exception($"El residuo con el Id {IdWaste} no existe");
            }

            return await _wasteRepositories.DeleteWaste(wasteToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<Waste>> GetAll()
        {
            return await _wasteRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Waste> GetWaste(int IdWaste)
        {
            return await _wasteRepositories.GetWaste(IdWaste);
            //throw new NotImplementedException();
        }

        public async Task<Waste> UpdateWaste(int IdWaste, int? IdWasteType = null, string? Humidity = null)
        {
            Waste newwaste = await _wasteRepositories.GetWaste(IdWaste);
            if (newwaste != null)
            {
                if (IdWasteType != null)
                {
                    newwaste.IdWasteType = (int)IdWasteType;
                }
                if (Humidity != null)
                {
                    newwaste.Humidity = Humidity;
                }
                return await _wasteRepositories.UpdateWaste(newwaste);
            }
            throw new NotImplementedException();
        }
    }
}
