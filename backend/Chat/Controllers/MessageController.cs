using ChatProject.Data.Dtos;
using ChatProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatProject.Controllers
{
    [Route("api/messages")]
    public class MessageController : ApiController
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService service)
        {
            this.messageService = service;
        }

        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] PostMessageDto messageDto)
        {
            await messageService.PostMessage(messageDto);
            return Ok();
        }
        [HttpGet, Route("chat/{chatId:long}/{loginedUserId:long}")]
        public async Task<ActionResult<List<MessageDto>>> Get([FromRoute] long chatId, [FromQuery] long page, [FromRoute] long loginedUserId)
        {
            return await messageService.Get(chatId, page, loginedUserId);
        }
        [HttpGet, Route("chat/{chatId:long}/total")]
        public async Task<ActionResult<long>> Get([FromRoute] long chatId)
        {
            return await messageService.GetTotal(chatId);
        }

        [HttpPut, Route("{id:long}")]
        public async Task<IActionResult> Edit([FromRoute] long id, [FromBody] EditMessageDto message)
        {
            await messageService.Edit(id, message.Text);
            return Ok();
        }

        [HttpDelete, Route("{id:long}")]
        public async Task<IActionResult> Delete([FromRoute] long id, [FromQuery] bool isForAll)
        {
            await messageService.Delete(id, isForAll);
            return Ok();
        }
        
    }
}
