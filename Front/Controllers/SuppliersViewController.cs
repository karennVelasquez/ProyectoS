using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.Text;
namespace Front.Controllers
{
    public class SuppliersViewController : Controller
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

                //Obtener datos adicionales
                List<Person> person = GetPerson();
                List<DocumentType> documentTypes = GetDocumentType();
                List<WasteType> wasteTypes = GetWasteType();

                //Mapear datos relacionados
                foreach (var suppliers in Loginlist)
                {
                    suppliers.Name = person.FirstOrDefault(p => p.IdPerson == suppliers.IdPerson)?.Name;
                    suppliers.LastName = person.FirstOrDefault(p => p.IdPerson == suppliers.IdPerson)?.Lastname;
                    suppliers.NumDocument = person.FirstOrDefault(ni => ni.IdPerson == suppliers.IdPerson).NumDocument;
                    suppliers.Waste_Type = wasteTypes.FirstOrDefault(wt => wt.IdWasteType  == suppliers.IdWasteType)?.Waste_Type;

                    var wasteTypeInfo = wasteTypes.FirstOrDefault(wt => wt.IdWasteType == suppliers.IdPerson);
                    var personInfo = person.FirstOrDefault(p => p.IdPerson == suppliers.IdPerson);
                    if (personInfo != null)
                    {
                        var documentType = documentTypes.FirstOrDefault(ti => ti.IdDocumentType == personInfo.IdDocumentType);
                        if (documentType != null)
                        {
                            suppliers.Document = documentType.Document;
                        }
                    }
                }
            }
            var inactiveLogins = Loginlist.Where(login => !login.IsDelete).ToList();

            return View(inactiveLogins);
        }

        ///
        private List<Person> GetPerson()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Person").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Person>>(data);
            }
            return new List<Person>();
        }
        ///
        private List<DocumentType> GetDocumentType()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/DocumentType").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<DocumentType>>(data);
            }
            return new List<DocumentType>();
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
        public IActionResult Create(SuppliersViewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Suppliers/Create?IdPerson={model.IdPerson}&IdWasteType={model.IdWasteType}", content).Result;
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Suppliers/Update/{model.IdSuppliers}?IdPerson={model.IdPerson}&IdWasteType={model.IdWasteType}", content).Result;
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

