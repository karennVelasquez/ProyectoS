using SGRA2._0.Model;
using SGRA2._0.Repositories;

namespace SGRA2._0.Service
{
    public interface IDocumentTypeService
    {
        Task<List<DocumentType>> GetAll();
        Task<DocumentType> GetDocumentType(int IdDocumentType);
        Task<DocumentType> CreateDocumentType(string Document);
        Task<DocumentType> UpdateDocumentType(int IdDocumentType, string? Document = null);
        Task<DocumentType> DeleteDocumentType(int IdDocumentType);
    }
    public class DocumentTypeService : IDocumentTypeService
    {
        public readonly IDocumentTypeRepositories _documentTypeRepositories;
        public DocumentTypeService(IDocumentTypeRepositories documentTypeRepositories)
        {
            _documentTypeRepositories = documentTypeRepositories;
        }
        public async Task<DocumentType> CreateDocumentType(string Document)
        {
            return await _documentTypeRepositories.CreateDocumentType(Document);
            //throw new NotImplementedException();
        }

        public async Task<DocumentType> DeleteDocumentType(int IdDocumentType)
        {
            // comprobar si existe
            DocumentType documentTypeToDelete = await _documentTypeRepositories.GetDocumentType(IdDocumentType);
            if (documentTypeToDelete == null)
            {
                throw new Exception($"El tipo de documento con el Id {IdDocumentType} no existe");
            }
            documentTypeToDelete.IsDelete = true;
            documentTypeToDelete.IdDocumentType = IdDocumentType;
            documentTypeToDelete.Date = DateTime.Now;

            return await _documentTypeRepositories.DeleteDocumentType(documentTypeToDelete);
            //throw new NotImplementedException();
        }

        public async Task<List<DocumentType>> GetAll()
        {
            return await _documentTypeRepositories.GetAll();
            //throw new NotImplementedException();
        }

        public async Task<DocumentType> GetDocumentType(int IdDocumentType)
        {
            return await _documentTypeRepositories.GetDocumentType(IdDocumentType);
            //throw new NotImplementedException();
        }

        public async Task<DocumentType> UpdateDocumentType(int IdDocumentType, string? Document = null)
        {
            DocumentType newdocumentType = await _documentTypeRepositories.GetDocumentType(IdDocumentType);
            if (newdocumentType != null)
            {
                if (Document != null)
                {
                    newdocumentType.Document = Document;
                }
                return await _documentTypeRepositories.UpdateDocumentType(newdocumentType);
            }
            throw new NotImplementedException();
        }
    }
}
