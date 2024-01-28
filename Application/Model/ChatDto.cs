namespace Application.Model;

public class ChatDto
{
    public required string ChatId { get; init; }
    public required string ChatName { get; init; }
    public required string ChatType { get; init; }
    public string? LastMessageType { get; init; }
    public string? NameOfLastMessageSender { get; init; }
    public DateTime? TimeOfLastMessage { get; init; }
    public string? LastMessageValue { get; init; }
}