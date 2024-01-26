using System.Text;
using Application.Cryptography.Providers;
using Domain.Cryptography;

namespace Application.UnitTests.Cryptography;

public class CryptographyTests
{
    private AesProvider _aesProvider;
    private DecryptCommand<AesProvider> _decryptCommand;
    private DecryptCommandHandler<AesProvider> _decryptCommandHandler;
    private EncryptCommand<AesProvider> _encryptCommand;
    private EncryptCommandHandler<AesProvider> _encryptCommandHandler;
    private string _key;
    [SetUp]
    public void SetUp()
    {
        _key = AesKeyGenerator.GenerateKey();
        _aesProvider = new AesProvider();
        _decryptCommandHandler = new DecryptCommandHandler<AesProvider>(_aesProvider);
        _encryptCommandHandler = new EncryptCommandHandler<AesProvider>(_aesProvider);
    }

    [Test]
    public async Task Aes_Should_Decrypt_Encrypt_Same()
    {
        var message = "qwertyuiopasdfghjklzxcvbnm;',./p[]1234";
        var messageBytes = Encoding.UTF8.GetBytes(message);
        var base64EncodedMessage = Convert.ToBase64String(messageBytes);
        _encryptCommand = new EncryptCommand<AesProvider>(base64EncodedMessage, _key);
        var encryptedMessage = await _encryptCommandHandler.Handle(_encryptCommand, default);

        _decryptCommand = new DecryptCommand<AesProvider>(encryptedMessage, _key);
        var decryptedMessage = await _decryptCommandHandler.Handle(_decryptCommand, default);
        Assert.That(message, Is.EqualTo(decryptedMessage));
    }
}