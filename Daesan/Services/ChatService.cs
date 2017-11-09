using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daesan.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace Daesan.Services
{
    public class ChatService
    {
        private readonly DatabaseContext _database;
        private readonly UserContextService _userContext;

        public ChatService(DatabaseContext database, UserContextService userContext)
        {
            _database = database;
            _userContext = userContext;
        }

        public async Task<MessageResponse> GetResponseAsync(string userKey, string type, string content)
        {
            content = content.Trim().Replace(" ", "").Replace("#", "");

            var user = await _userContext.GetUserAsync(userKey);

            var command = await _database.Commands.SingleOrDefaultAsync(c => c.Input == content);

            if (command == null)
            {
                if (user.CurrentScene == 0)
                {
                    user.CurrentScene++;
                    await _database.SaveChangesAsync();

                    return new MessageResponse
                    {
                        Keyboard = Keyboard.TextKeyboard,
                        Message = new Message
                        {
                            Text = "교보문고 광화문점의 B10 아래 빛을 찾아 그 곳으로 가시오!\n" +
                                   "도움을 얻고 싶다면 #B10 이라고 써서 본부로 보내시오. 여정자의 길을 밝혀줄 것이오."
                        }
                    };
                }

                
                var message = "우리가 찾는 답이 아닐세. 다시 찾아 보게나. 자네가 해낼거라 믿네.";

                return new MessageResponse
                {
                    Keyboard = Keyboard.TextKeyboard,
                    Message = new Message
                    {
                        Text = message
                    }
                };
            }

            if (!content.StartsWith("#"))
            {
                user.CurrentScene++;
                await _database.SaveChangesAsync();
            }

            return new MessageResponse
            {
                Keyboard = Keyboard.TextKeyboard,
                Message = new Message
                {
                    MessageButton = String.IsNullOrWhiteSpace(command.ResponseButtonUrl)
                        ? null
                        : new MessageButton
                        {
                            Url = command.ResponseButtonUrl,
                            Label = command.ResponseButtonLabel,
                        },
                    Photo = String.IsNullOrWhiteSpace(command.ResponsePhotoUrl)
                        ? null
                        : new Photo
                        {
                            Url = command.ResponsePhotoUrl,
                            Height = command.ResponsePhotoHeight,
                            Width = command.ResponsePhotoWidth
                        },
                    Text = command.ResponseText
                }
            };
        }
    }
}
