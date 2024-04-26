using System.ComponentModel.DataAnnotations;
using TicketBookingProject.Data.Models;

namespace TicketBookingProject.Data.Dto
{
    public class TrainDetailsDto
    {

        public int TrainNumber { get; set; }

        public string TrainName { get; set; }

        public string StartingCity { get; set; }

        public string DestinationCity { get; set; }

        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan DepartureTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime DestinationDate { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan DestinationTime { get; set; }

    }
}
