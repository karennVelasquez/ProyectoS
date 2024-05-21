using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.Text;
namespace Front.Controllers
{
    public class TimeViewController : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public TimeViewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult TimeGet()
        {
            List<TimeViewModel> Loginlist = new List<TimeViewModel>();
            HttpResponseMessage respone = _client.GetAsync(_client.BaseAddress + "/Time").Result;
            if (respone.IsSuccessStatusCode)
            {
                string data = respone.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<TimeViewModel>>(data);

                //Obtener datos adicionales

                List<ProcessStage> processStages = GetProcessStage();

                //Mapear datos

                foreach (var time in Loginlist)
                {
                    time.Stage = processStages.FirstOrDefault(ti => ti.IdProcessStage == time.IdProcessStage)?.Stage;
                }
            }
            var inactiveLogins = Loginlist.Where(login => !login.IsDelete).ToList();

            return View(inactiveLogins);
        }

        //
        private List<ProcessStage> GetProcessStage()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/ProcessStage").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<ProcessStage>>(data);
            }
            return new List<ProcessStage>();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TimeViewModel model)
        {
            try
            {
                String data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Time/Create?IdWaste={model.IdWaste}&IdProcessStage={model.IdProcessStage}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Time Created";
                    return RedirectToAction("TimeGet");
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
                TimeViewModel login = new TimeViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Time/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<TimeViewModel>(data);
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
        public IActionResult Update(TimeViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Time/Update/{model.IdTime}?IdWaste={model.IdWaste}&IdProcessStage={model.IdProcessStage}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Time details updated";
                    return RedirectToAction("TimeGet");
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
                TimeViewModel login = new TimeViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Time/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    login = JsonConvert.DeserializeObject<TimeViewModel>(data);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Time/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Time details deleted";
                    return RedirectToAction("TimeGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("TimeGet");
        }
    }
}