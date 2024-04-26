using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBookingProject.Data;
using TicketBookingProject.Data.Dto;
using TicketBookingProject.Data.Models;
using TicketBookingProject.IRepository;
using TicketBookingProject.repository;

namespace TicketBookingProject.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrainDetailsController : ControllerBase
    {
        private readonly ITrainDetailsRepository _trainingDetailsRepository;
        private readonly IMapper _mapper;
        public TrainDetailsController(ITrainDetailsRepository trainingDetailsRepository)
        {
            _trainingDetailsRepository = trainingDetailsRepository;
            

        }
        #region GetSeatAvailability
        [HttpGet("{trainNumber}/seatavailability")]
        public IActionResult GetSeatAvailability(int trainNumber)
        {
            var seatAvailabilityDto = _trainingDetailsRepository.GetSeatAvailability(trainNumber);

            if (seatAvailabilityDto == null)
            {
                return NotFound(); // Return 404 if data is  not found
            }

            return Ok(seatAvailabilityDto); // Return 200 with the DTO
        }
        #endregion
        #region GetAllTrains
        [HttpGet("GetAllTrains")]
        public IActionResult GetAllTrains()
        {
            IEnumerable<TrainDetailsDto> trainDtos = _trainingDetailsRepository.GetAllTrains();
            return Ok(trainDtos);
        }
        #endregion
        #region Get Available train
        [HttpGet()]    
        public IActionResult GetAvailableTrains(DateTime fromDate,DateTime? toDate=null)
        {
            var availableTrains = _trainingDetailsRepository.GetAvailableTrains(fromDate, toDate);

            if (availableTrains == null || !availableTrains.Any())
            {
                return NotFound("No trains available for the specified date range.");
            }

            return Ok(availableTrains);
        }
        #endregion
    }
}
