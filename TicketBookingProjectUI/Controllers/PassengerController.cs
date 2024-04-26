using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketBookingProject.Data.Dto;
using TicketBookingProject.Models;
using TicketBookingProjectUI.ConstantFile;
using TicketBookingProjectUI.Models;

namespace TicketBookingProjectUI.Controllers
{
    public class PassengerController : Controller
    {
        private readonly ApiService _apiService;
        public PassengerController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public IActionResult Passenger()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Passenger(PassengerDto passenger)
        {
            if (ModelState.IsValid)
            {
                var result = await _apiService.PassengerAsync(passenger);

                if (result.IsSuccessStatusCode)
                {
                    // Handle successful response (status code 200-299)
                    // You can access response content using result.Content if needed
                }
                else
                {
                    // Handle error response (status code other than 200-299)
                    // You can access status code, reason phrase, and content using result.StatusCode, result.ReasonPhrase, and result.Content if needed
                }
            }
             return View(passenger);
        }
           
            public IActionResult GetPassengerDetails()
            {
                return View();
            }
            [HttpGet]
            public async Task<IActionResult> GetPassengerDetails(PassengerDetails passengerDetails)
            {
                int passengerID=passengerDetails.P_Id;

                var jsonSting = await _apiService.GetPassengerAsync(passengerID);

                if (!string.IsNullOrEmpty(jsonSting))
                {
                   
                var passengerDetail= JsonConvert.DeserializeObject<PassengerDetails>(jsonSting);
                var passengerDto=new PassengerDto
                {
                    P_Id = passengerDetail.P_Id,
                    Passenger_Name = passengerDetail.Passenger_Name,
                };
                var viewModel = new PassengerViewModel
                {
                    PassengerDetails = passengerDetail,
                    PassengerDto = passengerDto
                };
                //pass the passengerDetail object to the view
                return View("Passenger", viewModel);
                }
                else
                {
                    // Handle error response or no data available
                    return View("Error");
                }
            }
    }
}

