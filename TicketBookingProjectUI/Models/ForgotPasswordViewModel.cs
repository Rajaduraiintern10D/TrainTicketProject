using System.ComponentModel.DataAnnotations;

namespace TicketBookingProjectUI.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; }
        [Required]
        public string NewPassword {  get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
