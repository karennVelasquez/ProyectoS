using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.Text;
namespace Front.Controllers
{
    public class EmployeeViewController : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public EmployeeViewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult EmployeeGet()
        {
            List<EmployeeViewModel> Loginlist = new List<EmployeeViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Employee").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(data);

                //Obtener datos adicionales
                List<Person> person = GetPerson();
                List<DocumentType> documentTypes = GetDocumentType();

                //Mapear datos relacionados
                foreach (var employee in Loginlist)
                {
                    employee.Name = person.FirstOrDefault(p => p.IdPerson == employee.IdPerson)?.Name;
                    employee.LastName = person.FirstOrDefault(p => p.IdPerson == employee.IdPerson)?.Lastname;
                    employee.NumDocument = person.FirstOrDefault(ni => ni.IdPerson == employee.IdPerson).NumDocument;

                    var personInfo = person.FirstOrDefault(p => p.IdPerson == employee.IdPerson);
                    if (personInfo != null)
                    {
                        var documentType = documentTypes.FirstOrDefault(ti => ti.IdDocumentType == personInfo.IdDocumentType);
                        if (documentType != null)
                        {
                            employee.Document = documentType.Document;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Employee/Create?IdPerson={model.IdPerson}&Position={model.Position}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Employee Created";
                    return RedirectToAction("EmployeeGet");
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
                EmployeeViewModel login = new EmployeeViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Employee/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<EmployeeViewModel>(data);
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
        public IActionResult Update(EmployeeViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Employee/Update/{model.IdEmployee}?IdPerson={model.IdPerson}&Position={model.Position}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Employee details updated";
                    return RedirectToAction("EmployeeGet");
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
                EmployeeViewModel login = new EmployeeViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Employee/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<EmployeeViewModel>(data);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Employee/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Employee details deleted";
                    return RedirectToAction("EmployeeGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("EmployeeGet");
        }
    }
}

