using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface ITemperatureService
    {
        Task<List<Temperature>> GetAll();
        Task<Temperature> GetTemperature(int IdTemperature);
        Task<Temperature> CreateTemperature(int IdWaste, string Decompositiontemperature, string Range);
        Task<Temperature> UpdateTemperature(int IdTemperature, int? IdWaste = null, string? Decompositiontemperature = null, string? Range = null);
        Task<Temperature> DeleteTemperature(int IdTemperature);
    }
    public class TemperatureService : ITemperatureService
    {
        public readonly TemperatureRepositories _temperatureRepositories;
        public TemperatureService(TemperatureRepositories _temperatureRepositories)
        {
            _temperatureRepositories = _temperatureRepositories;
        }
        public async Task<Temperature> CreateTemperature(int IdWaste, string Decompositiontemperature, string Range)
        {
            return await _temperatureRepositories.CreateTemperature(IdWaste, Decompositiontemperature, Range);
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

        public async Task<Temperature> UpdateTemperature(int IdTemperature, int? IdWaste = null, string? Decompositiontemperature = null, string? Range = null)
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
                if (Range != null)
                {
                    newtemperature.Range = Range;
                }
                return await _temperatureRepositories.UpdateTemperature(newtemperature);
            }
            throw new NotImplementedException();
        }
    }
}
