using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service

{
    public interface IWasteTypeService
    {
        Task<List<WasteType>> GetAll();
        Task<WasteType> GetWasteType(int IdWasteType);
        Task<WasteType> CreateWasteType(string Waste_Type, string Description, string Descomposition);
        Task<WasteType> UpdateWasteType(int IdWasteType, string? Waste_Type = null, string? Description = null, string? Descomposition = null);
        Task<WasteType> DeleteWasteType(int IdWasteType);
    }
    public class WasteTypeService : IWasteTypeService
    {
        public readonly IWasteTypeRepositories _wasteTypeRepositories;
        public WasteTypeService(IWasteTypeRepositories wasteTypeRepositories)
        {
            _wasteTypeRepositories = wasteTypeRepositories;
        }

        public async Task<WasteType> CreateWasteType(string Waste_Type, string Description, string Descomposition)
        {
            return await _wasteTypeRepositories.CreateWasteType(Waste_Type, Description, Descomposition);
            //throw new NotImplementedException();
        }

        public async Task<WasteType> DeleteWasteType(int IdWasteType)
        {
            // comprobar si existe
            WasteType wasteTypeToDelete = await _wasteTypeRepositories.GetWasteType(IdWasteType);
            if (wasteTypeToDelete == null)
            {
                throw new Exception($"El tipo de residuo con el Id {IdWasteType} no existe");
            }
            wasteTypeToDelete.IsDelete = true;
            wasteTypeToDelete.Date = DateTime.Now;
            return await _wasteTypeRepositories.DeleteWasteType(wasteTypeToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<WasteType>> GetAll()
        {
            return await _wasteTypeRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<WasteType> GetWasteType(int IdWasteType)
        {
            return await _wasteTypeRepositories.GetWasteType(IdWasteType);
            //throw new NotImplementedException();
        }

        public async Task<WasteType> UpdateWasteType(int IdWasteType, string? Waste_Type = null, string? Description = null, string? Descomposition = null)
        {
            WasteType newwasteType = await _wasteTypeRepositories.GetWasteType(IdWasteType);
            if (newwasteType != null)
            {
                if (Waste_Type != null)
                {
                    newwasteType.Waste_Type = Waste_Type;
                }
                if (Description != null)
                {
                    newwasteType.Description = Description;
                }
                if (Descomposition != null)
                {
                    newwasteType.Descomposition = Descomposition;
                }
                return await _wasteTypeRepositories.UpdateWasteType(newwasteType);
            }
            throw new NotImplementedException();
        }
    }
 }
