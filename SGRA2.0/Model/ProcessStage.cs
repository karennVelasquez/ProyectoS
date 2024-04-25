using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class ProcessStage
    {
        //EtapaProceso
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProcessStage { get; set; }
        public required string Stage { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}
