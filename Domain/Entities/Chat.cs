using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
[Table("Chats")]
public class Chat
{
    [Key] [Column("chat_id")] 
    public string ChatId { get; private set; } = Ulid.NewUlid().ToString();

    [Column("chat_creation_time")] 
    public DateTime CreationTime { get; private set; } = DateTime.Now;
    [Column("chat_name")]
    public string ChatName { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Message> Messages { get; private set; } = new List<Message>();
}