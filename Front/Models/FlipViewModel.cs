using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SGRA2._0.Model;

namespace Front.Models
{
    public class FlipViewModel
    {
        public FlipViewModel()
        {
            Waste = new List<SelectListItem>();
            WasteType = new List<SelectListItem>();
        }
        [DisplayName("Id")]
        public int IdFlip { get; set; }

        public int IdWaste { get; set; }
        public IEnumerable<SelectListItem> Waste { get; set; }
        public int IdWasteType { get; set; }
        public IEnumerable<SelectListItem> WasteType { get; set; }
        [DisplayName("Waste Type")]
        public string Waste_Type { get; set; }
        public string Description { get; set; }
        public string Descomposition { get; set; }


        [DisplayName("Flip Frequency")]
        public int Flipfrequency { get; set; }
        public bool IsDelete { get; set; }
    }
}