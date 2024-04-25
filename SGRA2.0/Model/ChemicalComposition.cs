using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class ChemicalComposition
    {
        //ComposicionQuimica
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdChemicalComposition { get; set; }
        public int IdWaste { get; set; }
        public Waste Waste { get; set; }
        public string Chemical_Composition { get; set; }
        public bool IsDelete {  get; set; }
        public DateTime? Date { get; set; }
    }
}
