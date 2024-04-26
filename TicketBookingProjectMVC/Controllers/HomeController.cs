using Microsoft.AspNetCore.Mvc;

namespace TicketBookingProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
