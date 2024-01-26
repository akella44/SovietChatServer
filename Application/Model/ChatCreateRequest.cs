namespace Application.Model;

public class ChatCreateRequest
{
    public ICollection<string> UserTags { get; set; }
    public string ChatName { get; set; }
}