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
    public int Id { get; private set; }
    [Required]
    [Column("user_name")]
    public string Name { get; set; }
    [Required]
    [Column("user_email")]
    public string Email { get; set; }
    [Required]
    [Column("user_tag")]
    public string Tag { get; set; }
    [Required]
    [Column("user_password")]
    public string Password { get; set; }

    public ICollection<Chat> Chats { get; private set; } = new List<Chat>();
}