using Application.Model;
using MediatR;

namespace Application.Messaging.Chat.Queries;

public record GetUserChatsQuery(string? Tag) : IRequest<IEnumerable<ChatDto>>;