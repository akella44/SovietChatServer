using Domain.Entities;

namespace Application.Interfaces;

public interface ISessionRepository
{
    public Task AddSession(Session session);
    public Task<Session> GetSessionByUserId(string Id);

    public Task UpdateOrAddConnection(Session session);
}