using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SGRA2._0.Model;

namespace Front.Models
{
    public class TimeViewModel
    {
        public TimeViewModel()
        {
            Waste = new List<SelectListItem>();
            WasteType = new List<SelectListItem>();
            ProcessStage = new List<SelectListItem>();
        }
        [DisplayName("Id")]
        public int IdTime { get; set; }

        public int IdWaste { get; set; }
        public IEnumerable<SelectListItem> Waste { get; set; }
        public int IdWasteType { get; set; }
        public IEnumerable<SelectListItem> WasteType { get; set; }
        [DisplayName("Waste Type")]
        public string Waste_Type { get; set; }


        public int IdProcessStage { get; set; }
        public IEnumerable<SelectListItem> ProcessStage { get; set; }
        [DisplayName("Process Stage")]
        public string Stage { get; set; }

        public bool IsDelete { get; set; }
    }
}