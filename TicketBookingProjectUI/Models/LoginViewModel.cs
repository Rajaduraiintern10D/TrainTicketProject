using System.ComponentModel.DataAnnotations;

namespace TicketBookingProjectUI.Models
{
    public class LoginViewModel
    {
        /*[Required]*/
        public string User_Name { get; set; }

        /*[Required]*/
        [DataType(DataType.Password)]
        public string User_Password { get; set; }
    }

}
