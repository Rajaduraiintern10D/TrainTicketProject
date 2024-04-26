using TicketBookingProject.Data.Dto;
using TicketBookingProject.Models;
using TicketBookingProject.Shared_Folder;

public interface IPassengerRepository
{
    //int CalculatePassenger(int trainNumber, int passengerCount);
    
    int  AddPassenger(PassengerDetails passengerDetails);
    PassengerDetails GetPassengerByID(int passengerId);
    void UpdatePassengerStatus(int passengerId,GlobalConstantEnums status);
    void UpdatePassenger(PassengerDetails passengerDetails, PassengerDto passengerDto);
    //Task<PassengerDetails> GetPassengerById(int id);
    //Task UpdatePassengerDetails(int passengerId, [FromBody] PassengerRequestModel passengerRequest);
    //PassengerDetails GetById(int id);

    //IEnumerable<PassengerDetails> GetAllPassengers();
    //void Add(PassengerDetails passenger);
    //void Update(PassengerDetails passenger);
    //void Delete(int id);
}
