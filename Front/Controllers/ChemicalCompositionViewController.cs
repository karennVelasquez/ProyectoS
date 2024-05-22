using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.Text;
namespace Front.Controllers
{
    public class ChemicalCompositionViewController : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public ChemicalCompositionViewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult ChemicalCompositionGet()
        {
            List<ChemicalCompositionViewModel> Loginlist = new List<ChemicalCompositionViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/ChemicalComposition").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<ChemicalCompositionViewModel>>(data);

                List<Waste> waste = GetWaste();
                List<WasteType> wasteTypes = GetWasteType();

                foreach (var chemicalComposition in Loginlist)
                {
                    var wasteInfo = waste.FirstOrDefault(ch => ch.IdWaste == chemicalComposition.IdWaste);
                    var wasteTypeInfo = wasteTypes.FirstOrDefault(ch => ch.IdWasteType == chemicalComposition.IdWasteType); 

                    if(wasteInfo != null || wasteTypeInfo !=null)
                    {
                        var waste_type = wasteTypes.FirstOrDefault(wt => wt.IdWasteType == wasteInfo.IdWasteType);

                        if(waste_type != null)
                        {
                            chemicalComposition.Waste_Type = waste_type.Waste_Type;
                        }
                    }
                }
            }
            var inactiveLogins = Loginlist.Where(login => !login.IsDelete).ToList();

            return View(inactiveLogins);
        }

        ///
        private List<Waste> GetWaste()
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
        public IActionResult Create(ChemicalCompositionViewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/ChemicalComposition/Create?IdWaste={model.IdWaste}&Chemical_Composition={model.Chemical_Composition}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "ChemicalComposition Created";
                    return RedirectToAction("ChemicalCompositionGet");
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
                ChemicalCompositionViewModel login = new ChemicalCompositionViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/ChemicalComposition/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<ChemicalCompositionViewModel>(data);
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
        public IActionResult Update(ChemicalCompositionViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/ChemicalComposition/Update/{model.IdChemicalComposition}?IdWaste={model.IdWaste}&Chemical_Composition={model.Chemical_Composition}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "ChemicalComposition details updated";
                    return RedirectToAction("ChemicalCompositionGet");
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
                ChemicalCompositionViewModel login = new ChemicalCompositionViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/ChemicalComposition/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<ChemicalCompositionViewModel>(data);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/ChemicalComposition/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "ChemicalComposition details deleted";
                    return RedirectToAction("ChemicalCompositionGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("ChemicalCompositionGet");
        }
    }
}