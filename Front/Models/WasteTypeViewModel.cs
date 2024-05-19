using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Front.Models
{
    public class WasteTypeViewModel
    {
        [DisplayName("Id")]
        public int IdWasteType { get; set; }
        [DisplayName("Waste Type")]
        public string Waste_Type { get; set; }
        public string Description { get; set; }
        public string Descomposition { get; set; }
        public bool IsDelete { get; set; }
    }
}
