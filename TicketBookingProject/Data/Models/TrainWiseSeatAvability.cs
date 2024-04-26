using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBookingProject.Data.Models
{
    public class TrainWiseSeatAvailability
    {
        [Key]
        [Required(ErrorMessage = "Train Number is required")]
        public int TrainNumber { get; set; }

        // Navigation property for TrainDetails
        [ForeignKey("TrainNumber")] // Added ForeignKey attribute to specify the foreign key relationship
        public TrainDetails TrainDetails { get; set; }

        public int FirstClassTotalSeat { get; set; }
        public int FirstClassFare { get; set; }
        public int SecondClassTotalSeat { get; set; }
        public int SecondClassFare { get; set; }
        public int SleeperClassTotalSeat { get; set; }
        public int SleeperClassFare { get; set; }
    }
}
