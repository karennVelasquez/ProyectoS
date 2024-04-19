using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class ProcessStage
    {
        //EtapaProceso
        [Key]
        public int IdProcessStage { get; set; }
        public required string Stage { get; set; }
    }
}
