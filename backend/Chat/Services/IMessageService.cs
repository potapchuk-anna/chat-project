using ChatProject.Data.Dtos;

namespace ChatProject.Services
{
    public interface IMessageService
    {
        public Task PostMessage(PostMessageDto message);
        public Task<List<MessageDto>> Get(long chatId, long page, long loginedUserId);
        public Task<long> GetTotal(long chatId);
        public Task Delete(long id, bool isForAll);
        public Task Edit(long id, string text);

    }
}
