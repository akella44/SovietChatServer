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

    public async Task AddSession(InitialSession initialSession)
    {
       await _appDbContext.Sessions.AddAsync(initialSession);
       await _appDbContext.SaveChangesAsync();
    }

    public async Task<InitialSession> GetBySignature(string signature)
    {
        return await _appDbContext.Sessions
            .FirstAsync(e => e.Signature == signature);
    }

    public async Task<InitialSession> GetByUserPublicKey(string publicKey)
    {
        return await _appDbContext.Sessions
            .FirstAsync(e => e.PublicKey == publicKey);
    }
}