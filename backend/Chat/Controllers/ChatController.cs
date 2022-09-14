using ChatProject.Data.Dtos;
using ChatProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatProject.Controllers
{
    [Route("api/chats")]
    public class ChatController : ApiController
    {
        private readonly IChatService service;

        public ChatController(IChatService service)
        {
            this.service = service;
        }
        [HttpGet, Route("{userId:long}")]
        public async Task<ActionResult<List<ChatDto>>> Get([FromRoute] long userId)
        {
            return await service.Get(userId);
        }
    }
}
