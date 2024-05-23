using Microsoft.AspNetCore.Mvc.Rendering;

namespace Front.ViewModels
{
    public class CreateChemicalVM
    {
        public ChemicalCompositionVM ChemicalCompositionModel { get; set; }
        public WasteVM WasteModel { get; set; }
        public WasteTypeVM WasteTypeModel { get; set; }
        public IEnumerable<SelectListItem> Wastes { get; set; }
    }
}
