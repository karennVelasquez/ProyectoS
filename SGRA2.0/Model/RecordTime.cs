using System.ComponentModel.DataAnnotations;

namespace SGRA2._0.Model
{
    public class RecordTime
    {
        //TiempoRecord
        [Key]
        public int IdRecordTime { get; set; }
        public required int IdLevel { get; set; }
       // public required Level Level { get; set; }
        public required int IdWaste { get; set; }
        //public required Waste Waste { get; set; }
        public required DateTime Collecttime { get; set; }
        public required int AmountCollected { get; set; }
    }
}
