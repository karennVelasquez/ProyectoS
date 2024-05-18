using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using SGRA2._0.Model;

namespace Front.Models
{
    public class GamesViewModel
    {
        [DisplayName("Id")]
        public int IdGames { get; set; }
        [DisplayName("IdGames")]
        public int IdLevel { get; set; }
        public Level Level { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}