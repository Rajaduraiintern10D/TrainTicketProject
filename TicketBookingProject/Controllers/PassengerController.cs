using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBookingProject.Data.Dto;
using TicketBookingProject.Data.Models;
using TicketBookingProject.IRepository;
using TicketBookingProject.Models;
using TicketBookingProject.Shared_Folder;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PassengerController : ControllerBase
{
    private readonly IPassengerRepository _passengerRepository;
    private readonly ITrainDetailsRepository _trainDetailsRepository;
    private readonly IMapper _mapper;
    public PassengerController(IPassengerRepository passengerRepository,
        ITrainDetailsRepository trainDetailsRepository,
        IMapper mapper)
    {
        _passengerRepository = passengerRepository;
        _trainDetailsRepository = trainDetailsRepository;
        _mapper = mapper;
    }
    #region AddPassenger
    [HttpPost("AddPassenger")]
    public IActionResult AddPassenger([FromBody] PassengerDto passengerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var seatAvailability = _trainDetailsRepository.GetSeatAvailability(passengerDto.TrainNumber);
        if (seatAvailability == null)
        {
            return NotFound("Train not found");
        }
        var trainDetails = _trainDetailsRepository.GetTrainDetails(passengerDto.TrainNumber);
        if (trainDetails == null)
        {
            return NotFound("Train details not found");
        }
        //Fetch Fare From TrainWiseSeatavailabilities based on the provided class and train number
        decimal? fare = _trainDetailsRepository.GetFareByClassAndTrainNumber
           (passengerDto.TrainNumber, passengerDto.Class);
        if (fare == null)
        {
            return NotFound($"Fare not found for class: {passengerDto.Class} " +
                "and train number: {passengerRequest.TrainNumber}");
        }
        // Calculate total fare based on fare per ticket and total ticket count
        decimal totalFare = fare.Value * passengerDto.TotalTicketCount;

        //Map dto to Entity
        var passengerDetails=_mapper.Map<PassengerDetails>(passengerDto);
        

        //populate starting city and destination city from Traindetails
        passengerDetails.DepartureDate=trainDetails.DepartureDate;
        passengerDetails.DepartureTime = trainDetails.DepartureTime;
        passengerDetails.DestinationDate = trainDetails.DestinationDate;
        passengerDetails.DestinationTime = trainDetails.DestinationTime;
        passengerDetails.StartingCity=trainDetails.StartingCity;
        passengerDetails.DestinationCity=trainDetails.DestinationCity;
        passengerDetails.TotalFare= totalFare;
        passengerDetails.BookedTicketTime=DateTime.Now;

        int insertedPassenger = _passengerRepository.AddPassenger(passengerDetails);

        //Get last inserted passenger from the database
        var lastInsertedPassneger=_passengerRepository.GetPassengerByID(insertedPassenger);
        

        return Ok(new { PassengerDetails = lastInsertedPassneger, Message = "Passenger added successfully" });

    }
    #endregion
    #region GetPassengerById
    [HttpGet("GetPassengerById/{passengerId}")]
    public IActionResult GetPassengerById(int passengerId)
    {
        var passenger = _passengerRepository.GetPassengerByID(passengerId);
        if (passenger == null)
        {
            return NotFound($"Passenger with ID {passengerId} not found");
        }
        return Ok(passenger);
    }
    #endregion
    #region UpdatePassengerStatus
    [HttpPatch("Confirm the Status of the Ticket By - Cancelled|Pending|Confirmed")]
    public IActionResult UpdatePassengerStatus(int PassengerId, string status)
    {
        if (!Enum.TryParse(status, true, out GlobalConstantEnums passengerStatus))
        {
            return BadRequest("Invalid status.");
        }

        _passengerRepository.UpdatePassengerStatus(PassengerId, passengerStatus);
        return Ok("Passenger status updated successfully");
    }
    #endregion
    #region UpdatePassengerDetails
    [HttpPut("UpdatePassengerDetails/{passengerId}")]
    public IActionResult UpdatePassengerDetails(int passengerId, [FromBody] PassengerDto updatedPassenger)
        {
        if (passengerId <= 0 || updatedPassenger == null)
        {  
            return BadRequest("Invalid passenger ID or invalid data.");
        }
        try
        {
            var existingPassenger = _passengerRepository.GetPassengerByID(passengerId);
            if (existingPassenger == null)
            {
                return NotFound($"Passenger with ID {passengerId} not found.");
            }
            // Update passenger details
            _passengerRepository.UpdatePassenger(existingPassenger, updatedPassenger);
            //Fetch the complete updated passenger details
            var updatedpasssengerdetails=_passengerRepository.GetPassengerByID(passengerId);
            return Ok( updatedpasssengerdetails);
        }
        catch (ArgumentException ex)
        {
            return BadRequest($"Error updating passenger details: {ex.Message}");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating passenger details: {ex.Message}");
        }
    }
    #endregion
}

    



