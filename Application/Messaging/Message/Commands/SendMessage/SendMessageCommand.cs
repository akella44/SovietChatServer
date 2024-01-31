using Domain.Enums;
using MediatR;

namespace Application.Messaging.Message.Commands.SendMessage;

public record SendMessageCommand(string ChatId, string ConnectionId, string UserTag, string? Value, MessageTypes MessageType) : IRequest;