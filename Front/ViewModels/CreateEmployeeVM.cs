using Front.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Front.ViewModels
{
    public class CreateEmployeeVM
    {
        public EmployeeVM EmployeeModel { get; set; }
        public PersonVM PersonModel { get; set; }
        public DocumentTypeVM DocumentTypeModel { get; set; }
        public IEnumerable<SelectListItem> DocumentTypes { get; set; }
    }
}
