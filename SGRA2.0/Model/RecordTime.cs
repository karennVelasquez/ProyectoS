using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGRA2._0.Model
{
    public class RecordTime
    {
        //TiempoRecord
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRecordTime { get; set; }
        public required int IdLevel { get; set; }
        public required Level Level { get; set; }
        public required int IdWaste { get; set; }
        public required Waste Waste { get; set; }
        public required DateTime Collecttime { get; set; }
        public required int AmountCollected { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}
