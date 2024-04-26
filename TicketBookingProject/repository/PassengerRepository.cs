using Microsoft.EntityFrameworkCore;

using TicketBookingProject.Models;
using TicketBookingProject.Data.ApplicationDbContext;
using System.Runtime.InteropServices;
using TicketBookingProject.IRepository;
using Microsoft.AspNetCore.Mvc;
using TicketBookingProject.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using TicketBookingProject.Shared_Folder;
using TicketBookingProject.Data.Dto;
using AutoMapper;

public class PassengerRepository : IPassengerRepository
{
    private readonly ApplicationDbContext _context;
    
    private readonly ITrainDetailsRepository _trainDetailsRepository;
    private readonly IMapper _mapper;

    public PassengerRepository(ApplicationDbContext context, IMapper mapper,
        ITrainDetailsRepository trainDetailsRepository)
    {
        _trainDetailsRepository = trainDetailsRepository;
        _context = context;
        _mapper = mapper;
    }
    #region Add Passenger
    public int AddPassenger(PassengerDetails passengerDetails)
    {
        _context.PassengerDetails.Add(passengerDetails);
        _context.SaveChanges();
        return passengerDetails.P_Id;
    }
    #endregion
    #region GetPassengerById
    public PassengerDetails GetPassengerByID(int passengerId)
    {
        return _context.PassengerDetails.Find(passengerId);
    }
    #endregion
    #region Update Passenger Status
    public void UpdatePassengerStatus(int passengerId, GlobalConstantEnums status)
    {
        var passenger = _context.PassengerDetails.Find(passengerId);
        if (passenger != null)
        {
            passenger.Status = status.ToString();
            _context.SaveChanges();
        }
    }
    #endregion

    #region Update passenger
     public void UpdatePassenger(PassengerDetails passengerDetails, PassengerDto passengerDto)
    {
        var existingPassenger = _context.PassengerDetails.Find(passengerDetails.P_Id);
        if (existingPassenger == null)
        {
            throw new ArgumentException("Passenger not found.", nameof(passengerDetails));
        }

        // Check if _trainDetailsRepository is null
        if (_trainDetailsRepository == null)
        {
            throw new Exception("_trainDetailsRepository is not initialized.");
        }

        // Retrieve train details
        var trainDetails = _trainDetailsRepository.GetTrainDetails(passengerDto.TrainNumber);

        // Check if trainDetails is null
        if (trainDetails == null)
        {
            throw new Exception("Train details not found.");
        }

        decimal? fare = _trainDetailsRepository.GetFareByClassAndTrainNumber
           (passengerDto.TrainNumber, passengerDto.Class);

        if (fare == null)
        {
            throw new Exception($"Fare not found for class: {passengerDto.Class} " +
                "and train number: {passengerRequest.TrainNumber}");
        }
        //Calculate total fare based on fare per ticket and total ticket count
        decimal totalFare = fare.Value * passengerDto.TotalTicketCount;

        existingPassenger.Passenger_Name = passengerDto.Passenger_Name;
        existingPassenger.Passenger_Age = passengerDto.Passenger_Age;
        existingPassenger.Passenger_gender = passengerDto.Passenger_gender;
        existingPassenger.Class = passengerDto.Class;
        existingPassenger.TrainNumber = passengerDto.TrainNumber;
        existingPassenger.TotalFare=totalFare;
        existingPassenger.StartingCity = passengerDetails.StartingCity;
        existingPassenger.DepartureDate = passengerDetails.DepartureDate;
        existingPassenger.DestinationCity = passengerDetails.DestinationCity;
        existingPassenger.DestinationDate= passengerDetails.DestinationDate;
        existingPassenger.TotalTicketCount= passengerDto.TotalTicketCount;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error updating passenger details: Database update error.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating passenger details.", ex);
        }

    }




    #endregion
}


