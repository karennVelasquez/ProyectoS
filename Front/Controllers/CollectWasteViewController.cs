using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Model;
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

                //Obtener datos adicionales

                List<DocumentType> documentTypes = GetDocumentType();
                List<Suppliers> suppliers = GetSuppliers();
                List<Person> persons = GetPerson();
                List<Composter> composter = GetComposter();

                foreach (var collectWaste in Loginlist)
                {
                    
                    collectWaste.Material = composter.FirstOrDefault(c => c.IdComposter == collectWaste.IdComposter)?.Material;
                    collectWaste.DrainageSystem = composter.FirstOrDefault(c => c.IdComposter == collectWaste.IdComposter)?.DrainageSystem;

                    var suppliersInfo = suppliers.FirstOrDefault(p => p.IdSuppliers == collectWaste.IdSuppliers);
                    var personInfo = persons.FirstOrDefault(p => p.IdPerson == suppliersInfo.IdPerson);
                    
                    if(suppliersInfo !=null || personInfo !=null)
                    {
                        var name = persons.FirstOrDefault(ti => ti.IdPerson == suppliersInfo.IdPerson);
                        var lastname = persons.FirstOrDefault(ti => ti.IdPerson == suppliersInfo.IdPerson);
                        var email = persons.FirstOrDefault(ti => ti.IdPerson == suppliersInfo.IdPerson);
                        var numDocument = persons.FirstOrDefault(ti => ti.IdPerson == suppliersInfo.IdPerson);

                        var documentType = documentTypes.FirstOrDefault(c => c.IdDocumentType == personInfo.IdDocumentType);


                        if (name != null || lastname != null || email != null || numDocument != null || documentType != null)
                        {
                            collectWaste.Name = name.Name;
                            collectWaste.Lastname = lastname.Lastname;
                            collectWaste.Email = email.Email;
                            collectWaste.NumDocument = numDocument.NumDocument;
                            collectWaste.Document = documentType.Document;
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
        private List<Suppliers> GetSuppliers()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Suppliers").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Suppliers>>(data);
            }
            return new List<Suppliers>();
        }

        ///
        private List<Composter> GetComposter()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Composter").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Composter>>(data);
            }
            return new List<Composter>();
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