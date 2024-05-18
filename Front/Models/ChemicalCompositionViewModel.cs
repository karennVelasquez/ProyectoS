using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SGRA2._0.Model;

namespace Front.Models
{
    public class ChemicalCompositionViewModel
    {
        [DisplayName("Id")]
        public int IdChemicalComposition { get; set; }
        [DisplayName("ChemicalComposition")]
        public int IdWaste { get; set; }
        public Waste Waste { get; set; }
        public string Chemical_Composition { get; set; }
        public bool IsDelete { get; set; }
    }
}