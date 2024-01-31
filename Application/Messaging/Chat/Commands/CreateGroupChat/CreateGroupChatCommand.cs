using MediatR;

namespace Application.Messaging.Chat.Commands.CreateGroupChat;

public record CreateGroupChatCommand(ICollection<string?> UserTags, string ChatName) : IRequest;