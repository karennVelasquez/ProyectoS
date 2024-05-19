using Microsoft.AspNetCore.Mvc;
using SGRA2._0.Context;
using SGRA2._0.Model;
using Microsoft.EntityFrameworkCore;
using Front.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FrontBerries.Controllers
{
    public class MainController : Controller
    {
        [HttpGet]
        public IActionResult MainPage()
        {
            return View();
        }
    }
}