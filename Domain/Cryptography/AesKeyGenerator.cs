using System.Security.Cryptography;

namespace Domain.Cryptography;

public static class AesKeyGenerator
{
    public static string GenerateKey()
    {
        using (Aes aes = Aes.Create())
        {
            return Convert.ToBase64String(aes.Key);
        }
    }

}