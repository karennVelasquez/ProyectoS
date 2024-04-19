using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class ChemicalComposition
    {
        //ComposicionQuimica
        [Key]
        public int IdChemicalComposition { get; set; }
        public required int IdWaste { get; set; }
    //    public required Waste Waste { get; set; }
        public required string Chemical_Composition { get; set; }
    }
}
