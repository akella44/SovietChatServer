namespace Application.Abstractions.Cryptography;

public interface IDecryptProvider
{
    public Task<byte[]> Decrypt(byte[] data, string key);
}