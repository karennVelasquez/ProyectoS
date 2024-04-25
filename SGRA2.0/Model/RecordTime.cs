using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class RecordTime
    {
        //TiempoRecord
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRecordTime { get; set; }
        public int IdLevel { get; set; }
        public Level Level { get; set; }
        public int IdWaste { get; set; }
        public Waste Waste { get; set; }
        public DateTime Collecttime { get; set; }
        public int AmountCollected { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}
