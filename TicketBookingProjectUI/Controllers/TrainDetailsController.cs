using Microsoft.AspNetCore.Mvc;
using TicketBookingProject.Data.Models;
using TicketBookingProject.Models;
using TicketBookingProjectUI.ConstantFile;

namespace TicketBookingProjectUI.Controllers
{
    public class TrainDetailsController : Controller
    {
        private readonly ApiService _apiService;
        public TrainDetailsController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public IActionResult Index()
        {
            return View("TrainDetails");
        }
        [HttpGet]
        public async Task<IActionResult> GetTrainDate(DateTime fromDate, DateTime todate)
        {
            try {
                string response = await _apiService.SendDateRangeData(fromDate, todate);
              
                return View("TrainDetailsPartialView",response);
            }
            catch (Exception ex) {
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
                return PartialView("ErrorPartialView");
            }
            return View("Index");
        }

    }
}
