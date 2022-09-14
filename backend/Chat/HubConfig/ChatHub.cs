using ChatProject.Data.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace ChatProject.HubConfig
{
    public class ChatHub : Hub
    {
        public Task SendMessage(long chatId)
        {
            return Clients.All.SendAsync("ReceiveMessageChatId", chatId);
        }
    }
}
