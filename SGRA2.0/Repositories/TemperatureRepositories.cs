using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface ITemperatureRepositories
    {
        Task<List<Temperature>> GetAll();
        Task<Temperature> GetTemperature(int IdTemperature);
        Task<Temperature> CreateTemperature(int IdWaste, string Decompositiontemperature, string Range);
        Task<Temperature> UpdateTemperature (Temperature temperature);
        Task<Temperature> DeleteTemperature(Temperature temperature);

    }
    public class TemperatureRepositories : ITemperatureRepositories
    {
        private readonly PersonDBContext _db;
        public TemperatureRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<Temperature> CreateTemperature(int IdWaste, string Decompositiontemperature, string Range)
        {
            Temperature newTemperature = new Temperature
            {
                IdWaste = IdWaste,
                Decompositiontemperature = Decompositiontemperature,
                Range = Range
            };
            _db.temperatures.AddAsync(newTemperature);
            _db.SaveChanges();
            return newTemperature;
        }
        public async Task<Temperature> DeleteTemperature(Temperature temperature)
        {
            _db.temperatures.Attach(temperature); //Llamamos la actualizacion
            _db.Entry(temperature).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return temperature;
        }
        public async Task<List<Temperature>> GetAll()
        {
            return await _db.temperatures.ToListAsync();
        }

        public async Task<Temperature> GetTemperature(int id)
        {
            return await _db.temperatures.FirstOrDefaultAsync(u => u.IdTemperature  == id);
        }
        public async Task<Temperature> UpdateTemperature(Temperature temperature)
        {
            Temperature TemperatureUpdate = await _db.temperatures.FindAsync(temperature.IdTemperature);    
            if (TemperatureUpdate != null) 
            {
                TemperatureUpdate.IdWaste = temperature.IdWaste;
                TemperatureUpdate.Decompositiontemperature = temperature.Decompositiontemperature;  
                TemperatureUpdate.Range = temperature.Range;

                await _db.SaveChangesAsync();
            }
            return TemperatureUpdate;
            /*
            _db.historicalCost.Attach(historicalCost); //Llamamos la actualizacion
            _db.Entry(historicalCost).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return historicalCost;
            */
        }
    }
}
