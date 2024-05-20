using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SGRA2._0.Model;

namespace Front.Models
{
    public class TimeViewModel
    {
        [DisplayName("Id")]
        public int IdTime { get; set; }

        [DisplayName("Waste")]
        public int IdWaste { get; set; }
        public Waste Waste { get; set; }
        public int Processduration { get; set; }

        [DisplayName("ProcessStage")]
        public int IdProcessStage { get; set; }
        public ProcessStage ProcessStage { get; set; }
        public bool IsDelete { get; set; }
    }
}