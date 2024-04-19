using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface ICollectWasteService
    {
        Task<List<CollectWaste>> GetAll();
        Task<CollectWaste> GetCollectWaste(int IdCollectWaste);
        Task<CollectWaste> CreateCollectWaste(int IdSuppliers,int IdComposter, DateTime CollectionDate, int Amount);
        Task<CollectWaste> UpdateCollectWaste(int IdCollectWaste, int? IdSuppliers = null, int? IdComposter = null, DateTime? CollectionDate = null, int? Amount = null);
        Task<CollectWaste> DeleteCollectWaste(int IdCollectWaste);
    }
    public class CollectWasteService : ICollectWasteService
    {
        public readonly CollectWasteRepositories _collectWasteRepositories;
        public CollectWasteService(CollectWasteRepositories collectWasteRepositories)
        {
            _collectWasteRepositories = collectWasteRepositories;
        }
        public async Task<CollectWaste> CreateCollectWaste(int IdSuppliers, int IdComposter, DateTime CollectionDate, int Amount)
        {
            return await _collectWasteRepositories.CreateCollectWaste(IdSuppliers, IdComposter, CollectionDate, Amount);
            //throw new NotImplementedException();
        }

        public async Task<CollectWaste> DeleteCollectWaste(int IdCollectWaste)
        {
            // comprobar si existe
            CollectWaste collectWasteToDelete = await _collectWasteRepositories.GetCollectWaste(IdCollectWaste);
            if (collectWasteToDelete == null)
            {
                throw new Exception($"La recolecta con el Id {IdCollectWaste} no existe");
            }

            return await _collectWasteRepositories.DeleteCollectWaste(collectWasteToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<CollectWaste>> GetAll()
        {
            return await _collectWasteRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<CollectWaste> GetCollectWaste(int IdCollectWaste)
        {
            return await _collectWasteRepositories.GetCollectWaste(IdCollectWaste);
            //throw new NotImplementedException();
        }

        public async Task<CollectWaste> UpdateCollectWaste(int IdCollectWaste, int? IdSuppliers = null, int? IdComposter = null, DateTime? CollectionDate = null, int? Amount = null)
        {
            CollectWaste newcollectWaste = await _collectWasteRepositories.GetCollectWaste(IdCollectWaste);
            if (newcollectWaste != null)
            {
                if (IdSuppliers != null)
                {
                    newcollectWaste.IdSuppliers = (int)IdSuppliers;
                }
                if (IdComposter != null)
                {
                    newcollectWaste.IdComposter = (int)IdComposter;
                }
                if (CollectionDate != null)
                {
                    newcollectWaste.CollectionDate = (DateTime)CollectionDate;
                }
                if (Amount != null)
                {
                    newcollectWaste.Amount = (int)Amount;
                }
                return await _collectWasteRepositories.UpdateCollectWaste(newcollectWaste);
            }
            throw new NotImplementedException();
        }
    }
}
