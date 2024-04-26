using TicketBookingProject.Data.Dto;
using TicketBookingProject.Models;

namespace TicketBookingProject.IRepositry
{
    public interface IUserCredentialRepository
    {
        string AuthenticateUser(UserCredentialsDto users);
        void AddUser(UsersCredential user);
        void UpdatePassword(string userNameOrEmail, string newPassword);

    }
}
