using Application.Messaging.Message.Commands.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;
[ApiController]
[Route("api/v1/message")]
public sealed class MessageController(ISender sender) : ControllerBase
{
    [HttpPost("send-without-encrypt")]
    public async Task<IResult> SendMessage(TestSendMessageCommand sendMessageCommand)
    {
        await sender.Send(sendMessageCommand);
        return Results.Ok();
    }
}