using System.ComponentModel.DataAnnotations;

namespace TicketBookingProject.Data.Models
{
    public class Image
    {
        [Key]
        public int Image_Id { get; set; }

        [Required]
        public byte[] Data { get; set; }

        public int P_Id { get; set; }
    }
}
