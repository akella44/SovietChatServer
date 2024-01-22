using Domain.Entities;

namespace Application.Interfaces;

public interface ISessionRepository
{
    Task AddSession(Session session);
    Task<Session> GetBySignature(string signature);

    Task<Session> GetByUserPublicKey(string publicKey);
}