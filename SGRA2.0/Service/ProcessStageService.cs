using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface IProcessStageService
    {
        Task<List<ProcessStage>> GetAll();
        Task<ProcessStage> GetProcessStage(int IdProcessStage);
        Task<ProcessStage> CreateProcessStage(string  Stage);
        Task<ProcessStage> UpdateProcessStage(int IdProcessStage, string? Stage = null);
        Task<ProcessStage> DeleteProcessStage(int IdProcessStage);
    }
    public class ProcessStageService : IProcessStageService
    {
        public readonly ProcessStageRepositories _processStageRepositories;
        public ProcessStageService(ProcessStageRepositories etapaProcesoRepositories)
        {
            _processStageRepositories = etapaProcesoRepositories;
        }

        public async Task<ProcessStage> CreateProcessStage(string Stage)
        {
            return await _processStageRepositories.CreateProcessStage( Stage);
            //throw new NotImplementedException();
        }

        public async Task<ProcessStage> DeleteProcessStage(int IdProcessStage)
        {
            // comprobar si existe
            ProcessStage processStageToDelete = await _processStageRepositories.GetProcessStage(IdProcessStage);
            if (processStageToDelete == null)
            {
                throw new Exception($"La etapa con el Id {IdProcessStage} no existe");
            }

            return await _processStageRepositories.DeleteProcessStage(processStageToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<ProcessStage>> GetAll()
        {
            return await _processStageRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<ProcessStage> GetProcessStage(int IdProcessStage)
        {
            return await _processStageRepositories.GetProcessStage(IdProcessStage);
            //throw new NotImplementedException();
        }

        public async Task<ProcessStage> UpdateProcessStage(int IdProcessStage, string?  Stage = null)
        {
            ProcessStage newprocessStage = await _processStageRepositories.GetProcessStage(IdProcessStage);
            if (newprocessStage != null)
            {
                if (Stage != null)
                {
                    newprocessStage.Stage = Stage;
                }
                return await _processStageRepositories.UpdateProcessStage(newprocessStage);
            }
            throw new NotImplementedException();
        }
    }
}
