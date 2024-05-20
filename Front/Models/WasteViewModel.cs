using SGRA2._0.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Front.Models
{
    public class WasteViewModel
    {
        [DisplayName("Id")]
        public int IdWaste { get; set; }

        [DisplayName("Waste Type")]
        public int IdWasteType { get; set; }
        public WasteType WasteType { get; set; }
        public bool IsDelete { get; set; }
    }
}
