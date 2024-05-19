using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Models
{
    public class ComposterViewModel
    {
        [DisplayName("Id")]
        public int IdComposter { get; set; }
        public string Material { get; set; }
        public string DrainageSystem { get; set; }
        public bool IsDelete { get; set; }
    }
}