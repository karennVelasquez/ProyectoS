using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IChemicalCompositionRepositories
    {
        Task<List<ChemicalComposition>> GetAll();
        Task<ChemicalComposition> GetChemicalComposition(int IdChemicalComposition);
        Task<ChemicalComposition> CreateChemicalComposition(int IdWaste, string Chemical_Composition);
        Task<ChemicalComposition> UpdateChemicalComposition(ChemicalComposition chemicalComposition);
        Task<ChemicalComposition> DeleteChemicalComposition(ChemicalComposition chemicalComposition);
    }
    public class ChemicalCompositionRepositories : IChemicalCompositionRepositories
    {
        
        private readonly PersonDBContext _db;
        public ChemicalCompositionRepositories(PersonDBContext db)
        {
            _db = db;
            
        }
        public async Task<ChemicalComposition> CreateChemicalComposition(int IdWaste, string Chemical_Composition)
        {
            //
            ChemicalComposition newChemicalComposition = new ChemicalComposition
            {
                IdWaste = IdWaste,
                Chemical_Composition = Chemical_Composition
                //
            };
            _db.chemicalCompositions.AddAsync(newChemicalComposition);
            _db.SaveChanges();
            return newChemicalComposition;
        }
        public async Task<ChemicalComposition> DeleteChemicalComposition(ChemicalComposition chemicalComposition)
        {
            _db.chemicalCompositions.Attach(chemicalComposition); //Llamamos la actualizacion
            _db.Entry(chemicalComposition).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return chemicalComposition;
        }
        public async Task<List<ChemicalComposition>> GetAll()
        {
            return await _db.chemicalCompositions.ToListAsync();
        }
        public async Task<ChemicalComposition> GetChemicalComposition(int id)
        {
            return await _db.chemicalCompositions.FirstOrDefaultAsync(u => u.IdChemicalComposition == id);
        }
        public async Task<ChemicalComposition> UpdateChemicalComposition(ChemicalComposition chemicalComposition)
        {
            ChemicalComposition ChemicalCompositionUpdate = await _db.chemicalCompositions.FindAsync(chemicalComposition.IdChemicalComposition);
            if (ChemicalCompositionUpdate != null)
            {
                
                ChemicalCompositionUpdate.IdWaste= chemicalComposition.IdWaste;
                ChemicalCompositionUpdate.Chemical_Composition = chemicalComposition.Chemical_Composition;

                await _db.SaveChangesAsync();
            }
            //_db.chemicalCompositions.Attach(chemicalComposition); //Llamamos la actualizacion
            //_db.Entry(chemicalComposition).State = EntityState.Modified;
            //await _db.SaveChangesAsync();
            return chemicalComposition;
        }
    }
}
