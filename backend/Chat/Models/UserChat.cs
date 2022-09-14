namespace ChatProject.Models
{
    public class UserChat: BaseModel
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long ChatId { get; set; }
        public Chat Chat { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
