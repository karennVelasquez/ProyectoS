using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SGRA2._0.Model
{
    public class Person
    {
        //Persona
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPerson { get; set; }
        public required string Name { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public required int IdDocumentType { get; set; }
        public required DocumentType DocumentType { get; set; }
        public required int Document { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }

    }
}
