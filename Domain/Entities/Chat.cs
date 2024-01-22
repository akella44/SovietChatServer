using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
[Table("Chats")]
public class Chat
{
    [Key]
    [Column("chat_id")]
    public int ChatId { get; private set; }
}