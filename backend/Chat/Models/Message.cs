namespace ChatProject.Models
{
    public class Message: BaseModel
    {
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public long UserChatId { get; set; }
        public UserChat UserChat { get; set; }
        public bool? IsDeletedForSender { get; set; }
        public long? ReplyForId { get; set; }
        public Message? Reply { get; set; }
    }
}
