using System.ComponentModel.DataAnnotations.Schema;
using Domain.Cryptography;

namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;

[Table("Sessions")]
public class Session
{
    public Session()
    {
        SessionId = Ulid.NewUlid().ToString();
        Signature = Sha256Encoder.Encrypt(SessionId.ToString());
        CreatedTime = DateTime.UtcNow;
        SessionKey = AesKeyGenerator.GenerateKey();
    }

    [Key]
    [Column("session_id")] 
    public string SessionId { get; private set; }
    [Column("session_created_time")] 
    public DateTime CreatedTime { get; private set; }

    [Column("session_signature")]
    public string Signature { get; private set; }
    
    [Column("user_public_key")]
    public string PublicKey { get; set; }
    
    [Column("session_key")]
    public string SessionKey { get; private set; }
}