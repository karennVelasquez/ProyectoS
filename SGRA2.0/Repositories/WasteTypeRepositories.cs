using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IWasteTypeRepositories
    {
        Task<List<WasteType>> GetAll();
        Task<WasteType> GetWasteType(int IdWasteType);
        Task<WasteType> CreateWasteType(string Waste_Type, string Description, string Descomposition);
        Task<WasteType> UpdateWasteType(WasteType wasteType);
        Task<WasteType> DeleteWasteType(WasteType wasteType);
    }
    public class WasteTypeRepositories : IWasteTypeRepositories
    {

        private readonly PersonDBContext _db;
        public WasteTypeRepositories (PersonDBContext db)
        {
            _db = db;
        }
        public async Task<WasteType> CreateWasteType(string Waste_Type, string Description, string Descomposition)
        {
            WasteType newWasteType = new WasteType
            {
                Waste_Type = Waste_Type,
                Description = Description,
                Descomposition = Descomposition
            };
            _db.wasteTypes.AddAsync(newWasteType);
            _db.SaveChanges();
            return newWasteType;
        }
        public async Task<WasteType> DeleteWasteType(WasteType wasteType)
        {
            _db.wasteTypes.Attach(wasteType); //Llamamos la actualizacion
            _db.Entry(wasteType).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return wasteType;
        }
        public async Task<List<WasteType>> GetAll()
        {
            return await _db.wasteTypes.ToListAsync();
        }
        public async Task<WasteType> GetWasteType(int id)
        {
            return await _db.wasteTypes.FirstOrDefaultAsync(u => u.IdWasteType == id);
        }
        public async Task<WasteType> UpdateWasteType(WasteType wasteType)
        {
            WasteType WasteTypeUpdate = await _db.wasteTypes.FindAsync(wasteType.IdWasteType);
            if (WasteTypeUpdate != null) 
            {
                WasteTypeUpdate.Waste_Type = wasteType.Waste_Type;
                WasteTypeUpdate.Description = wasteType.Description;
                WasteTypeUpdate.Descomposition = wasteType.Descomposition;

                await _db.SaveChangesAsync();
            }
            return WasteTypeUpdate;
            /*
             _db.historicalCost.Attach(historicalCost); //Llamamos la actualizacion
             _db.Entry(historicalCost).State = EntityState.Modified;
             await _db.SaveChangesAsync();
             return historicalCost;
             */
        }
    }
}
