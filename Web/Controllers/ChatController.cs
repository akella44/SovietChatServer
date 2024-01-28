using System.Security.Claims;
using System.Text.Json;
using Application.Cryptography.Providers;
using Application.Customers.Login.Quries;
using Application.Messaging.Chat.Queries;
using Application.Messaging.CreateChat;
using Application.Model;
using Domain.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HostingEnvironmentExtensions = Microsoft.Extensions.Hosting.HostingEnvironmentExtensions;

namespace Web.Controllers;
[ApiController]
[Authorize]
[Route("api/v1/chats")]
public sealed class ChatController(ISender sender) : ControllerBase
{
    [HttpPost("create-group-chat")]
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
    
    [HttpGet("my-chats")]
    public async Task<IResult> GetUserChats()
    {
        Request.Headers.TryGetValue("signature", out var signature );
        
        if(string.IsNullOrEmpty(signature))
            return Results.BadRequest(Content("Miss signature in ur request headers"));
        
        var sessionQuery = new GetSessionQuery(signature);
        var session = await sender.Send(sessionQuery);
        
        if (User.FindFirstValue(ClaimTypes.NameIdentifier) == null)
            return Results.BadRequest();
        
        var getUserChatsQuery = new GetUserChatsQuery(User.FindFirstValue(ClaimTypes.NameIdentifier));
        IEnumerable<ChatDto> chatDtos = await sender.Send(getUserChatsQuery);

        var data = JsonSerializer.Serialize(chatDtos);
        var encryptedData = await sender.Send(new EncryptCommand<AesProvider>(data, session.SessionKey)); 
        return Results.Ok(Content(encryptedData));
    }
    
    /*[HttpPost("create-group-chat-without-encrypt")]
    public async Task<IResult> CreateChat(CreateChatCommand createChatCommand)
    {
        createChatCommand.UserTags.Add(User.FindFirstValue(ClaimTypes.NameIdentifier));
        await sender.Send(createChatCommand);
        
        return Results.Ok();
    }
    
    [HttpGet("my-chats-without-encrypt")]
    public async Task<IResult> GetUserChats()
    {
        if (User.FindFirstValue(ClaimTypes.NameIdentifier) == null)
            return Results.BadRequest();
        
        var getUserChatsQuery = new GetUserChatsQuery(User.FindFirstValue(ClaimTypes.NameIdentifier));
        IEnumerable<ChatDto> chatDtos = await sender.Send(getUserChatsQuery);
        
        return Results.Ok(chatDtos);
    }*/
}