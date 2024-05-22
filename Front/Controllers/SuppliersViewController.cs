using Azure;
using Front.Models;
using Front.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                List<DocumentType> documentTypes = GetDocumentTypes();
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

            CreateSuppliersVM createSuppliersVM = new CreateSuppliersVM
            {
                SuppliersModel = new SuppliersVM(),
                PersonModel = new PersonVM(),
                DocumentTypeModel = new DocumentTypeVM(),
                DocumentTypes = GetDocumentTypesSelectList(),

                WasteTypeModel = new WasteTypeVM(),
                WasteTypes = GetWasteTypesSelectList(),


            };
            return View(createSuppliersVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSuppliersVM createSuppliersVM)
        {
            if (!ModelState.IsValid)
            {
                createSuppliersVM.DocumentTypes = GetDocumentTypesSelectList();
                createSuppliersVM.WasteTypes = GetWasteTypesSelectList();
                return View(createSuppliersVM);
            }
            try
            {
                createSuppliersVM.DocumentTypes = GetDocumentTypesSelectList();

                int id = 5; //no se todavia
                int TI = 1;

                //crear tipode residuo
                var wasteTypeData = JsonConvert.SerializeObject(createSuppliersVM.WasteTypeModel);
                var wasteTypeContent = new StringContent(wasteTypeData, Encoding.UTF8, "application/json");
                var wasteTypeResponse = await _client.PostAsync(_client.BaseAddress + $"/WasteType?Waste_Type={createSuppliersVM.WasteTypeModel.Waste_Type}&Description={createSuppliersVM.WasteTypeModel.Description}&Descomposition={createSuppliersVM.WasteTypeModel.Descomposition}", wasteTypeContent);
                
                if(!wasteTypeResponse.IsSuccessStatusCode) 
                {
                    TempData["errorMessage"] = "Error creting person";
                    return View();
                }
                var wasteTypeResponseData = await wasteTypeResponse.Content.ReadAsStringAsync();
                var createWasteType = JsonConvert.DeserializeObject<WasteTypeViewModel>(wasteTypeResponseData);
                int wasteTypeId = createWasteType.IdWasteType;
    
                //crearpersona
                var personData = JsonConvert.SerializeObject(createSuppliersVM.PersonModel);
                var personContent = new StringContent(personData, Encoding.UTF8, "application/json");
                var personResponde = await _client.PostAsync(_client.BaseAddress + $"/Suppliers?Name={createSuppliersVM.PersonModel.Name}&LastName{createSuppliersVM.PersonModel.Lastname}" +
                    $"&Email={createSuppliersVM.PersonModel.Email}&IdDocumentType={TI}&NumDocument={createSuppliersVM.PersonModel.NumDocument}", personContent);

                if (!personResponde.IsSuccessStatusCode)
                {
                    TempData["errorMessage"] = "Error creting person";
                    return View();
                }
                var personResponseData = await personResponde.Content.ReadAsStringAsync();
                var createPerson = JsonConvert.DeserializeObject<PersonViewModel>(personResponseData);
                int personId = createPerson.IdPerson;

                //crearproveedor
                var suppliersData = JsonConvert.SerializeObject(createSuppliersVM.SuppliersModel);
                var suppliersContent = new StringContent(suppliersData, Encoding.UTF8, "application/json");
                var suppliersResponse = await _client.PostAsync(_client.BaseAddress + $"/Customer?IdPerson={personId}", suppliersContent);
                if (!suppliersResponse.IsSuccessStatusCode)
                {
                    TempData["errorMessage"] = "Error creating Suppliers";
                    return View();
                }

                TempData["successMessage"] = "Suppliers created successfully";
                return RedirectToAction("SuppliersGet");

                /*String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Suppliers/Create?IdPerson={model.IdPerson}&IdWasteType={model.IdWasteType}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Suppliers Created";
                    return RedirectToAction("SuppliersGet");
                }*/
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
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

        //
        private List<SelectListItem> GetWasteTypesSelectList()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/WasteType").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var typeIdentifications = JsonConvert.DeserializeObject<List<WasteType>>(data);
                return typeIdentifications.Select(ti => new SelectListItem
                {
                    Value = ti.IdWasteType.ToString(),
                    Text = ti.Waste_Type
                }).ToList();
            }
            return new List<SelectListItem>();
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

