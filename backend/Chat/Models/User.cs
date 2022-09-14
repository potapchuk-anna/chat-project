namespace ChatProject.Models
{
    public class User: BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username {  get; set; }
        public virtual ICollection<UserChat> UserChats { get; set; }
    }
}
