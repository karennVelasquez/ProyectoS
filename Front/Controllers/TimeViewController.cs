using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class TimeViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
