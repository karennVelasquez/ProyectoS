using System.ComponentModel.DataAnnotations;
namespace SGRA2._0.Model
{
    public class Person
    {
        //Persona
        [Key]
        public int IdPerson { get; set; }
        public required string Name { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
        public required int IdDocumentType { get; set; }
        public required DocumentType DocumentType { get; set; }
        public required int Document { get; set; }
    }
}
