using Microsoft.AspNetCore.Mvc.Rendering;
using SGRA2._0.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Front.Models
{
    public class WasteViewModel
    {
        public WasteViewModel()
        {
            
            WasteType = new List<SelectListItem>();
        }

        [DisplayName("Id")]
        public int IdWaste { get; set; }

        public int IdWasteType { get; set; }
        public IEnumerable<SelectListItem> WasteType { get; set; }
        [DisplayName("Waste Type")]
        public string Waste_Type { get; set; }
        public string Description { get; set; }
        public string Descomposition { get; set; }
        public bool IsDelete { get; set; }
    }
}
