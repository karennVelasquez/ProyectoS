using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
    public class PersonViewModel
    {
        [DisplayName("Id")]
        public int IdPerson { get; set; }
        [DisplayName("IdPerson")]
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int IdDocumentType { get; set; }
        public DocumentTYpeViewModel DocumentType { get; set; }
        public int Document { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}