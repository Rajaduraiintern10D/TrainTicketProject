using TicketBookingProject.Data.Dto;
using TicketBookingProject.Data.Models;

namespace TicketBookingProject.IRepository
{
    public interface ITrainDetailsRepository
    {
        //To get Fare by clss and train number
        Task AddTrainDetails(TrainDetails trainDetails);
        decimal? GetFareByClassAndTrainNumber(int  trainNumber,string TrainClass);
        TrainWiseSeatAvailablityDto GetSeatAvailability(int trainNumber);
        IEnumerable<TrainDetailsDto> GetAllTrains();
        IEnumerable<TrainDetails> GetAvailableTrains(DateTime fromDate,DateTime ? toDate);
        TrainDetails GetTrainDetails(int trainNumber);
    }
}
