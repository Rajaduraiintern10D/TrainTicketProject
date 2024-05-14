using Microsoft.AspNetCore.Mvc;
using TicketBookingProjectUI.ConstantFile;

namespace TicketBookingProjectUI.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly ApiService _apiService;
        public DashBoardController(ApiService apiService)
        {
            _apiService = apiService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Call the GetDataAsync method from the ApiService
            var jsonString = await _apiService.GetDataAsync();

            if (!string.IsNullOrEmpty(jsonString))
            {
                // Pass the retrieved JSON string to the view
                return View("Dashboard", jsonString);
            }
            else
            {
                // Handle error response or no data available
                return View("Error");
            }
        }


    }
}
