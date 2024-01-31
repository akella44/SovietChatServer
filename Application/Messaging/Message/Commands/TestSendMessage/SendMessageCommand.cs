using MediatR;

namespace Application.Messaging.Message.Commands.SendMessage;

public record TestSendMessageCommand(string ChatId, string UserTag, string? Value, string MessageType) : IRequest;