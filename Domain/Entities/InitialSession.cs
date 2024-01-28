using System.ComponentModel.DataAnnotations.Schema;
using Domain.Cryptography;

namespace Domain.Entities;
using System.ComponentModel.DataAnnotations;

[Table("InitialSessions")]
public class InitialSession
{
    public InitialSession()
    {
        Signature = Sha256Encoder.Encrypt(SessionId.ToString());
        SessionKey = AesKeyGenerator.GenerateKey();
    }

    [Key]
    [Column("session_id")] 
    public string SessionId { get; private set; } = Ulid.NewUlid().ToString();
    [Column("session_created_time")] 
    public DateTime CreatedTime { get; private set; } = DateTime.UtcNow;

    [Column("session_signature")]
    public string Signature { get; private set; }
    [Base64String]
    [Column("user_public_key")]
    public string PublicKey { get; init; }
    [Base64String]
    [Column("session_key")]
    public string SessionKey { get; private set; }
}