using System.Text;
using Application.Abstractions.Cryptography;
using MediatR;

namespace Domain.Cryptography;

public class DecryptCommandHandler<TDecoder> : IRequestHandler<DecryptCommand<TDecoder>, string>
    where TDecoder : IDecryptProvider
{
    private readonly TDecoder _decoder;

    public DecryptCommandHandler(TDecoder decoder)
    {
        _decoder = decoder;
    }

    public async Task<string> Handle(DecryptCommand<TDecoder> request, CancellationToken cancellationToken)
    {
        byte[] encryptedDataBytes = Convert.FromBase64String(request.Data);
        byte[] dataBytes = await _decoder.Decrypt(encryptedDataBytes, request.Key);
        return Encoding.UTF8.GetString(dataBytes);
    }
}