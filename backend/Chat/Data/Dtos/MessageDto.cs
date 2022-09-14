namespace ChatProject.Data.Dtos
{
    public class MessageDto
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public long SenderId { get; set; }
        public string SenderUsername { get; set; }
        public MessageDto? ReplyFor { get;set; }
    }
}
