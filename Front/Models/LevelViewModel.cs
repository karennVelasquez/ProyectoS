using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Models
{
    public class LevelViewModel
    {
        [DisplayName("Id")]
        public int IdLevel { get; set; }
        public int NumLevel { get; set; }
        public bool IsDelete { get; set; }
    }
}