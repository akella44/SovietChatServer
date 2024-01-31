using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Session
{
    [Key]
    [Column("session_id")]
    public string SessionId { get; private set; } = Ulid.NewUlid().ToString();
    [Column("user_id")]
    [Required]
    public User User { get; set; }
    [Column("connection_id")]
    [Required]
    public string? Connection { get; set; }
    [Column("session_creation_time")]
    public DateTime CreationTime { get; private set; } = DateTime.UtcNow;
}