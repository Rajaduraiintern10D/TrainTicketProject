using System.ComponentModel.DataAnnotations;
using TicketBookingProject.Data.Dto;
using TicketBookingProject.Models;

namespace TicketBookingProjectUI.Models
{
    public class PassengerViewModel
    {
        public PassengerDetails PassengerDetails { get; set; }
        public PassengerDto PassengerDto { get; set; }
        public byte[] ImageData { get; set; } // Add this property for image data

    }
}
