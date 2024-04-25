using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Person
    {
        //Persona
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPerson { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int IdDocumentType { get; set; }
        public DocumentType DocumentType { get; set; }
        public int Document { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }

    }
}
