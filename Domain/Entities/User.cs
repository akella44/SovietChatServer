using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index(nameof(Tag))]
[Table("Users")]
public class User
{
    [Key]
    [Column("user_id")]
    public string Id { get; private set; } = Ulid.NewUlid().ToString();
    [Required]
    [Column("user_name")]
    public string Name { get; init; }
    [Required]
    [Column("user_email")]
    public string Email { get; init; }
    [Required]
    [Column("user_tag")]
    public string Tag { get; init; }
    [Required]
    [Column("user_password")]
    public string Password { get; init; }
    public ICollection<Chat> Chats { get; private set; } = new List<Chat>();
}