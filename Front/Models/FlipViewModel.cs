using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SGRA2._0.Model;

namespace Front.Models
{
    public class FlipViewModel
    {
        [DisplayName("Id")]
        public int IdFlip { get; set; }
        [DisplayName("Flip")]
        public int IdWaste { get; set; }
        public Waste Waste { get; set; }
        public int Flipfrequency { get; set; }
        public bool IsDelete { get; set; }
    }
}