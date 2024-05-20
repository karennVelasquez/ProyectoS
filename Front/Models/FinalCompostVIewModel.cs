using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SGRA2._0.Model;

namespace Front.Models
{
    public class FinalCompostVIewModel
    {
        [DisplayName("Id")]
        public int IdFinalCompost { get; set; }

        [DisplayName("Waste")]
        public int IdWaste { get; set; }
        public Waste Waste { get; set; }
        public string HumidityLevel { get; set; }
        //Nivel de humedad
        public string FinalPh { get; set; }
        public string Nutrients { get; set; }
        public bool IsDelete { get; set; }
    }
}