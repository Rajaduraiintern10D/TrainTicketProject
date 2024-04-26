using System;
using System.ComponentModel.DataAnnotations;

namespace TicketBookingProjectUI.Models
{
    public class TrainData
    {
        public int TrainNumber { get; set; }

        public string TrainName { get; set; }

        public string StartingCity { get; set; }

        public string DestinationCity { get; set; }

        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        public string DepartureTime { get; set; } // Change data type to string

        [DataType(DataType.Date)]
        public DateTime DestinationDate { get; set; }

        public string DestinationTime { get; set; } // Change data type to string
    }
}
