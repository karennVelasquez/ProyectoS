using Front.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Context;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Front.Controllers
{
    public class AccesoSystemController : Controller
    {
        private readonly PersonDBContext _personDBContext;
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;
        public AccesoSystemController(PersonDBContext persondbContext)
        {
            _personDBContext = persondbContext;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        /*  [HttpGet]
          public IActionResult Registro()
          {
              return View();
          }

          [HttpPost]
          public async Task<IActionResult> Registro(PersonLoginWM model)
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
                      HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/PersonLogin/Create?UserName={model.UserName}&Password={model.Password}&IdPerson={model.IdPerson}", content);
                      if (response.IsSuccessStatusCode)
                      {
                          TempData["successMessage"] = "User Created";
                          return RedirectToAction("Login", "PersonLogin");
                      }
                  }
                  catch (Exception ex)
                  {
                      TempData["errorMessage"] = ex.Message;
                      return View();
                  }
                  return View();
              }
          }*/


        [HttpGet]
        public IActionResult LoginS()
        {
            //if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginS(PersonLoginWM model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(
                    _client.BaseAddress + $"/PersonLogin/Authentication?userName={model.UserName}&password={model.Password}", content);

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
                        return RedirectToAction("Index", "Home");
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

