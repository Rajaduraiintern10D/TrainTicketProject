using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBookingProject.Data.Models
{
    public class TrainDetails
    {



        [Key]
        [Required(ErrorMessage = "Train Number is required")]
        public int TrainNumber { get; set; }

        [Required(ErrorMessage = "Train name is Required")]
        public string TrainName { get; set; }

        [Required(ErrorMessage = "Train StartingCity is required")]
        public string StartingCity { get; set; }

        [Required(ErrorMessage = "Train DestinationCity is required")]
        public string DestinationCity { get; set; }

        [Required(ErrorMessage = "Train DepartureDateTime is Required")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage ="Train Departure time is required")]
        [DataType(DataType.Time)]
        public TimeSpan DepartureTime {  get; set; }

        [Required(ErrorMessage = "Train DestinationDateTime is Required")]
        [DataType(DataType.Date)]
        public DateTime DestinationDate { get; set; }

        [Required(ErrorMessage ="Train DestinationTime is Required")]
        [DataType(DataType.Time)]
        public TimeSpan DestinationTime { get; set; }

        // Navigation property for TrainWiseSeatAvailability
        public TrainWiseSeatAvailability SeatAvailability { get; set; }



    }
}
