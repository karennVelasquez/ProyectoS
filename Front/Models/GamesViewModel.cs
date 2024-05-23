using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SGRA2._0.Model;

namespace Front.Models
{
    public class GamesViewModel
    {
        public GamesViewModel()
        {
            
            Level = new List<SelectListItem>();
        }
        [DisplayName("Id")]
        public int IdGames { get; set; }

        public int IdLevel { get; set; }
        public IEnumerable<SelectListItem> Level { get; set; }
        [DisplayName("Level")]
        public int NumLevel { get; set; }
        public bool IsDelete { get; set; }
    }
}