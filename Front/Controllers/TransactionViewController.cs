using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.Text;
namespace Front.Controllers
{
    public class TransactionViewController : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public TransactionViewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult TransactionGet()
        {
            List<TransactionViewModel> Loginlist = new List<TransactionViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Transaction").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<TransactionViewModel>>(data);

                List<DocumentType> documentTypes = GetDocumentType();
                List<Suppliers> suppliers = GetSuppliers();
                List<Person> persons = GetPerson();
   

                foreach (var transaction in Loginlist)
                {
                    var suppliersInfo = suppliers.FirstOrDefault(p => p.IdSuppliers == transaction.IdSuppliers);
                    var personInfo = persons.FirstOrDefault(p => p.IdPerson == suppliersInfo.IdPerson);

                    if (suppliersInfo != null || personInfo != null)
                    {
                        var name = persons.FirstOrDefault(ti => ti.IdPerson == suppliersInfo.IdPerson);
                        var lastname = persons.FirstOrDefault(ti => ti.IdPerson == suppliersInfo.IdPerson);
                        var email = persons.FirstOrDefault(ti => ti.IdPerson == suppliersInfo.IdPerson);
                        var numDocument = persons.FirstOrDefault(ti => ti.IdPerson == suppliersInfo.IdPerson);

                        var documentType = documentTypes.FirstOrDefault(c => c.IdDocumentType == personInfo.IdDocumentType);


                        if (name != null || lastname != null || email != null || numDocument != null || documentType != null)
                        {
                            transaction.Name = name.Name;
                            transaction.Lastname = lastname.Lastname;
                            transaction.Email = email.Email;
                            transaction.NumDocument = numDocument.NumDocument;
                            transaction.Document = documentType.Document;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TransactionViewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Transaction/Create?IdSuppliers={model.IdSuppliers}&DeliveredQuantity={model.DeliveredQuantity}&DeliveredDate={model.DeliveredDate}&Price={model.Price}&Quality{model.Quality}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Transaction Created";
                    return RedirectToAction("TransactionGet");
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
                TransactionViewModel login = new TransactionViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Transaction/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<TransactionViewModel>(data);
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
        public IActionResult Update(TransactionViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Transaction/Update/{model.IdTransaction}?IdSuppliers={model.IdSuppliers}&DeliveredQuantity={model.DeliveredQuantity}&DeliveredDate={model.DeliveredDate}&Price={model.Price}&Quality{model.Quality}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Transaction details updated";
                    return RedirectToAction("TransactionGet");
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
                TransactionViewModel login = new TransactionViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Transaction/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<TransactionViewModel>(data);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Transaction/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Transaction details deleted";
                    return RedirectToAction("TransactionGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("TransactionGet");
        }
    }
}