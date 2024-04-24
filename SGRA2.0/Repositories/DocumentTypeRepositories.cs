using Microsoft.EntityFrameworkCore;
using SGRA2._0.Context;
using SGRA2._0.Model;
using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Repositories
{
    public interface IDocumentTypeRepositories
    {
        Task<List<DocumentType>> GetAll();
        Task<DocumentType> GetDocumentType(int IdDocument);
        Task<DocumentType> CreateDocumentType(string Document);
        Task<DocumentType> UpdateDocumentType(DocumentType documentType);
        Task<DocumentType> DeleteDocumentType(DocumentType documentType);
    }
    public class DocumentTypeRepositories : IDocumentTypeRepositories
    {
        private readonly PersonDBContext _db;
        public DocumentTypeRepositories(PersonDBContext db)
        {
            _db = db;
        }
        public async Task<DocumentType> CreateDocumentType(string Document)
        {
            DocumentType newDocumentType = new DocumentType
            {
                Document = Document,
                //
            };
            _db.documentTypes.AddAsync(newDocumentType);
            _db.SaveChanges();
            return newDocumentType;
        }
        public async Task<DocumentType> DeleteDocumentType(DocumentType documentType)
        {
            _db.documentTypes.Attach(documentType); //Llamamos la actualizacion
            _db.Entry(documentType).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return documentType;
        }
        public async Task<List<DocumentType>> GetAll()
        {
            return await _db.documentTypes.ToListAsync();
        }
        public async Task<DocumentType> GetDocumentType(int id)
        {
            return await _db.documentTypes.FirstOrDefaultAsync(u => u.IdDocumentType == id);
        }
        public async Task<DocumentType> UpdateDocumentType(DocumentType documentType)
        {
            DocumentType DocumentTypeUpdate = await _db.documentTypes.FindAsync(documentType.IdDocumentType);
            if (DocumentTypeUpdate != null)
            {
                
                DocumentTypeUpdate.Document = documentType.Document;

                await _db.SaveChangesAsync();
            }
            //_db.documentTypes.Attach(documentType); //Llamamos la actualizacion
            //_db.Entry(documentType).State = EntityState.Modified;
            //await _db.SaveChangesAsync();
            return documentType;
        }
    }
}
