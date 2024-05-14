using System.ComponentModel.DataAnnotations;

namespace TicketBookingProjectUI.Models
{
    public class RegisterUser
    {
        public int User_ID { get; set; }

       
        public string User_Name { get; set; }
        public string User_Password { get; set; }
        public string Role { get; set; }
        public string MailId { get; set; }
    }
}
