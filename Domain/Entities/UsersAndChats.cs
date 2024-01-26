using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
[Table("UsersWithChats")]
public class UsersAndChats
{
    public string ChatId { get; set; }
    public string UserId { get; set; }
}