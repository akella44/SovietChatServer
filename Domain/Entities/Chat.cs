using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;
[Table("Chats")]
public class Chat
{
    public Chat()
    {
        Users = new List<User>();
    }

    [Key] 
    [Column("chat_id")] 
    public string ChatId { get; private set; } = Ulid.NewUlid().ToString();
    
    [Column("chat_creation_time")] 
    public DateTime CreationTime { get; private set; } = DateTime.UtcNow;
    [Column("chat_name")]
    public string ChatName { get; set; }
    [Column("chat_type")]
    [EnumDataType(typeof(ChatTypes))]
    public ChatTypes ChatType { get; init; }

    public ICollection<User> Users { get; set; }
}