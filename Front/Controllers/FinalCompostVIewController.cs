using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
namespace Front.Controllers
{
    public class FinalCompostVIewController : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public FinalCompostVIewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult FinalCompostGet()
        {
            List<FinalCompostVIewModel> Loginlist = new List<FinalCompostVIewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/FinalCompost").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<FinalCompostVIewModel>>(data);
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
        public IActionResult Create(FinalCompostVIewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/FinalCompost/Create?IdWaste={model.IdWaste}&HumidityLevel={model.HumidityLevel}&FinalPh={model.FinalPh}&Nutrients={model.Nutrients}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "FinalCompost Created";
                    return RedirectToAction("FinalCompostGet");
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
                FinalCompostVIewModel login = new FinalCompostVIewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/FinalCompost/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<FinalCompostVIewModel>(data);
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
        public IActionResult Update(FinalCompostVIewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/FinalCompost/Update/{model.IdFinalCompost}?IdWaste={model.IdWaste}&HumidityLevel={model.HumidityLevel}&FinalPh={model.FinalPh}&Nutrients={model.Nutrients}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "FinalCompost details updated";
                    return RedirectToAction("FinalCompostGet");
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
                FinalCompostVIewModel login = new FinalCompostVIewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/FinalCompost/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<FinalCompostVIewModel>(data);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/FinalCompost/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "FinalCompost details deleted";
                    return RedirectToAction("FinalCompostGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("FinalCompostGet");
        }
    }
}