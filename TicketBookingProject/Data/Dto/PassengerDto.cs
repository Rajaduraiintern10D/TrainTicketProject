using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TicketBookingProject.Data.Models;

namespace TicketBookingProject.Data.Dto
{
    public class PassengerDto
    {
        public int P_Id { get; set; }
        public string Passenger_Name { get; set; }
        public int Passenger_Age { get; set; }
        public string Passenger_gender { get; set; }
        public int TrainNumber { get; set; } 
        public string Class { get; set; }
        public int TotalTicketCount { get; set; }
       // public decimal TotalFare { get; set; }

    }
}
