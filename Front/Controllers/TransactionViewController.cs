using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class TransactionViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
