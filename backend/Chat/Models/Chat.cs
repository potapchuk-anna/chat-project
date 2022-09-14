namespace ChatProject.Models
{
    public class Chat: BaseModel
    {
        public string? Name { get; set; }
        public Type Type { get; set; }
        public virtual ICollection<UserChat> UserChats { get; set; }
    }
}
