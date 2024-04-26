// Image.cs
using System.ComponentModel.DataAnnotations;
using TicketBookingProject.Models;

namespace TicketBookingProject.Data.Models
{
    public class Image
    {
        [Key]
        public int Image_Id { get; set; }

        [Required]
        public byte[] Data { get; set; }

       
    }
}
