using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;
[Table("Messages")]
public class Message
{
    [Key]
    [Column("message_id")] 
    public string Id { get; private set; } = Ulid.NewUlid().ToString();
    [Required]
    public User MessageOwner { get; init; }
    [Required]
    public Chat MessageChat { get; init; }
    [Required]
    [Column("message_type")]
    [EnumDataType(typeof(MessageTypes))]
    public MessageTypes MessageType { get; init; }
    [Column("message_value")]
    public string? Value { get; init; }
    [Required]
    [Column("message_send_time")]
    public DateTime SendTime { get; private set; } = DateTime.UtcNow;
}