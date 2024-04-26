using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using TicketBookingProject.Data.Models;

namespace TicketBookingProject.Models
{
    public class UsersCredential
    {
        [Key]
        public int User_ID { get; set; }

        [Required]
        public string User_Name { get; set; }

        [Required]
        public string User_Password { get; set; }

        [Required]
        public string Role { get; set; }

        // New property
        [Required]
        public string MailId { get; set; }
       
   
    }
}
