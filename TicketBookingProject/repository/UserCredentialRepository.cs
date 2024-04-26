using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TicketBookingProject.Data.ApplicationDbContext;
using TicketBookingProject.Data.Dto;
using TicketBookingProject.IRepositry;
using TicketBookingProject.Models;

namespace TicketBookingProject.Repositry
{
    public class UserCredentialRepository : IUserCredentialRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserCredentialRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public void AddUser(UsersCredential User)
        {
            _context.UsersCredential.Add(User);
            _context.SaveChanges();
        }
        #region AuthenticateUser
        public string AuthenticateUser(UserCredentialsDto user)
        {
            var userFromDb = _context.UsersCredential.SingleOrDefault(u => u.User_Name == user.User_Name);

            if (userFromDb != null && userFromDb.User_Password == user.User_Password)
            {
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.User_Name),
                    new Claim(JwtRegisteredClaimNames.Email, user.User_Name)
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = signingCredentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return jwtToken;
            }

            return null;
        }
        #endregion
        public void UpdatePassword(String UsernameOrEmail,string newPassword)
        {
            var userFromDb = _context.UsersCredential.SingleOrDefault(u => u.User_Name == UsernameOrEmail || u.MailId == UsernameOrEmail);
            {
                if (userFromDb != null)
                {
                    userFromDb.User_Password = newPassword;
                    _context.SaveChanges();
                }
            }
        }
        
    }
}
