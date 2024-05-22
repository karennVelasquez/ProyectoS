using Azure;
using Front.Models;
using Front.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
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
                List<DocumentType> documentTypes = GetDocumentTypes();

                //Mapear datos relacionados
                foreach (var customer in Loginlist)
                {
                    customer.Name = person.FirstOrDefault(p => p.IdPerson == customer.IdPerson)?.Name;
                    customer.LastName = person.FirstOrDefault(p => p.IdPerson == customer.IdPerson)?.Lastname;
                    customer.Email = person.FirstOrDefault(p => p.IdPerson == customer.IdPerson)?.Email;
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
        private List<DocumentType> GetDocumentTypes ()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/DocumentType").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<DocumentType>>(data);
            }
            return new List<DocumentType>();
        }


        //createcustomer
        [HttpGet]
        public IActionResult Create()
        {
            CreateCustomerVM createCustomerVM = new CreateCustomerVM
            {
                CustomerModel = new CustomerVM(),
                PersonModel = new PersonVM(),
                DocumentTypeModel = new DocumentTypeVM(),
                DocumentTypes = GetDocumentTypesSelectList(),

            };
            return View(createCustomerVM);
        }
        [HttpPost]
        public async Task<IActionResult>Create(CreateCustomerVM createCustomerVM)
        {
            if(!ModelState.IsValid)
            {
                createCustomerVM.DocumentTypes = GetDocumentTypesSelectList();
                return View(createCustomerVM);
            }

            try
            {
                createCustomerVM.DocumentTypes = GetDocumentTypesSelectList();

                int id = 5; //no se todavia
                int TI = 1;
                //crearpersona
                var personData = JsonConvert.SerializeObject(createCustomerVM.PersonModel);
                var personContent = new StringContent(personData, Encoding.UTF8, "application/json");
                var personResponde = await _client.PostAsync(_client.BaseAddress + $"/Customer?Name={createCustomerVM.PersonModel.Name}&LastName{createCustomerVM.PersonModel.Lastname}" +
                    $"&Email={createCustomerVM.PersonModel.Email}&IdDocumentType={TI}&NumDocument={createCustomerVM.PersonModel.NumDocument}", personContent);

                if(!personResponde.IsSuccessStatusCode) 
                {
                    TempData["errorMessage"] = "Error creting person";
                    return View();
                }
                var personResponseData = await personResponde.Content.ReadAsStringAsync();
                var createPerson = JsonConvert.DeserializeObject<PersonViewModel>(personResponseData);
                int personId = createPerson.IdPerson;

                //Crearcliente
                var customerData = JsonConvert.SerializeObject(createCustomerVM.CustomerModel);
                var customerContent = new StringContent(customerData, Encoding.UTF8, "application/json");
                var customerResponse = await _client.PostAsync(_client.BaseAddress + $"/Customer?IdPerson={personId}", customerContent);
                if(!customerResponse.IsSuccessStatusCode) 
                {
                    TempData["errorMessage"] = "Error creating customer";
                    return View();
                }

                TempData["successMessage"] = "Customer created successfully";
                return RedirectToAction("CustomerGet");
                
                /* String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Customer/Create?IdPerson={model.IdPerson}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Customer Created";
                    return RedirectToAction("CustomerGet");
                }*/
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        private List<SelectListItem> GetDocumentTypesSelectList()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/DocumentType").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var typeIdentifications = JsonConvert.DeserializeObject<List<DocumentType>>(data);
                return typeIdentifications.Select(ti => new SelectListItem
                {
                    Value = ti.IdDocumentType.ToString(),
                    Text = ti.Document
                }).ToList();
            }
            return new List<SelectListItem>();
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