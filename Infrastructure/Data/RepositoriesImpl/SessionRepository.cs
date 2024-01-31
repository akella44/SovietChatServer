using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.RepositoriesImpl;

public class SessionRepository : ISessionRepository
{
    private readonly AppDbContext _appDbContext;

    public SessionRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddSession(Session session)
    {
        await _appDbContext.Sessions.AddAsync(session);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Session> GetSessionByUserId(string id)
    {
        return await _appDbContext.Sessions.FirstAsync(s => s.User.Id == id);
    }

    public async Task UpdateOrAddConnection(Session newSession)
    {
        Session? session = await _appDbContext.Sessions.FirstOrDefaultAsync(
            s => s.User.Id == newSession.User.Id);
        if (session != null)
        {
            session.Connection = newSession.Connection;
            await _appDbContext.SaveChangesAsync();
            return;
        }

        await AddSession(newSession);
    }
}