using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class Time
    {
        [Key]
        public int IdTime { get; set; }
        public required int IdWaste { get; set; }
        public Waste Waste { get; set; }
        public required int Processduration { get; set; }
        public required int IdProcessStage { get; set; }
        public ProcessStage EtapProcessStageaProceso { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Date { get; set; }
        public string ModifiedBy { get; set; }
    }
}
