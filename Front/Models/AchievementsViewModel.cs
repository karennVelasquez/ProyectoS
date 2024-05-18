using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.ComponentModel;
using System.Text;

namespace Front.Models
{
    public class AchievementsViewModel
    {
        [DisplayName("Id")]
        public int IdAchievements { get; set; }
        [DisplayName("IdAchievements")]
        public int IdUser { get; set; }
        public User User { get; set; }
        public int IdGames { get; set; }
        public Games Games { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Date { get; set; }
    }
}