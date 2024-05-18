using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SGRA2._0.Model;

namespace Front.Models
{
    public class PersonLoginViewModel
    {
        [DisplayName("Id")]
        public int IdLoginP { get; set; }
        [DisplayName("LoginP")]
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IdPerson { get; set; }
        public Person Person { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}