using ChatProject.Models;
using Microsoft.EntityFrameworkCore;
namespace ChatProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>()
                .Property(c => c.Type)
                .HasConversion<string>();
            modelBuilder.Entity<Chat>()
                .HasMany(c => c.UserChats)
                .WithOne(uc => uc.Chat)
                .HasForeignKey(uc => uc.ChatId);           

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserChats)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Reply)
                .WithMany()
                .HasForeignKey(m => m.ReplyForId)
                .IsRequired(false);
            modelBuilder.Entity<UserChat>()
                .HasMany(uc => uc.Messages)
                .WithOne(m => m.UserChat)
                .HasForeignKey(m => m.UserChatId);
        }
    }
}
