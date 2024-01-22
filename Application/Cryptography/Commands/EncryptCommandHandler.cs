using System.Text;
using Application.Abstractions.Cryptography;
using MediatR;

namespace Domain.Cryptography;

public class EncryptCommandHandler<TEncoder> : IRequestHandler<EncryptCommand<TEncoder>, string>
    where TEncoder : IEncryptProvider
{
    private readonly TEncoder _encoder;

    public EncryptCommandHandler(TEncoder encoder)
    {
        _encoder = encoder;
    }

    public async Task<string> Handle(EncryptCommand<TEncoder> request, CancellationToken cancellationToken)
    {
        byte[] dataBytes = Convert.FromBase64String(request.Data);
        byte[] encryptedDataBytes = await _encoder.Encrypt(dataBytes, request.Key);
        return Convert.ToBase64String(encryptedDataBytes);
    }
}