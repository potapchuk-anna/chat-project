using ChatProject.Data;
using ChatProject.Data.Dtos;
using ChatProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatProject.Services
{
    public class AuthService: IAuthService
    {
        private readonly ApplicationDbContext context;
        public AuthService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<string> Login(UserDto user)
        {
            var authUser = await context.Users.FirstOrDefaultAsync(u => 
            u.Password==user.Password && u.Email == user.Email);
            if (authUser == null)
                throw new Exception("User does not exist.");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret1234567890"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7271",
                    audience: "https://localhost:4200",
                    claims: new List<Claim>()
                    {
                        new ("id", authUser.Id.ToString())
                    },
                    expires:DateTime.Now.AddDays(7),
                    signingCredentials: signinCredentials
                );
            return new JwtSecurityTokenHandler()
                .WriteToken(tokenOptions);
        }
    }
}
