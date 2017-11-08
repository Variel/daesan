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
            content = content.Trim().Replace(" ", "");

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

                string message;

                if (content == "대산")
                {
                    user.CurrentScene = 1;
                    await _database.SaveChangesAsync();

                    message = "나는 독립본부 안정근이다! 음모에 빠진 동료 김창수의 목숨을 구해주시오! 그는 국모시해범을 처단했으나 도리어 사형을 선고받았소.\n" +
                              "고종께서 친히 집행중지를 명했으나 일본군은 문서를 날조해 오늘밤 그를 죽일 계획이오. 그를 구해주시오! 당신만이 희망이오.\n" +
                              "광화문교보문고 B10 빛 아래 실마리를 숨겨두었으니 찾아가시오. 길눈이 어두우면 \"#B10\"을 보내시게나";
                }
                else if (content.StartsWith("#"))
                {
                    message = "허가된 질문만 대답할 수 있소.\n" +
                              "우리가 숨겨둔 지령을 잘 찾아보면 무엇을 물어야 할지, 무엇을 대답해야 할지 찾을 수 있을 것이오.\n" +
                              "실마리를 찾고 싶다면 #B10처럼 #으로 궁금한 대상을 보내면 되오.\n" +
                              "본부와의 교신을 처음부터 다시 하고 싶다면 '대산'을 입력하시오.";
                }
                else
                {
                    message = "우리가 찾는 답이 아니오. 다시 찾아 보길 바라오.\n" +
                              "그대가 해 낼거라 믿소.\n" +
                              "실마리를 찾고 싶다면 #B10처럼 #으로 궁금한 대상을 보내면 되오.\n" +
                              "본부와의 교신을 처음부터 다시 하고 싶다면 '대산'을 입력하시오.";
                }

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
