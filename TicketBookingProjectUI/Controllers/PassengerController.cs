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
        public async Task<IActionResult> Passenger(PassengerDto passengerDto, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                //check if image is provided 
                if (Image != null && Image.Length > 0)
                {
                    // Read the image data into a byte array
                    using (var stream = new MemoryStream())
                    {
                        await Image.CopyToAsync(stream);
                        // Convert the byte array to a Base64 string
                        passengerDto.Image = Convert.ToBase64String(stream.ToArray());
                    }
                }

                // Now, you can pass passengerDto to your ApiService
                var result = await _apiService.PassengerAsync(passengerDto);

                if (result.IsSuccessStatusCode)
                {
                  var responseContent=  await result.Content.ReadAsStringAsync();
                  var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    var PassengerId = responseObject.passengerId;
                    ViewBag.ResponseData = PassengerId;
                }
                else
                {

                }
                return View();
            }
            return View(passengerDto);
        }

        public IActionResult GetPassengerDetails()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetPassengerDetails(PassengerDetails passengerDetails)
        {
            int passengerID = passengerDetails.P_Id;

            // Call the Web API to get passenger details along with image data
            var response = await _apiService.GetPassengerAsync(passengerID);

            if (!string.IsNullOrEmpty(response))
            {
                // Deserialize the response JSON string
                var responseObject = JsonConvert.DeserializeObject<dynamic>(response);

                // Extract passenger details and image data from the response
                var passengerDetail = JsonConvert.DeserializeObject<PassengerDetails>(responseObject.passenger.ToString());
                var imageData = Convert.FromBase64String(responseObject.imagedata.ToString());

                var passengerDto = new PassengerDto
                {
                    P_Id = passengerDetail.P_Id,
                    Passenger_Name = passengerDetail.Passenger_Name,
                };

                var viewModel = new PassengerViewModel
                {
                    PassengerDetails = passengerDetail,
                    PassengerDto = passengerDto,
                    ImageData = imageData // Add image data to the view model
                };

                // Pass the view model to the view
                return View("Passenger", viewModel);
            }
            else
            {
                // Handle error response or no data available
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassenger(int passengerId, PassengerDto passengerDto, IFormFile PassengerImage)
        {
            try
            {
                // Check if an image file was uploaded
                if (PassengerImage != null && PassengerImage.Length > 0)
                {       
                    // Convert the image to a Base64 string
                    using (var memoryStream = new MemoryStream())
                    {
                        await PassengerImage.CopyToAsync(memoryStream);
                        passengerDto.Image = Convert.ToBase64String(memoryStream.ToArray());
                    }
                }

                // Call the API service to update the passenger
                var success = await _apiService.UpdatePassengerAsync(passengerId, passengerDto);

                if (success)
                {
                    // Set success message in TempData
                    TempData["SuccessMessage"] = "Passenger updated successfully.";
                    // If the update is successful, return a success view
                    return RedirectToAction("Passenger", "Passenger");
                }
                else
                {
                    // If the update fails, return an error view
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error updating passenger: {ex.Message}");
                return View("Error");
            }
        }


    }
}

