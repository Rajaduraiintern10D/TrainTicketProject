using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims; // Add this line
using TicketBookingProjectUI.ConstantFile;
using TicketBookingProjectUI.Models;


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
            // Check if the user is already authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Redirect to the appropriate logged-in page
                return RedirectToAction("Passenger", "Passenger");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await _apiService.AuthenticateAsync(model);
                if (!string.IsNullOrEmpty(token))
                {
                    // Store the token in session
                    HttpContext.Session.SetString("JWTToken", token);

                    // Sign in the user using cookie authentication
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.User_Name),
                
                // Add any additional claims as needed
            };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    /*var authProperties = new AuthenticationProperties
                    {
                        *//*// You can customize the authentication properties if needed
                        IsPersistent = false*//*
                    };*/

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity)/*,
                            authProperties*/);


                    return RedirectToAction("Passenger", "Passenger");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to a page after logout (optional)
            return RedirectToAction("Index", "Sign");
        }


    }
}
