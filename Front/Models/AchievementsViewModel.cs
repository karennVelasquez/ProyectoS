using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.ComponentModel;
using System.Text;

namespace Front.Models
{
    public class AchievementsViewModel
    {
        public AchievementsViewModel()
        {
            User = new List<SelectListItem>();
            Games = new List<SelectListItem>();
            Level = new List<SelectListItem>();
        }
        [DisplayName("Id")]
        public int IdAchievements { get; set; }

   
        public int IdUser { get; set; }
        public IEnumerable<SelectListItem> User { get; set; }
        [DisplayName("User")]
        public string UserName { get; set; }

        public int IdGames { get; set; }
        public IEnumerable<SelectListItem> Games { get; set; }
        public int IdLevel { get; set; }
        public IEnumerable<SelectListItem> Level { get; set; }
        [DisplayName("Level")]
        public int NumLevel { get; set; }
        public bool IsDelete { get; set; }
    }
}