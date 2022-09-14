namespace ChatProject.Data.Dtos
{
    public class PostMessageDto
    {
        public string Text { get; set; }
        public long? ChatId { get; set; }
        public long SenderId { get; set; }
        public long? UserId { get; set; }
        public long? ReplyForId { get; set; }
    }
}
