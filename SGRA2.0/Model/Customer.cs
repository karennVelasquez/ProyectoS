using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class Customer
    {
        //Cliente
        [Key]
        public int IdCustomer { get; set; }
        public required int IdPerson { get; set; }
    //    public required Person Person { get; set; }
    }
}
