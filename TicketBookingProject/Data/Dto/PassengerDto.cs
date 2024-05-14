using System.ComponentModel.DataAnnotations;

namespace TicketBookingProject.Data.Dto
{
    public class PassengerDto
    {
        public int P_Id { get; set; }

        [Required(ErrorMessage = "Passenger name is required")]
        public string Passenger_Name { get; set; }

        [Required(ErrorMessage = "Passenger age is required")]
        [Range(1, 150, ErrorMessage = "Passenger age must be between 1 and 150")]
        public int Passenger_Age { get; set; }

        [Required(ErrorMessage = "Passenger gender is required")]
        public string Passenger_gender { get; set; }

        [Required(ErrorMessage = "Train number is required")]
        public int TrainNumber { get; set; }

        [Required(ErrorMessage = "Class is required")]
        public string Class { get; set; }

        [Required(ErrorMessage = "Total ticket count is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Total ticket count must be at least 1")]
        public int TotalTicketCount { get; set; }


        public string? Image { get; set; }
    }
}
