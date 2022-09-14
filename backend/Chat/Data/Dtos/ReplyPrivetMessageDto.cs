namespace ChatProject.Data.Dtos
{
    public class ReplyPrivetMessageDto
    {
        public string Text { get; set; }
        public long UserId { get; set; }
        public long ChatId { get; set; }
        public long ReplyTo { get; set; }
    }
}
