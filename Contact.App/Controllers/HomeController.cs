using Microsoft.AspNetCore.Mvc;

namespace Contact.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
