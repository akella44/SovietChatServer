using Domain.Enums;

namespace Application.Model;

public class SendTextMessageRequest
{
    public required string ChatId { get; set; }
    public required string MessageValue { get; set; }
    public MessageTypes MessageType { get; } = MessageTypes.Message; 
}