using MediatR;

namespace Application.Messaging.Session.Commands.CreateSession;

public record CreateSessionCommand(string Tag, string ConnectionId) : IRequest;