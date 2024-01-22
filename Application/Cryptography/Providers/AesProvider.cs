using System.Security.Cryptography;
using Application.Abstractions.Cryptography;

namespace Application.Cryptography.Providers;

public class AesProvider : IEncryptProvider, IDecryptProvider
{
    public async Task<byte[]> Encrypt(byte[] dataBytes, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(key);
            aes.GenerateIV();
            
            using (ICryptoTransform encryptor = aes.CreateEncryptor())
            {
                /*byte[] dataBytes = Convert.FromBase64String(data);*/
                byte[] encryptedDataBytes =
                    await Task.Run(() => encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length)); 
                byte[] result = new byte[aes.IV.Length + encryptedDataBytes.Length];
                Array.Copy(aes.IV, result, aes.IV.Length);
                Array.Copy(encryptedDataBytes, 0, result, aes.IV.Length, encryptedDataBytes.Length);
                return result; /*Convert.ToBase64String(result);*/
            }
        }
    }

    public async Task<byte[]> Decrypt(byte[] dataBytes, string key)
    {
        using (Aes aes = Aes.Create())
        {
            /*byte[] dataBytes = Convert.FromBase64String(data);*/
            aes.Key = Convert.FromBase64String(key);
            byte[] IV = new byte[aes.IV.Length];
            Array.Copy(dataBytes, IV, aes.IV.Length);
            aes.IV = IV;
            
            using (ICryptoTransform decryptor = aes.CreateDecryptor())
            {
                byte[] encryptedData = new byte[dataBytes.Length - aes.IV.Length];
                Array.Copy(dataBytes, aes.IV.Length, encryptedData, 0, encryptedData.Length);
                byte[] decryptedDataBytes =
                    await Task.Run(() => decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length));
                return decryptedDataBytes; /*Encoding.UTF8.GetString(decryptedDataBytes);*/
            }
        }
    }
}