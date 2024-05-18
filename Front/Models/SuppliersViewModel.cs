using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
    public class SuppliersViewModel
    {
        [DisplayName("Id")]
        public int IdSuppliers { get; set; }
        [DisplayName("IdSuppliers")]
        public int IdPerson { get; set; }
        public PersonViewModel Person { get; set; }
        public int IdWasteType { get; set; }
        public WasteTypeViewModel WasteType { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}
