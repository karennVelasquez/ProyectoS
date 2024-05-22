using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.Net;
using System.Text;
namespace Front.Controllers
{
    public class PersonViewController1 : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public PersonViewController1()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult PersonGet()
        {
            List<PersonViewModel> Loginlist = new List<PersonViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Person").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<PersonViewModel>>(data);

                //Obtener datos adicionales

                List<DocumentType> documentTypes = GetDocumentTypes();

                //Mapear datos

                foreach (var person in Loginlist)
                {
                    person.Document = documentTypes.FirstOrDefault(ti => ti.IdDocumentType == person.IdDocumentType)?.Document;
                }
            }
            var inactiveLogins = Loginlist.Where(login => !login.IsDelete).ToList();

            return View(inactiveLogins);
        }

        private List<DocumentType> GetDocumentTypes()
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
            var model = new PersonViewModel
            {

                DocumentTypes = GetDocumentTypesSelectList()
            };   
            return View();
        }
        [HttpPost]
        public IActionResult Create(PersonViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.DocumentTypes = GetDocumentTypesSelectList();

                return View(model);
            }
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Person/Create?Name={model.Name}&Lastname={model.Lastname}&Email={model.Email}&IdDocumentType={model.IdDocumentType}&NumDocument={model.NumDocument}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Person Created";
                    return RedirectToAction("PersonGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                //return View();
            }

            //
            model.DocumentTypes = GetDocumentTypesSelectList();
            return View();

        }

        //

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
                PersonViewModel login = new PersonViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Person/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<PersonViewModel>(data);
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
        public IActionResult Update(PersonViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Person/Update/{model.IdPerson}?Name={model.Name}&Lastname={model.Lastname}&Email={model.Email}&IdDocumentType={model.IdDocumentType}&NumDocument={model.NumDocument}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Person details updated";
                    return RedirectToAction("PersonGet");
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
                PersonViewModel login = new PersonViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Person/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<PersonViewModel>(data);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Person/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Person details deleted";
                    return RedirectToAction("PersonGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("PersonGet");
        }
    }
}

