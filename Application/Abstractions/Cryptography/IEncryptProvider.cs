namespace Application.Abstractions.Cryptography;

public interface IEncryptProvider
{
    public Task<byte[]> Encrypt(byte[] data, string key);
}