using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SGRA2._0.Model;

namespace Front.Models
{
    public class TemperatureViewModel
    {
        [DisplayName("Id")]
        public int IdTemperature { get; set; }

        [DisplayName("Waste")]
        public int IdWaste { get; set; }
        public Waste Waste { get; set; }
        public string Decompositiontemperature { get; set; }
        public bool IsDelete { get; set; }
    }
}