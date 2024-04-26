using Microsoft.AspNetCore.Mvc;
using TicketBookingProjectUI.ConstantFile;
using TicketBookingProjectUI.Models;
using TicketBookingProjectUI.ConstantFile;

namespace TicketBookingProjectUI.Controllers
{
    public class SignController : Controller
    {
        private readonly ApiService _apiService;

        public SignController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
               var token= await _apiService.AuthenticateAsync(model);
                if (token != null)
                {
                    // Authentication successful, store the token securely (e.g., in a cookie)
                    // For demonstration purposes, let's store it in TempData
                    TempData["Token"] = token;

                    // Redirect authenticated user to dashboard or home page
                    return RedirectToAction("Passenger", "Passenger");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }
            return View(model);
        }
    }
}
