using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
namespace Front.Controllers
{
    public class CollectWasteViewController : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public CollectWasteViewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult CollectWasteGet()
        {
            List<CollectWasteViewModel> Loginlist = new List<CollectWasteViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/CollectWaste").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<CollectWasteViewModel>>(data);
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
        public IActionResult Create(CollectWasteViewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/CollectWaste/Create?IdSuppliers={model.IdSuppliers}&IdComposter={model.IdComposter}&CollectionDate={model.CollectionDate}&Amount={model.Amount}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "CollectWaste Created";
                    return RedirectToAction("CollectWasteGet");
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
                CollectWasteViewModel login = new CollectWasteViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/CollectWaste/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<CollectWasteViewModel>(data);
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
        public IActionResult Update(CollectWasteViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/CollectWaste/Update/{model.IdCollectWaste}?IdSuppliers={model.IdSuppliers}&IdComposter={model.IdComposter}&CollectionDate={model.CollectionDate}&Amount={model.Amount}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "CollectWaste details updated";
                    return RedirectToAction("CollectWasteGet");
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
                CollectWasteViewModel login = new CollectWasteViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/CollectWaste/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<CollectWasteViewModel>(data);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/CollectWaste/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "CollectWaste details deleted";
                    return RedirectToAction("CollectWasteGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("CollectWasteGet");
        }
    }
}