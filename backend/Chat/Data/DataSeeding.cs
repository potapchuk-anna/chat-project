using ChatProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatProject.Data
{
    public class DataSeeding
    {
        private readonly ApplicationDbContext context;

        public DataSeeding(ApplicationDbContext context)
        {
            this.context = context;
        }

        //seeding data
        public void SeedData()
        {
            List<User> users = new List<User>
            {
               new User//1
               {
                    Email = "lily@i.ua",
                    Password = "12345",
                    Username = "lily_blue"
               },
                new User//2
                {
                    Email = "dan@i.ua",
                    Password = "12345",
                    Username = "denis"
                },
                new User//3
                {
                    Email = "jhon@i.ua",
                    Password = "12345",
                    Username = "jhonny"
                },
                new User//4
                {
                    Email = "kate@i.ua",
                    Password = "12345",
                    Username = "kate"
                },
                new User//5
                {
                    Email = "glib@i.ua",
                    Password = "234",
                    Username = "glib"
                },
                new User//6
                {
                    Email = "peter@i.ua",
                    Password = "5555555",
                    Username = "peter"
                },
                new User//7
                {
                    Email = "kiril@i.ua",
                    Password = "15437",
                    Username = "kiril"
                },
                new User//8
                {
                    Email = "max@i.ua",
                    Password = "12345",
                    Username = "max"
                },
                new User//9
                {
                    Email = "andrew@i.ua",
                    Password = "545455",
                    Username = "andrew"
                },
                new User//10
                {
                    Email = "anya@i.ua",
                    Password = "2342424",
                    Username = "anya"
                }
            };
            List<Chat> chats = new List<Chat>
            {
                new Chat//0
                {
                    Name="Classmates Chat",
                    Type=Models.Type.Chat,
                },
                new Chat//1
                {
                    Type=Models.Type.Private,
                },
                new Chat//2
                {
                    Type=Models.Type.Private,
                },
                new Chat//3
                {
                    Type=Models.Type.Private,
                },
                new Chat//4
                {
                    Type=Models.Type.Private,
                },
                new Chat//5
                {
                    Type=Models.Type.Private,
                },
                new Chat//6
                {
                    Name="Girls Chat",
                    Type=Models.Type.Chat,
                },
                new Chat//7
                {
                    Name="Hometask Chat",
                    Type=Models.Type.Chat,
                }
            };
            List<UserChat> userChats = new List<UserChat>
            {
                new UserChat//0
                {
                    User=users[0],
                    Chat = chats[1],
                },
                new UserChat//1
                {
                    User=users[0],
                    Chat = chats[2],
                },
                new UserChat//2
                {
                    User=users[0],
                    Chat = chats[3],
                },
                new UserChat//3
                {
                    User=users[0],
                    Chat = chats[4],
                },
                new UserChat//4
                {
                    User=users[0],
                    Chat = chats[5],
                },
                new UserChat//5
                {
                    User=users[1],
                    Chat = chats[1],
                },
                new UserChat//6
                {
                    User=users[0],
                    Chat = chats[0],
                },
                new UserChat//7
                {
                    User=users[1],
                    Chat = chats[0],
                },
                new UserChat//8
                {
                    User=users[2],
                    Chat = chats[0],
                },
                new UserChat//9
                {
                    User=users[0],
                    Chat = chats[6],
                },
                new UserChat//10
                {
                    User=users[3],
                    Chat = chats[6],
                },
                new UserChat//11
                {
                    User=users[9],
                    Chat = chats[6],
                },
                new UserChat//12
                {
                    User=users[0],
                    Chat = chats[7],
                },
                new UserChat//13
                {
                    User=users[1],
                    Chat = chats[7],
                },
                new UserChat//14
                {
                    User=users[2],
                    Chat = chats[7],
                },
                new UserChat//15
                {
                    User=users[4],
                    Chat = chats[7],
                },
                new UserChat//16
                {
                    User=users[5],
                    Chat = chats[7],
                },
                new UserChat//17
                {
                    User=users[6],
                    Chat = chats[7],
                },
                new UserChat//18
                {
                    User=users[7],
                    Chat = chats[7],
                },
                new UserChat//19
                {
                    User=users[8],
                    Chat = chats[7],
                },
                 new UserChat//20
                {
                    User=users[2],
                    Chat = chats[2],
                },
                new UserChat//21
                {
                    User=users[3],
                    Chat = chats[3],
                },
                new UserChat//22
                {
                    User=users[4],
                    Chat = chats[4],
                },
                new UserChat//23
                {
                    User=users[5],
                    Chat = chats[5],
                },
            };
            List<Message> messages = new List<Message>
            {
                new Message
                {
                    Text="Hello",
                    UserChat=userChats[0],
                    Time = DateTime.UtcNow
                },                
                new Message
                {
                    Text="Hi",
                    UserChat=userChats[1],
                    Time = DateTime.UtcNow
                },
                new Message
                {
                    Text="Hello",
                    UserChat=userChats[2],
                    Time = DateTime.UtcNow
                },
                new Message
                {
                    Text="Hello",
                    UserChat=userChats[3],
                    Time = DateTime.UtcNow
                },
                new Message
                {
                    Text="Hello",
                    UserChat=userChats[4],
                    Time = DateTime.UtcNow
                },
                new Message
                {
                    Text="Hello)",
                    UserChat=userChats[5],
                    Time = DateTime.UtcNow
                },
                new Message
                {
                    Text="Hello)",
                    UserChat=userChats[6],
                    Time = DateTime.UtcNow
                },
                new Message
                {
                    Text="Hey",
                    UserChat=userChats[8],
                    Time = DateTime.UtcNow
                },
            };
            if(!context.UserChats.Any())
                context.UserChats.AddRange(userChats);
            if (!context.Messages.Any())
                context.Messages.AddRange(messages);
            context.SaveChanges();           
        }
    }
}
