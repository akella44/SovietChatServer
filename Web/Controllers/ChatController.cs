using System.Security.Claims;
using System.Text.Json;
using Application.Cryptography.Providers;
using Application.Customers.Login.Quries;
using Application.Messaging.CreateChat;
using Domain.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;
[ApiController]
[Authorize]
[Route("api/v1/chats")]
public sealed class ChatController(ISender sender) : ControllerBase
{
    [HttpPost("create")]
    [Consumes("text/plain")]
    public async Task<IResult> CreateChat([FromBody] string body)
    {
        Request.Headers.TryGetValue("signature", out var signature );
        
        if(string.IsNullOrEmpty(signature))
            return Results.BadRequest(Content("Miss signature in ur request headers"));
        
        var sessionQuery = new GetSessionQuery(signature);
        var session = await sender.Send(sessionQuery);
        var decryptedData = await sender.Send(new DecryptCommand<AesProvider>(body, session.SessionKey));
        
        CreateChatCommand? createChatCommand = JsonSerializer.Deserialize<CreateChatCommand>(decryptedData);
        
        if (createChatCommand == null)
            return Results.BadRequest();
        
        createChatCommand.UserTags.Add(User.FindFirstValue(ClaimTypes.NameIdentifier));
        await sender.Send(createChatCommand);
        
        return Results.Ok();
    }
}