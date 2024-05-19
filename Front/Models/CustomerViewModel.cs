using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SGRA2._0.Model;

namespace Front.Models
{
    public class CustomerViewModel
    {
        [DisplayName("Id")]
        public int IdCustomer { get; set; }
        [DisplayName("Customer")]
        public int IdPerson { get; set; }
        public Person Person { get; set; }
        public bool IsDelete { get; set; }
    }
}