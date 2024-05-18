using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Models
{
    public class ProcessStageViewModel
    {
        [DisplayName("Id")]
        public int IdProcessStage { get; set; }
        [DisplayName("ProcessStage")]
        public string Stage { get; set; }
        public bool IsDelete { get; set; }
    }
}