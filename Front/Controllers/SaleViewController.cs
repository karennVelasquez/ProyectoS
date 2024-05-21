using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.Text;
namespace Front.Controllers
{
    public class SaleViewController : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public SaleViewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult SaleGet()
        {
            List<SaleViewModel> Loginlist = new List<SaleViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Sale").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<SaleViewModel>>(data);

                
               //Obtener datos adicionales
                List<Person> persons = GetPerson();
                List<Customer> customers = GetCustomer();

                // Mapear datos relacionados
                foreach (var sale in Loginlist)
                {
                    sale.Name = persons.FirstOrDefault(p => p.IdPerson == sale.IdPerson)?.Name;
                    sale.LastName = persons.FirstOrDefault(p => p.IdPerson == sale.IdPerson)?.Lastname;

                    var customerInfo = customers.FirstOrDefault(c => c.IdCustomer == sale.IdCustomer);
                    if(customerInfo != null)
                    {
                        var person = persons.FirstOrDefault(t => t.IdPerson == customerInfo.IdPerson);
                        if(person != null)
                        {
                            sale.Name = person.Name;
                            sale.LastName = person.Lastname;
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
        private List<Customer> GetCustomer()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Person").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Customer>>(data);
            }
            return new List<Customer>();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SaleViewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Sale/Create?IdCustomer={model.IdCustomer}&SaleDate={model.SaleDate}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Sale Created";
                    return RedirectToAction("SaleGet");
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
                SaleViewModel login = new SaleViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Sale/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<SaleViewModel>(data);
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
        public IActionResult Update(SaleViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Sale/Update/{model.IdSale}?IdCustomer={model.IdCustomer}&SaleDate={model.SaleDate}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Sale details updated";
                    return RedirectToAction("SaleGet");
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
                SaleViewModel login = new SaleViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Sale/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<SaleViewModel>(data);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Sale/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Sale details deleted";
                    return RedirectToAction("SaleGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("SaleGet");
        }
    }
}