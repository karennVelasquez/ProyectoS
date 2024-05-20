using Azure;
using Front.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGRA2._0.Model;
using System.Text;

namespace Front.Controllers
{
    public class AchievementsViewController : Controller
    {
        Uri baseAddress = new Uri("http://sistemagestionresiduosagricolas.somee.com/api");
        private readonly HttpClient _client;

        public AchievementsViewController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult AchievementsGet()
        {
            List<AchievementsViewModel> Loginlist = new List<AchievementsViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Achievements").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Loginlist = JsonConvert.DeserializeObject<List<AchievementsViewModel>>(data);

                // Obtener datos adicionales
                List<User> users = GetUsers();
                List<Games> games = GetGames();
                List<Level> levels = GetLevels();

                // Mapear datos relacionados
                foreach (var achievement in Loginlist)
                {
                    var user = users.FirstOrDefault(u => u.IdUser == achievement.IdUser);
                    var game = games.FirstOrDefault(g => g.IdGames == achievement.IdGames);
                    var level = levels.FirstOrDefault(l => l.IdLevel == game?.IdLevel);

                    if (user != null)
                    {
                        achievement.UserName = user.UserName;
                    }

                    if (level != null)
                    {
                        achievement.NumLevel = level.NumLevel;
                    }
                }
            }

            var activeAchievements = Loginlist.Where(achievement => !achievement.IsDelete).ToList();

            return View(activeAchievements);
        }

        private List<User> GetUsers()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/User").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<User>>(data);
            }
            return new List<User>();
        }

        private List<Games> GetGames()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Games").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Games>>(data);
            }
            return new List<Games>();
        }

        private List<Level> GetLevels()
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Level").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<Level>>(data);
            }
            return new List<Level>();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AchievementsViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + $"/Achievements/Create?IdUser={model.IdUser}&IdGames={model.IdGames}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Achievement Created";
                    return RedirectToAction("AchievementsGet");
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
                AchievementsViewModel achievement = new AchievementsViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Achievements/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    achievement = JsonConvert.DeserializeObject<AchievementsViewModel>(data);
                }
                return View(achievement);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Update(AchievementsViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Achievements/Update/{model.IdAchievements}?IdUser={model.IdUser}&IdGames={model.IdGames}", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Achievement details updated";
                    return RedirectToAction("AchievementsGet");
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
                AchievementsViewModel achievement = new AchievementsViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Achievements/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    achievement = JsonConvert.DeserializeObject<AchievementsViewModel>(data);
                }
                return View(achievement);
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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + $"/Achievements/Delete/{id}", null).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Achievement details deleted";
                    return RedirectToAction("AchievementsGet");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View("AchievementsGet");
        }
    }
}
