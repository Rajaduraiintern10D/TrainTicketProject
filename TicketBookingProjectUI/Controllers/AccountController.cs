using Microsoft.AspNetCore.Mvc;
using TicketBookingProjectUI.ConstantFile;
using TicketBookingProjectUI.Models;

namespace TicketBookingProjectUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiService _apiService;
        public AccountController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {

            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        #region Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser registerUser)
        {
            if (ModelState.IsValid)
            {
                if (await _apiService.ResgisterAsync(registerUser))
                {
                    TempData["SuccessMessage"] = "User Registered Successfully";

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update password. Please try again.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid model state.");
            }
            // Redirect to Index action of SignController
            return RedirectToAction("Index", "Sign");
        }
        #endregion

        #region ForgotPassword
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _apiService.ForgetPasswordAsync(model))
                {
                    TempData["SuccessMessage"] = "Password Updated Successfully";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update password. Please try again.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid model state.");
            }

            // Redirect to Index action of SignController
            return RedirectToAction("Index", "Sign");
        }
        #endregion
    }
}