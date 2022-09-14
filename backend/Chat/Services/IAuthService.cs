using ChatProject.Data.Dtos;

namespace ChatProject.Services
{
    public interface IAuthService
    {
        public Task<string> Login(UserDto user);
    }
}
