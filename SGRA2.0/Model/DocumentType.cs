using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class DocumentType
    {
        //TipoDocumento
        [Key]
        public int IdDocumentType { get; set; }
        public required string Document { get; set; }
    }
}
