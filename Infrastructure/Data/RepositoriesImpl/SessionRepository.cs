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

    public async Task<Session> GetBySignature(string signature)
    {
        return await _appDbContext.Sessions
            .FirstAsync(e => e.Signature == signature);
    }

    public async Task<Session> GetByUserPublicKey(string publicKey)
    {
        return await _appDbContext.Sessions
            .FirstAsync(e => e.PublicKey == publicKey);
    }
}