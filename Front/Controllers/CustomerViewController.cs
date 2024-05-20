using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.Text;
namespace Front.Controllers
{
    public class CustomerViewController : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public CustomerViewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult CustomerGet()
        {
            List<CustomerViewModel> Loginlist = new List<CustomerViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Customer").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<CustomerViewModel>>(data);

                //Obtener datos adicionales
                List<Person> person = GetPerson();
                List<DocumentType> documentTypes = GetDocumentType();

                //Mapear datos relacionados
                foreach (var customer in Loginlist)
                {
                    customer.Name = person.FirstOrDefault(p => p.IdPerson == customer.IdPerson)?.Name;
                    customer.LastName = person.FirstOrDefault(p => p.IdPerson == customer.IdPerson)?.Lastname;
                    customer.NumDocument = person.FirstOrDefault(ni => ni.IdPerson == customer.IdPerson).NumDocument;

                    var personInfo = person.FirstOrDefault(p => p.IdPerson == customer.IdPerson);
                    if(personInfo != null)
                    {
                        var documentType = documentTypes.FirstOrDefault(ti => ti.IdDocumentType == personInfo.IdDocumentType);
                        if(documentType != null)
                        {
                            customer.Document = documentType.Document;
                        }
                    }
                }
            }
            var inactiveLogins = Loginlist.Where(login => !login.IsDelete).ToList();

            return View(inactiveLogins);
        }

        ///
        private List<Person> GetPerson ()
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
        private List<DocumentType> GetDocumentType ()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/DocumentType").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<DocumentType>>(data);
            }
            return new List<DocumentType>();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CustomerViewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Customer/Create?IdPerson={model.IdPerson}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Customer Created";
                    return RedirectToAction("CustomerGet");
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
                CustomerViewModel login = new CustomerViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Customer/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<CustomerViewModel>(data);
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
        public IActionResult Update(CustomerViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Customer/Update/{model.IdCustomer}?IdPerson={model.IdPerson}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Customer details updated";
                    return RedirectToAction("CustomerGet");
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
                CustomerViewModel login = new CustomerViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Customer/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<CustomerViewModel>(data);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Customer/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Customer details deleted";
                    return RedirectToAction("CustomerGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("CustomerGet");
        }
    }
}