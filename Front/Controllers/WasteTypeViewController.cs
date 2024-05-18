using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
namespace Front.Controllers
{
    public class WasteTypeViewController : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public WasteTypeViewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult WasteTypeGet()
        {
            List<WasteTypeViewModel> Loginlist = new List<WasteTypeViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/WasteType").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<WasteTypeViewModel>>(data);
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
        public IActionResult Create(WasteTypeViewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/WasteType/Create?Waste_Type={model.Waste_Type}&Description={model.Description}&Descomposition={model.Descomposition}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "WasteType Created";
                    return RedirectToAction("WasteTypeGet");
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
                WasteTypeViewModel login = new WasteTypeViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/WasteType/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<WasteTypeViewModel>(data);
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
        public IActionResult Update(WasteTypeViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/WasteType/Update/{model.IdWasteType}?Waste_Type={model.Waste_Type}&Description={model.Description}&Descomposition={model.Descomposition}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "WasteType details updated";
                    return RedirectToAction("WasteTypeGet");
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
                WasteTypeViewModel login = new WasteTypeViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/WasteType/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<WasteTypeViewModel>(data);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/WasteType/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "WasteType details deleted";
                    return RedirectToAction("WasteTypeGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("WasteTypeGet");
        }
    }
}
 
