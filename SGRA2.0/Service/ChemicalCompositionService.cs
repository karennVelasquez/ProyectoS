using SGRA2._0.Model;
using SGRA2._0.Repositories;
namespace SGRA2._0.Service
{
    public interface IChemicalCompositionService
    {
        Task<List<ChemicalComposition>> GetAll();
        Task<ChemicalComposition> GetChemicalComposition(int IdChemicalComposition);
        Task<ChemicalComposition> CreateChemicalComposition(int IdWaste, string Chemical_Composition);
        Task<ChemicalComposition> UpdateChemicalComposition(int IdChemicalComposition, int? IdWaste = null, string? Chemical_Composition = null);
        Task<ChemicalComposition> DeleteChemicalComposition(int IdChemicalComposition);
    }
    public class ChemicalCompositionService : IChemicalCompositionService
    {
        public readonly IChemicalCompositionRepositories _chemicalCompositionRepositories;
        public ChemicalCompositionService(IChemicalCompositionRepositories chemicalCompositionRepositories)
        {
            _chemicalCompositionRepositories = chemicalCompositionRepositories;
        }

        public async Task<ChemicalComposition> CreateChemicalComposition(int IdWaste, string Chemical_Composition)
        {
            return await _chemicalCompositionRepositories.CreateChemicalComposition(IdWaste, Chemical_Composition);
            //throw new NotImplementedException();
        }

        public async Task<ChemicalComposition> DeleteChemicalComposition(int IdChemicalComposition)
        {
            // comprobar si existe
            ChemicalComposition chemicalCompositionToDelete = await _chemicalCompositionRepositories.GetChemicalComposition(IdChemicalComposition);
            if (chemicalCompositionToDelete == null)
            {
                throw new Exception($"La composicion quimica con el Id {IdChemicalComposition} no existe");
            }

            chemicalCompositionToDelete.IsDelete = true;
            chemicalCompositionToDelete.Date = DateTime.Now;

            return await _chemicalCompositionRepositories.DeleteChemicalComposition(chemicalCompositionToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<ChemicalComposition>> GetAll()
        {
            return await _chemicalCompositionRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<ChemicalComposition> GetChemicalComposition(int IdChemicalComposition)
        {
            return await _chemicalCompositionRepositories.GetChemicalComposition(IdChemicalComposition);
            //throw new NotImplementedException();
        }

        public async Task<ChemicalComposition> UpdateChemicalComposition(int IdChemicalComposition, int? IdWaste = null, string? Chemical_Composition = null)
        {
            ChemicalComposition newchemicalComposition = await _chemicalCompositionRepositories.GetChemicalComposition(IdChemicalComposition);
            if (newchemicalComposition != null)
            {
                if (IdWaste != null)
                {
                    newchemicalComposition.IdWaste = (int)IdWaste;
                }
                if (Chemical_Composition != null)
                {
                    newchemicalComposition.Chemical_Composition = Chemical_Composition;
                }
                return await _chemicalCompositionRepositories.UpdateChemicalComposition(newchemicalComposition);
            }
            throw new NotImplementedException();
        }
    }
}
