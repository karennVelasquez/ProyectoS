using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface IFinalCompostService
    {
        Task<List<FinalCompost>> GetAll();
        Task<FinalCompost> GetFinalCompost(int IdFinalCompost);
        Task<FinalCompost> CreateFinalCompost(int IdWaste, string HumidityLevel, string FinalPh, string Nutrients);
        Task<FinalCompost> UpdateFinalCompost(int IdFinalCompost, int? IdWaste = null, string? HumidityLevel = null, string? FinalPh = null, string? Nutrients = null);
        Task<FinalCompost> DeleteFinalCompost(int IdFinalCompost);
    }
    public class FinalCompostService : IFinalCompostService
    {
        public readonly FinalCompostRepositories _finalCompostRepositories;
        public FinalCompostService(FinalCompostRepositories finalCompostRepositories)
        {
            _finalCompostRepositories = finalCompostRepositories;
        }
        public async Task<FinalCompost> CreateFinalCompost(int IdWaste, string HumidityLevel, string FinalPh, string Nutrients)
        {
            return await _finalCompostRepositories.CreateFinalCompost(IdWaste, HumidityLevel, FinalPh, Nutrients);
            //throw new NotImplementedException();
        }

        public async Task<FinalCompost> DeleteFinalCompost(int IdFinalCompost)
        {
            // comprobar si existe
            FinalCompost finalCompostToDelete = await _finalCompostRepositories.GetFinalCompost(IdFinalCompost);
            if (finalCompostToDelete == null)
            {
                throw new Exception($"El compost con el Id {IdFinalCompost} no existe");
            }
            finalCompostToDelete.IsDelete = true;
            finalCompostToDelete.Date = DateTime.Now;
            return await _finalCompostRepositories.DeleteFinalCompost(finalCompostToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<FinalCompost>> GetAll()
        {
            return await _finalCompostRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<FinalCompost> GetFinalCompost(int IdFinalCompost)
        {
            return await _finalCompostRepositories.GetFinalCompost(IdFinalCompost);
            //throw new NotImplementedException();
        }

        public async Task<FinalCompost> UpdateFinalCompost(int IdFinalCompost, int? IdWaste = null, string? HumidityLevel = null, string? FinalPh = null, string? Nutrients = null)
        {
            FinalCompost newcompostfinal = await _finalCompostRepositories.GetFinalCompost(IdFinalCompost);
            if (newcompostfinal != null)
            {
                if (IdWaste != null)
                {
                    newcompostfinal.IdWaste = (int)IdWaste;
                }
                if (HumidityLevel != null)
                {
                    newcompostfinal.HumidityLevel = HumidityLevel;
                }
                if (FinalPh != null)
                {
                    newcompostfinal.FinalPh = FinalPh;
                }
                if (Nutrients != null)
                {
                    newcompostfinal.Nutrients = Nutrients;
                }
                return await _finalCompostRepositories.UpdateFinalCompost(newcompostfinal);
            }
            throw new NotImplementedException();
        }
    }
}
