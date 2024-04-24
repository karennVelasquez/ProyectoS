using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Customer
    {
        //Cliente
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCustomer { get; set; }
        public int IdPerson { get; set; }
        public  Person Person { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? Date {  get; set; }
    }
}
