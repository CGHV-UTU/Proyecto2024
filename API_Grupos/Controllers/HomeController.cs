using Microsoft.AspNetCore.Mvc;

namespace API_Grupos.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
