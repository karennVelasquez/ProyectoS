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


namespace Front.Controllers
{
    public class AccesoController : Controller
    {
        private readonly PersonDBContext _personDBContext;
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;
        public AccesoController(PersonDBContext persondbContext)
        {
            _personDBContext = persondbContext;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(UsuarioWM model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }
            else
            {
                try
                {
                    String data = JsonConvert.SerializeObject(model);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/User/Create?UserName={model.UserName}&Email={model.Email}&Password={model.Password}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "User Created";
                        return RedirectToAction("Login", "Acceso");
                    }
                }
                catch (Exception ex)
                {
                    TempData["errorMessage"] = ex.Message;
                    return View();
                }
                return View();
            }
        }


        [HttpGet]
        public IActionResult Login()
        {
            //if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserWM model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(
                    _client.BaseAddress + $"/User/Authentication?userName={model.UserName}&email={model.Email}&password={model.Password}", content);

                string token = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    if (token == "false")
                    {
                        ViewData["Mensaje"] = "No se encontraron coincidencias";
                        return View();
                    }
                    else
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, model.UserName),
                            new Claim("Token", token)
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties
                        );

                        TempData["successMessage"] = "Login successful";
                        TempData["responseBody"] = token;
                        return RedirectToAction("IndexGame", "Home");
                    }
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewData["Mensaje"] = "Invalid username or password.";
                    return View();
                }
                else
                {
                    ViewData["Mensaje"] = "Error: " + response.ReasonPhrase;
                    TempData["responseBody"] = token;
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}

