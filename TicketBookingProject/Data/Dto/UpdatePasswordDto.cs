namespace TicketBookingProject.Data.Dto
{
    public class UpdatePasswordDto
    {
        public string UserNameOrEmail { get; set; }
        public string NewPassword { get; set; }
    }
}
