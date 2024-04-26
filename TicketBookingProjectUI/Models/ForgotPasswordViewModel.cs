using System.ComponentModel.DataAnnotations;

namespace TicketBookingProjectUI.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        public string NewPassword {  get; set; }
        public string ConfirmPassword { get; set; }
    }
}
