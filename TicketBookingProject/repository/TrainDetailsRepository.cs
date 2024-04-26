using Microsoft.EntityFrameworkCore;
using TicketBookingProject.Data.ApplicationDbContext;

using TicketBookingProject.Data.Dto;
using TicketBookingProject.Data.Models;
using TicketBookingProject.IRepository;

using AutoMapper;

namespace TicketBookingProject.repository
{
    public class TrainDetailsRepository : ITrainDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TrainDetailsRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddTrainDetails(TrainDetails trainDetails)
        {
            await _context.TrainDetails.AddAsync(trainDetails);
            await _context.SaveChangesAsync();
        }
        #region Get Seat Availability
        public TrainWiseSeatAvailablityDto GetSeatAvailability(int trainNumber)
        {
           var seatavailability=_context.TrainWiseSeatAvailabilities.FirstOrDefault(sa=>sa.TrainNumber == trainNumber);

            if (seatavailability == null)
            {
                return null;
            }
            var seatavailablitydto=_mapper.Map<TrainWiseSeatAvailablityDto>(seatavailability);
            return seatavailablitydto;
        }
        #endregion
        #region Get All trains

        public IEnumerable<TrainDetailsDto> GetAllTrains()
        {
            var trains = _context.TrainDetails.ToList(); // Retrieve all train details from the database
            var trainDtos = _mapper.Map<IEnumerable<TrainDetailsDto>>(trains); // Map entities to DTOs
            return trainDtos;
        }
        #endregion
        #region Get Available Trains        
        public IEnumerable<TrainDetails> GetAvailableTrains(DateTime fromDate, DateTime? toDate = null)
        {
            if (!toDate.HasValue)
            {
                 // When toDate is not provided, only consider trains available on fromDate
                return _context.TrainDetails
                    .Where(train => train.DepartureDate.Date == fromDate.Date)
                    .ToList();
            }
            else
            {
                // Filter trains based on the date range
                return _context.TrainDetails
                    .Where(train => train.DepartureDate >= fromDate && train.DepartureDate <= toDate.Value)
                    .ToList();
            }
        }
        #endregion
        #region GetTrainDetails
        public TrainDetails GetTrainDetails(int trainNumber)
        {
            return _context.TrainDetails
                .Include(td => td.SeatAvailability)
                .FirstOrDefault(td => td.TrainNumber == trainNumber);
        }
        #endregion
        #region GetFareByyClassAndTrainNumber
        public decimal? GetFareByClassAndTrainNumber(int trainNumber, string className)
        {
            decimal? fare = null;

            switch (className)
               {
                case "FirstClass":
                    fare = _context.TrainWiseSeatAvailabilities
                                .Where(sa => sa.TrainNumber == trainNumber)
                                .Select(sa => sa.FirstClassFare)
                                .FirstOrDefault();
                    break;

                case "SecondClass":
                    fare = _context.TrainWiseSeatAvailabilities
                                .Where(sa => sa.TrainNumber == trainNumber)
                                .Select(sa => sa.SecondClassFare)
                                .FirstOrDefault();
                    break;

                case "SleeperClass":
                    fare = _context.TrainWiseSeatAvailabilities
                                .Where(sa => sa.TrainNumber == trainNumber)
                                .Select(sa => sa.SleeperClassFare)
                                .FirstOrDefault();
                    break;

                default:
                    // Handle invalid class name
                    break;
            }

            return fare;
        }
        #endregion
    }
}
