using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface IFlipService
    {
        Task<List<Flip>> GetAll();
        Task<Flip> GetFlip(int IdFlip);
        Task<Flip> CreateFlip(int IdWaste, int Flipfrequency);
        Task<Flip> UpdateFlip(int IdFlip, int? IdWaste = null, int? Flipfrequency = null);
        Task<Flip> DeleteFlip(int IdFlip);
    }
    public class FlipService : IFlipService
    {
        public readonly IFlipRepositories _flipRepositories;
        public FlipService(IFlipRepositories flipRepositories)
        {
            _flipRepositories = flipRepositories;
        }
        public async Task<Flip> CreateFlip(int IdWaste, int Flipfrequency)
        {
            return await _flipRepositories.CreateFlip(IdWaste, Flipfrequency);
            //throw new NotImplementedException();
        }

        public async Task<Flip> DeleteFlip(int IdFlip)
        {
            // comprobar si existe
            Flip flipToDelete = await _flipRepositories.GetFlip(IdFlip);
            if (flipToDelete == null)
            {
                throw new Exception($"El volteo con el Id {IdFlip} no existe");
            }
            flipToDelete.IsDelete = true;
            flipToDelete.Date = DateTime.Now;

            return await _flipRepositories.DeleteFlip(flipToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<Flip>> GetAll()
        {
            return await _flipRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<Flip> GetFlip(int IdFlip)
        {
            return await _flipRepositories.GetFlip(IdFlip);

            //throw new NotImplementedException();
        }

        public async Task<Flip> UpdateFlip(int IdFlip, int? IdWaste = null, int? Flipfrequency = null)
        {
            Flip newflip = await _flipRepositories.GetFlip(IdFlip);
            if (newflip != null)
            {
                if (IdWaste != null)
                {
                    newflip.IdWaste = (int)IdWaste;
                }
                if (Flipfrequency != null)
                {
                    newflip.Flipfrequency = (int)Flipfrequency;
                }
               
                return await _flipRepositories.UpdateFlip(newflip);
            }
            throw new NotImplementedException();
        }
    }
}
