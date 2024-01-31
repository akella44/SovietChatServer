namespace Application.Model;

public class ReceivingTextMessage
{
    public DateTime ReceiveMessageTime { get; } = DateTime.Now;
    public required string MessageValue { get; set; }
    public required string ChatId { get; set; }
    public required string MessageOwnerName { get; set; }
}