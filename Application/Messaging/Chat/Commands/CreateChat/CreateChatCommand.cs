using MediatR;

namespace Application.Messaging.CreateChat;

public record CreateChatCommand(ICollection<string?> UserTags, string ChatName) : IRequest;