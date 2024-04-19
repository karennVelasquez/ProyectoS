using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class Employee
    {
        //Empleado
        [Key]
        public int IdEmployee { get; set; }
        public required int IdPerson { get; set; }
    //    public required Person Person { get; set; }
        public required string Position { get; set; }
    }
}
