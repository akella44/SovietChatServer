using Application.Abstractions.Cryptography;
using MediatR;

namespace Domain.Cryptography;

public record EncryptCommand<TEncoder>(string Data, string Key) : IRequest<string>
    where TEncoder : IEncryptProvider;