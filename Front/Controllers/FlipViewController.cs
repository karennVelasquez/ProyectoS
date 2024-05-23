using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.Text;
namespace Front.Controllers
{
    public class FlipViewController : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public FlipViewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult FlipGet()
        {
            List<FlipViewModel> Loginlist = new List<FlipViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Flip").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<FlipViewModel>>(data);

                List<Waste> wastes = GetWastes();
                List<WasteType> wasteTypes = GetWasteTypes();

                foreach (var chemicalComposition in Loginlist)
                {
                    var wasteInfo = wastes.FirstOrDefault(ch => ch.IdWaste == chemicalComposition.IdWaste);
                    var wasteTypeInfo = wasteTypes.FirstOrDefault(ch => ch.IdWasteType == chemicalComposition.IdWasteType);

                    if (wasteInfo != null || wasteTypeInfo != null)
                    {
                        var waste_type = wasteTypes.FirstOrDefault(wt => wt.IdWasteType == wasteInfo.IdWasteType);

                        if (waste_type != null)
                        {
                            chemicalComposition.Waste_Type = waste_type.Waste_Type;
                            chemicalComposition.Description = waste_type.Description;
                            chemicalComposition.Descomposition = waste_type.Descomposition;
                        }
                    }
                }
            }
            var inactiveLogins = Loginlist.Where(login => !login.IsDelete).ToList();

            return View(inactiveLogins);
        }

        ///
        private List<Waste> GetWastes()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Waste").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Waste>>(data);
            }
            return new List<Waste>();
        }

        ///
        private List<WasteType> GetWasteTypes()
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
        public IActionResult Create(FlipViewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Flip/Create?IdWaste={model.IdWaste}&Flipfrequency={model.Flipfrequency}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Flip Created";
                    return RedirectToAction("FlipGet");
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
                FlipViewModel login = new FlipViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Flip/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<FlipViewModel>(data);
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
        public IActionResult Update(FlipViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Flip/Update/{model.IdFlip}?IdWaste={model.IdWaste}&Flipfrequency={model.Flipfrequency}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Flip details updated";
                    return RedirectToAction("FlipGet");
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
                FlipViewModel login = new FlipViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Flip/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<FlipViewModel>(data);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Flip/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Flip details deleted";
                    return RedirectToAction("FlipGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("FlipGet");
        }
    }
}