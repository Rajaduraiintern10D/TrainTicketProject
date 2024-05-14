using Microsoft.AspNetCore.Http; // Add this namespace for IFormFile
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TicketBookingProject.Data.Models;

namespace TicketBookingProject.Models
{
    public class PassengerDetails
    {
        [Key]
        public int P_Id { get; set; }
        public string Passenger_Name { get; set; }
        [Required]
        public int Passenger_Age { get; set; }
        [Required]
        public string Passenger_gender { get; set; }
        
        // Foreign key referencing TrainDetails
        [ForeignKey("Train")]
        public int TrainNumber { get; set; }
        [JsonIgnore]
        // Navigation property for TrainDetails
        public TrainDetails Train { get; set; }
       
        [Required]
        public string StartingCity { get; set; }
       
        [Required]
        public string DestinationCity { get; set; }
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan DepartureTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime DestinationDate { get; set; }
        [DataType(DataType.Time)]   
        public TimeSpan DestinationTime { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        public int TotalTicketCount { get; set; }
        public decimal TotalFare { get; set; }
        public string Status { get; set; } = "Pending";
        [DataType(DataType.DateTime)]
        public DateTime BookedTicketTime { get; set; }

       
    }
}
