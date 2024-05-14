using Microsoft.AspNetCore.Mvc;
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
        #region GetTraindate
        [HttpGet]
        public async Task<IActionResult> GetTrainDate(DateTime fromDate, DateTime toDate)
        {
            try
            {
                string response = await _apiService.SendDateRangeData(fromDate, toDate);
                ViewBag.TrainDateResponseData = response;
                return View("TrainDetails");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
                return PartialView("ErrorPartialView");
            }
        }
        #endregion

        #region GetSeatAvilability
        [HttpGet]
        public async Task<IActionResult> GetSeatAvilability(int trainnumber)
        {
            try
            {
                string response = await _apiService.GetSeatAvailabilityAsync(trainnumber);
                ViewBag.SeatAvailabilityResponseData = response;
                return View("TrainDetails");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
                return PartialView("ErrorPartialView");
            }
        }
        #endregion 

        #region GetAllTrainDetails
        [HttpGet]
        public async Task<IActionResult> GetAllTrainDetails()
        {
            try
            {
                string response = await _apiService.GetAllTrainDetailsAsync();
                ViewBag.AllTrainDetailsResponseData = response;
                return View("TrainDetails");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
                return PartialView("ErrorPartialView");
            }
        }
        #endregion
    }
}
