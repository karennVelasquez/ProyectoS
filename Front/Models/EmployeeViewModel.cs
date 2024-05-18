using SGRA2._0.Model;
using System.ComponentModel;

namespace Front.Models
{
    public class EmployeeViewModel
    {
        [DisplayName("Id")]
        public int IdEmployee { get; set; }
        [DisplayName("Person")]
        public int IdPerson { get; set; }
        public Person Person { get; set; }
        public string Position { get; set; }
        public bool IsDelete { get; set; }
    }
}
