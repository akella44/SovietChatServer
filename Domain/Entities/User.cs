using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index(nameof(Tag))]
[Table("Users")]
public class User
{
    [Key]
    [Column("user_id")]
    public int Id { get; set; }
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
    [Required]
    [Column("user_role")]
    public Roles Roles { get; set; }
    public ICollection<Chat> Chats { get; } = new List<Chat>();
}