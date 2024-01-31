using System.Security.Claims;
using System.Text.Json;
using Application.Messaging.Chat.Queries;
using Application.Messaging.Message.Commands.SendMessage;
using Application.Messaging.Session.Commands.CreateSession;
using Application.Messaging.Session.Queries;
using Application.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs;
[Authorize]
public class ChatHub : Hub
{
    private readonly ISender _sender;

    public ChatHub(ISender sender)
    {
        _sender = sender;
    }

    public override async Task OnConnectedAsync()
    {
        var userTag = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var createSessionCommand = new CreateSessionCommand(userTag, Context.ConnectionId);
        
        await _sender.Send(createSessionCommand);
        
        var chatIdsQuery = new GetUserChatIdsQuery(userTag);
        var chatIdsCollection = await _sender.Send(chatIdsQuery);

        foreach (var chatId in chatIdsCollection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId, default);
        }
    }

    public async Task GetMyChats()
    {
        var getUserChatsQuery = new GetUserChatsQuery(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        IEnumerable<ChatDto> chatDtos = await _sender.Send(getUserChatsQuery);

        var data = JsonSerializer.Serialize(chatDtos, new JsonSerializerOptions
        {
            WriteIndented = false
        });
        await Clients.Caller.SendAsync(data);
    }

    public async Task SendTextMessage(SendTextMessageRequest request)
    {
        var sendMessageCommand = new SendMessageCommand(
            request.ChatId, Context.ConnectionId, Context.User.FindFirstValue(ClaimTypes.NameIdentifier), 
            request.MessageValue, request.MessageType);

        await _sender.Send(sendMessageCommand);

        var receiveMessage = new ReceivingTextMessage
        {
            MessageValue = request.MessageValue,
            MessageOwnerName = Context.User.FindFirstValue(ClaimTypes.NameIdentifier),
            ChatId = request.ChatId
        };
        
        await Clients.Groups(request.ChatId).SendAsync("ReceiveMessage", receiveMessage);
    }
}