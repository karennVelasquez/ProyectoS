using Microsoft.AspNetCore.Mvc.Rendering;

namespace Front.ViewModels
{
    public class CreateSuppliersVM
    {
        public SuppliersVM SuppliersModel { get; set; }
        public PersonVM PersonModel { get; set; }
        public DocumentTypeVM DocumentTypeModel { get; set; }
        public IEnumerable<SelectListItem> DocumentTypes { get; set; }
        public WasteTypeVM WasteTypeModel { get; set; }
        public IEnumerable<SelectListItem> WasteTypes { get; set; }
    }
}
