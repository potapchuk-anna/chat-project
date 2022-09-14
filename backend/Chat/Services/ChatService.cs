using ChatProject.Data;
using ChatProject.Data.Dtos;
using ChatProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatProject.Services
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext context;

        public ChatService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<ChatDto>> Get(long userId)
        {
            return await context.UserChats
                .Include(c => c.Chat)
                .Include(uc => uc.Messages)
                .Where(uc=>uc.UserId == userId)
                .Select(uc=>new ChatDto
                {
                    Id = uc.Chat.Id,
                    LastMessage = context.Messages
                    .Include(m=>m.UserChat)
                    .Where(m=>m.UserChat.ChatId==uc.ChatId && (m.IsDeletedForSender==null || m.IsDeletedForSender==true && m.UserChat.UserId!=userId))
                    .OrderBy(m=>m.Time)
                    .Last().Text,
                    Name = uc.Chat.Name==null? 
                    context.UserChats
                    .Include(c=>c.User)
                    .FirstOrDefault(c=>c.ChatId==uc.ChatId && c.UserId!=uc.UserId).User.Username: uc.Chat.Name
                })
                .ToListAsync();   
        }
    }
}
