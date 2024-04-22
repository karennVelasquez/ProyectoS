using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class ChemicalComposition
    {
        //ComposicionQuimica
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdChemicalComposition { get; set; }
        public required int IdWaste { get; set; }
    //    public required Waste Waste { get; set; }
        public required string Chemical_Composition { get; set; }
        public bool IsDeleted {  get; set; }
        public DateTime Date { get; set; }
    }
}
