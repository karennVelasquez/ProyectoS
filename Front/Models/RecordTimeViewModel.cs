using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SGRA2._0.Model;

namespace Front.Models
{
    public class RecordTimeViewModel
    {
        [DisplayName("Id")]
        public int IdRecordTime { get; set; }
        [DisplayName("RecordTime")]
        public int IdLevel { get; set; }
        public Level Level { get; set; }
        public DateTime Collecttime { get; set; }
        public bool IsDelete { get; set; }
    }
}