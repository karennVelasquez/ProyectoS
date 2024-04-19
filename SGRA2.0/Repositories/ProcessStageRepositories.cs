using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IProcessStageRepositories
    {
        Task<List<ProcessStage>> GetAll();
        Task<ProcessStage> GetProcessStage(int id);
        Task<ProcessStage> CreateProcessStage(string Stage);
        Task<ProcessStage> UpdateProcessStage(ProcessStage processStage);
        Task<ProcessStage> DeleteProcessStage(ProcessStage processStage);
    }
    public class ProcessStageRepositories : IProcessStageRepositories
    {
        private readonly PersonDBContext _db;
        public ProcessStageRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<ProcessStage> CreateProcessStage(string Stage)
        {
            ProcessStage newProcessStage = new ProcessStage
            { Stage = Stage };
            _db.processStages.AddAsync(newProcessStage);
            _db.SaveChanges();
            return newProcessStage;
        }
        public async Task<ProcessStage> DeleteProcessStage(ProcessStage processStage)
        {
            _db.processStages.Attach(processStage); //Llamamos la actualizacion
            _db.Entry(processStage).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return processStage;
        }
        public async Task<List<ProcessStage>> GetAll()
        {
            return await _db.processStages.ToListAsync();
        }
        public async Task<ProcessStage> GetProcessStage(int id)
        {
            return await _db.processStages.FirstOrDefaultAsync(u => u.IdProcessStage == id);
        }
        public async Task<ProcessStage> UpdateProcessStage(ProcessStage processStage)
        {
            _db.processStages.Attach(processStage); //Llamamos la actualizacion
            _db.Entry(processStage).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return processStage;
        }
    }
}
