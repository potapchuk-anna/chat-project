using ChatProject.Data.Dtos;
using ChatProject.Models;

namespace ChatProject.Services
{
    public interface IChatService
    {
        public Task<List<ChatDto>> Get(long userId);
    }
}
