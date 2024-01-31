using Domain.Entities;
using MediatR;

namespace Application.Messaging.Session.Queries;

public record GetUserChatIdsQuery(string UserTag) : IRequest<IEnumerable<string>>;