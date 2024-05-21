using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.Text;
namespace Front.Controllers
{
    public class WasteViewController : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public WasteViewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult WasteGet()
        {
            List<WasteViewModel> Loginlist = new List<WasteViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Waste").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<WasteViewModel>>(data);

                List<WasteType> wasteTypes = GetWasteType();

                foreach (var waste in Loginlist)
                {
                    waste.Waste_Type = wasteTypes.FirstOrDefault(wt => wt.IdWasteType == waste.IdWasteType)?.Waste_Type;
                }
        
            }
            var inactiveLogins = Loginlist.Where(login => !login.IsDelete).ToList();

            return View(inactiveLogins);
        }

        ///
        private List<WasteType> GetWasteType()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/WasteType").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<WasteType>>(data);
            }
            return new List<WasteType>();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(WasteViewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Waste/Create?IdWasteType={model.IdWasteType}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Waste Created";
                    return RedirectToAction("WasteGet");
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
                WasteViewModel login = new WasteViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Waste/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<WasteViewModel>(data);
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
        public IActionResult Update(WasteViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Waste/Update/{model.IdWaste}?IdWasteType={model.IdWasteType}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Waste details updated";
                    return RedirectToAction("WasteGet");
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
                WasteViewModel login = new WasteViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Waste/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<WasteViewModel>(data);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Waste/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Waste details deleted";
                    return RedirectToAction("WasteGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("WasteGet");
        }
    }
}


