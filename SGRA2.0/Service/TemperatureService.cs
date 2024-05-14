using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface ITemperatureService
    {
        Task<List<Temperature>> GetAll();
        Task<Temperature> GetTemperature(int IdTemperature);
        Task<Temperature> CreateTemperature(int IdWaste, string Decompositiontemperature);
        Task<Temperature> UpdateTemperature(int IdTemperature, int? IdWaste = null, string? Decompositiontemperature = null);
        Task<Temperature> DeleteTemperature(int IdTemperature);
    }
    public class TemperatureService : ITemperatureService
    {
        public readonly ITemperatureRepositories _temperatureRepositories;
        public TemperatureService(ITemperatureRepositories temperatureRepositories)
        {
            _temperatureRepositories = temperatureRepositories;
        }
        public async Task<Temperature> CreateTemperature(int IdWaste, string Decompositiontemperature)
        {
            return await _temperatureRepositories.CreateTemperature(IdWaste, Decompositiontemperature);
            //throw new NotImplementedException();
        }

        public async Task<Temperature> DeleteTemperature(int IdTemperature)
        {
            // comprobar si existe
            Temperature temperatureToDelete = await _temperatureRepositories.GetTemperature(IdTemperature);
            if (temperatureToDelete == null)
            {
                throw new Exception($"La temperatura con el Id {IdTemperature} no existe");
            }
            temperatureToDelete.IsDelete = true;
            temperatureToDelete.Date = DateTime.Now;
            return await _temperatureRepositories.DeleteTemperature(temperatureToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<Temperature>> GetAll()
        {
            return await _temperatureRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Temperature> GetTemperature(int IdTemperature)
        {
            return await _temperatureRepositories.GetTemperature(IdTemperature);
            //throw new NotImplementedException();
        }

        public async Task<Temperature> UpdateTemperature(int IdTemperature, int? IdWaste = null, string? Decompositiontemperature = null)
        {
            Temperature newtemperature = await _temperatureRepositories.GetTemperature(IdTemperature);
            if (newtemperature != null)
            {
                if (IdWaste != null)
                {
                    newtemperature.IdWaste = (int)IdWaste;
                }
                if (Decompositiontemperature != null)
                {
                    newtemperature.Decompositiontemperature = Decompositiontemperature;
                }
                
                return await _temperatureRepositories.UpdateTemperature(newtemperature);
            }
            throw new NotImplementedException();
        }
    }
}
