using System.Security.Cryptography;
using Application.Abstractions.Cryptography;

namespace Application.Cryptography.Providers;

public class RsaProvider : IEncryptProvider
{
    public async Task<byte[]> Encrypt(byte[] data, string key)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportRSAPublicKey(Convert.FromBase64String(key), out _);
            byte[] encryptedDataBytes = await Task.Run(() => rsa.Encrypt(data, false));
            return encryptedDataBytes;
        }
    }
}