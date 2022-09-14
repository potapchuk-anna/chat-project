using ChatProject.Data;
using ChatProject.Data.Dtos;
using ChatProject.HubConfig;
using ChatProject.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ChatProject.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext context;
        private readonly IHubContext<ChatHub> hubContext;

        public MessageService(ApplicationDbContext context, IHubContext<ChatHub> hubContext)
        {
            this.context = context;
            this.hubContext = hubContext;
        }
        public async Task Delete(long id, bool isForAll)
        {
            var message = context.Messages
                .Include(c=>c.UserChat)
                .FirstOrDefault(m => m.Id == id);
            if (isForAll)
            {
                context.Messages.Remove(message);
            }
            else
            {
                message.IsDeletedForSender = true;
                context.Messages.Update(message);
            }
            await context.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("ReceiveMessageChatId", message.UserChat.ChatId);
        }

        public async Task Edit(long id, string text)
        {
            var message = context.Messages
                .Include(m=>m.UserChat)
                .FirstOrDefault(m => m.Id == id);
            message.Text = text;
            context.Messages.Update(message);
            await context.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("ReceiveMessageChatId", message.UserChat.ChatId);
        }

        public async Task<List<MessageDto>> Get(long chatId, long page, long loginedUserId)
        {
            var messages = context.Messages.Include(m => m.UserChat)
                .ThenInclude(uc=>uc.User)
                .Include(m => m.Reply)
                .Where(m => m.UserChat.ChatId == chatId && (m.IsDeletedForSender==null || m.IsDeletedForSender == true && m.UserChat.UserId!=loginedUserId))
                .OrderBy(m => m.Time)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    Text = m.Text,
                    SenderId = m.UserChat.UserId,
                    ReplyFor = m.Reply != null ? new MessageDto
                    {
                        Id = (long)m.ReplyForId,
                        Text = m.Reply.Text,
                        SenderId = m.Reply.UserChat.UserId,
                    } : null,
                    SenderUsername = m.UserChat.User.Username
                });
            var paginatedMessages = await messages               
                .Skip((int)(page-1)*20)
                .Take(20)
                .ToListAsync();
            return paginatedMessages;
        }

        public async Task<long> GetTotal(long chatId)
        {
            return await context.Messages.Include(m => m.UserChat)
                .Where(m => m.UserChat.ChatId == chatId && m.IsDeletedForSender == null).LongCountAsync();
        }

        public async Task PostMessage(PostMessageDto message)
        {
            var curChatId = message.ChatId;
            if (!curChatId.HasValue)//private reply
            {
                var oldChatUser = context.UserChats
                    .Include(c=>c.Chat)
                    .Where(c => c.UserId == message.UserId && c.Chat.Type == Models.Type.Private);
                var oldChatSender = context.UserChats
                    .Include(c => c.Chat)
                    .Where(c => c.UserId == message.SenderId && c.Chat.Type == Models.Type.Private);

                var curChatIds = await oldChatUser.Select(c=>c.ChatId).Intersect(oldChatSender.Select(c=>c.ChatId)).ToListAsync();
                if(curChatIds.Count!=0)
                {
                    curChatId = curChatIds.First();
                }
                if (!curChatId.HasValue)
                {
                    var chat = (await context.Chats.AddAsync(new Chat
                    {
                        Type = Models.Type.Private,
                    })).Entity;
                    await context.SaveChangesAsync();
                    var userChat = (await context.UserChats.AddAsync(
                        new UserChat
                        {
                            UserId = message.SenderId,
                            ChatId = chat.Id
                        }
                    )).Entity;

                    await context.UserChats.AddAsync(new UserChat
                    {
                        UserId = (long)message.UserId,//another user
                        ChatId = chat.Id
                    });
                    await context.SaveChangesAsync();
                    await context.Messages.AddAsync(new Message
                    {
                        Text = message.Text,
                        UserChatId = userChat.Id,
                        ReplyForId = message.ReplyForId,
                        Time = DateTime.Now.ToUniversalTime()
                    });
                    await context.SaveChangesAsync();
                }   
            }
            if(curChatId.HasValue)
            {
                var chat = await context.Chats
                .Include(c => c.UserChats)
                .FirstOrDefaultAsync(c => c.Id == curChatId);
                var userChat = await context.UserChats.FirstAsync(uc => uc.ChatId == chat.Id && uc.UserId == message.SenderId);
                await context.AddAsync(new Message
                {
                    Text = message.Text,
                    UserChat = userChat,
                    Time = DateTime.Now.ToUniversalTime(),
                    ReplyForId = message.ReplyForId!=null?message.ReplyForId:null
                });
                await context.SaveChangesAsync();
            }
            await hubContext.Clients.All.SendAsync("ReceiveMessageChatId", curChatId);
            
        }
    }
}
