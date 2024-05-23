using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Front.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using SGRA2._0.Model;

namespace Front.Models
{
    public class TimeViewModel
    {
        public TimeViewModel()
        {
            Wastes = new List<SelectListItem>();
            WasteTypes = new List<SelectListItem>();
            ProcessStages = new List<SelectListItem>();
        }
        [DisplayName("Id")]
        public int IdTime { get; set; }

        public int IdWaste { get; set; }
        public IEnumerable<SelectListItem> Wastes { get; set; }
        public int IdWasteType { get; set; }
        public IEnumerable<SelectListItem> WasteTypes { get; set; }
        [DisplayName("Waste Type")]
        public string Waste_Type { get; set; }
        public string Description { get; set; }
        public string Descomposition { get; set; }

        public int IdProcessStage { get; set; }
        public IEnumerable<SelectListItem> ProcessStages { get; set; }
        public string Stage { get; set; }

        public bool IsDelete { get; set; }
    }
}