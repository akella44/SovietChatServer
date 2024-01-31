using Domain.Entities;

namespace Application.Interfaces;

public interface IInitialSessionRepository
{
    Task AddSession(InitialSession initialSession);
    Task<InitialSession> GetBySignature(string signature);

    Task<InitialSession> GetByUserPublicKey(string publicKey);
}