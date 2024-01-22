using Application.Abstractions.Cryptography;
using MediatR;

namespace Domain.Cryptography;

public record DecryptCommand<TDecoder>(string Data, string Key) : IRequest<string>
    where TDecoder : IDecryptProvider;