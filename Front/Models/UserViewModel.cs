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
    public class UserViewModel()
    {
        [DisplayName("Id")]
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsDelete { get; set; }
        //
    }
}
