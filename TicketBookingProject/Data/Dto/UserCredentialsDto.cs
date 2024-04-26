using System.ComponentModel.DataAnnotations;

namespace TicketBookingProject.Data.Dto
{
    public class UserCredentialsDto
    {
        public string User_Name { get; set; }
        [Required]
        public string User_Password { get; set; }
    }
}
