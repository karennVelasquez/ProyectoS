using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front.Controllers
{
    public class SuppliersViewController
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public SuppliersViewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult SuppliersGet()
        {
            List<SuppliersViewModel> Loginlist = new List<SuppliersViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Suppliers").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<SuppliersViewModel>>(data);
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
        public IActionResult Create(SuppliersViewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Suppliers/Create?Suppliers={model.Suppliers}&IdPerson={model.IdPerson}&Person={model.Person}&IdWasteType={model.IdWasteType}&WasteType={model.WasteType}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Suppliers Created";
                    return RedirectToAction("SuppliersGet");
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
                SuppliersViewModel login = new SuppliersViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Suppliers/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<SuppliersViewModel>(data);
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
        public IActionResult Update(SuppliersViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Suppliers/Update/{model.IdSuppliers}?Waste_Type={model.Waste_Type}&Description={model.Description}&Descomposition={model.Descomposition}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Suppliers details updated";
                    return RedirectToAction("SuppliersGet");
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
                SuppliersViewModel login = new SuppliersViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Suppliers/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<SuppliersViewModel>(data);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Suppliers/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Suppliers details deleted";
                    return RedirectToAction("SuppliersGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("SuppliersGet");
        }
    }
}