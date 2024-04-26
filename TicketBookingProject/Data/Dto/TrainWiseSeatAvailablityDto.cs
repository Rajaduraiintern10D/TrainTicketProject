using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TicketBookingProject.Data.Models;

namespace TicketBookingProject.Data.Dto
{
    public class TrainWiseSeatAvailablityDto
    {
        public int TrainNumber { get; set; }
        public int FirstClassTotalSeat { get; set; }
        public int FirstClassFare { get; set; }
        public int SecondClassTotalSeat { get; set; }
        public int SecondClassFare { get; set; }
        public int SleeperClassTotalSeat { get; set; }
        public int SleeperClassFare { get; set; }
    }
}
