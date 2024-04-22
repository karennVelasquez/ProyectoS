using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class DocumentType
    {
        //TipoDocumento
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDocumentType { get; set; }
        public required string Document { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Date {  get; set; }
    }
}
