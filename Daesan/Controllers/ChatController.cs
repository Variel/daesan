using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daesan.Models;
using Daesan.Models.Request;
using Daesan.Models.Response;
using Daesan.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;

namespace Daesan.Controllers
{
    public class ChatController : Controller
    {
        private readonly ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("keyboard")]
        public object Keyboard()
        {
            return new Keyboard { Type = KeyboardType.Text };
        }
        
        [Route("message")]
        public async Task<MessageResponse> Message([FromBody] ChatRequest request)
        {
            return await _chatService.GetResponseAsync(request.UserKey, request.Type, request.Content);
        }
    }
}
