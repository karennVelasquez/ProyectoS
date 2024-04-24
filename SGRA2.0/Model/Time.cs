using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class Time
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTime { get; set; }
        public  int IdWaste { get; set; }
        public Waste Waste { get; set; }
        public int Processduration { get; set; }
        public  int IdProcessStage { get; set; }
        public ProcessStage ProcessStage { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}
