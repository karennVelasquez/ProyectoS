using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
namespace Front.Controllers
{
    public class TemperatureViewController : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public TemperatureViewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult TemperatureGet()
        {
            List<TemperatureViewModel> Loginlist = new List<TemperatureViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Temperature").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<TemperatureViewModel>>(data);
            }
            var inactiveLogins = Loginlist.Where(login => !login.IsDelete).ToList();

            return View(inactiveLogins);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TemperatureViewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Temperature/Create?IdWaste={model.IdWaste}&Decompositiontemperature={model.Decompositiontemperature}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Temperature Created";
                    return RedirectToAction("TemperatureGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
            {
                TemperatureViewModel login = new TemperatureViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Temperature/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<TemperatureViewModel>(data);
                }
                return View(login);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Update(TemperatureViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Temperature/Update/{model.IdTemperature}?IdWaste={model.IdWaste}&Decompositiontemperature={model.Decompositiontemperature}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Temperature details updated";
                    return RedirectToAction("TemperatureGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                TemperatureViewModel login = new TemperatureViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Temperature/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<TemperatureViewModel>(data);
                }
                return View(login);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Temperature/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Temperature details deleted";
                    return RedirectToAction("TemperatureGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("TemperatureGet");
        }
    }
}