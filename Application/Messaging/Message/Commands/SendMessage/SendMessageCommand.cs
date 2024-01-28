using MediatR;

namespace Application.Messaging.Message.Commands.SendMessage;

public record SendMessageCommand(string ChatId, string UserTag, string? Value, string MessageType) : IRequest;