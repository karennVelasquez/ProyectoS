using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SGRA2._0.Repositories
{
    public interface IWasteRepositories
    {
        Task<List<Waste>> GetAll();
        Task<Waste> GetWaste(int id);
        Task<Waste> CreateWaste(int IdWasteType);
        Task<Waste> UpdateWaste(Waste waste);
        Task<Waste> DeleteWaste(Waste waste);
    }
    public class WasteRepositories : IWasteRepositories
    {

        private readonly PersonDBContext _db;
        public WasteRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<Waste> CreateWaste(int idWasteType)
        {
            WasteType? wasteType = _db.wasteTypes.FirstOrDefault(ut => ut.IdWasteType == idWasteType);
            Waste newWaste = new Waste
            {
                IdWasteType = idWasteType,
                IsDelete = false,
                Date = null
            };
            _db.wastes.AddAsync(newWaste);
            _db.SaveChanges();
            return newWaste;
        }
        public async Task<Waste> DeleteWaste(Waste waste)
        {
            _db.wastes.Attach(waste); //Llamamos la actualizacion
            _db.Entry(waste).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return waste;
        }
        public async Task<List<Waste>> GetAll()
        {
            return await _db.wastes.ToListAsync();
        }
        public async Task<Waste> GetWaste(int id)
        {
            return await _db.wastes.FirstOrDefaultAsync(u => u.IdWaste == id);
        }
        public async Task<Waste> UpdateWaste(Waste waste)
        {
            Waste WasteUpdate = await _db.wastes.FindAsync(waste.IdWaste);
            if (WasteUpdate != null) 
            {
                WasteUpdate.IdWasteType = waste.IdWasteType;

                await _db.SaveChangesAsync();
            }

            return WasteUpdate;
            /*
            _db.wastes.Attach(waste); //Llamamos la actualizacion
            _db.Entry(waste).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return waste;
            */
        }
    }
}
