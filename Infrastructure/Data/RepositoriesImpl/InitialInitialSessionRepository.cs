using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.RepositoriesImpl;

public class InitialInitialSessionRepository : IInitialSessionRepository
{
    private readonly AppDbContext _appDbContext;

    public InitialInitialSessionRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddSession(InitialSession initialSession)
    {
       await _appDbContext.InitialSessions.AddAsync(initialSession);
       await _appDbContext.SaveChangesAsync();
    }

    public async Task<InitialSession> GetBySignature(string signature)
    {
        return await _appDbContext.InitialSessions
            .FirstAsync(e => e.Signature == signature);
    }

    public async Task<InitialSession> GetByUserPublicKey(string publicKey)
    {
        return await _appDbContext.InitialSessions
            .FirstAsync(e => e.PublicKey == publicKey);
    }
}